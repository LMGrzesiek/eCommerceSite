using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Braintree;

namespace eCommerceSite.Controllers
{
    public class CheckoutController : Controller
    {
        private const string ANONYMOUS_IDENTIFIER = "AnonymousIDentifier";

        private readonly eCommerceContext _context;
        private readonly IEmailSender _emailSender;
        private readonly IBraintreeGateway _braintreeGateway;

        public CheckoutController(eCommerceContext context, IEmailSender emailSender, IBraintreeGateway braintreeGateway)
        {
            _context = context;
            _emailSender = emailSender;
            _braintreeGateway = braintreeGateway;
        }


        public async Task<IActionResult> Index()
        {
            ViewBag.BraintreeClientToken = await _braintreeGateway.ClientToken.GenerateAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CheckoutViewModel model, string braintreeNonce)
        {
            string username = null;
            string anonymousIdentifier = null;
            Cart cart = null;
            if (User.Identity.IsAuthenticated)   //All controllers and views have a "User" property which I can check.  This returns True if they are logged in, false otherwise
            {
                username = User.Identity.Name;   //track carts by user name
                cart = await _context.Carts.Include(x => x.CartItems).ThenInclude(x => x.Card).FirstOrDefaultAsync(c => c.User.UserName == username);
            }
            else
            {
                anonymousIdentifier = string.Empty;
                if (Request.Cookies.ContainsKey(ANONYMOUS_IDENTIFIER))
                {
                    anonymousIdentifier = Request.Cookies[ANONYMOUS_IDENTIFIER];
                }
                else
                {
                    anonymousIdentifier = Guid.NewGuid().ToString();
                    Response.Cookies.Append(ANONYMOUS_IDENTIFIER, anonymousIdentifier);
                }

                cart = await _context.Carts.Include(x => x.CartItems).ThenInclude(x => x.Card).FirstOrDefaultAsync(c => c.AnonymousIdentifier == anonymousIdentifier);
            }

            if (ModelState.IsValid)
            {
                TransactionRequest transactionRequest = new TransactionRequest
                {
                    Amount = cart.CartItems.Sum(x => x.Quantity * x.Card.Price),
                    PaymentMethodNonce = braintreeNonce
                };
                var transactionResult = await _braintreeGateway.Transaction.SaleAsync(transactionRequest);
                
                    //TODO: Before Converting from a Cart to an order, I should validate address + payment info against third party APIs (Braintree + SmartyStreets).  If either of these 3rd party APIs cannot process the info successfully, I should add a ModelError and re-display the form.
                    Order order = new Order
                    {
                        ContactEmail = model.Email,
                        ContactPhoneNumber = model.PhoneNumber,
                        ShippingCity = model.City,
                        ShippingPostalCode = model.PostalCode,
                        ShippingStreet1 = model.Street1,
                        ShippingState = model.State,
                        PlacementDate = DateTime.UtcNow,
                        TrackingNumber = Guid.NewGuid().ToString().Substring(0, 8),
                        SubTotal = cart.CartItems.Sum(x => x.Quantity * x.Card.Price),
                        Total = cart.CartItems.Sum(x => x.Quantity * x.Card.Price),
                        OrderItems = cart.CartItems.Select(cartItem => new OrderItem
                        {
                            Quantity = cartItem.Quantity,
                            UnitPrice = cartItem.Card.Price,
                            CardID = cartItem.CardID,
                            CardDescription = cartItem.Card.Description,
                            CardName = cartItem.Card.Name,
                            LineTotal = cartItem.Quantity * cartItem.Card.Price
                        }).ToArray()
                    };
                    _context.Orders.Add(order);
                    _context.Carts.Remove(cart);
                    if (User.Identity.IsAuthenticated)
                    {
                        var user = _context.Users.Single(x => x.UserName == User.Identity.Name);
                        user.Cart = null;
                        user.CartId = null;
                    }
                    Response.Cookies.Delete(ANONYMOUS_IDENTIFIER);
                    _context.SaveChanges();

                    await _emailSender.SendEmailAsync(
                        model.Email,
                        "Receipt for order #" + order.TrackingNumber,
                        FormatOrderAsHtml(order, this.HttpContext.Request)
                    );


                    return RedirectToAction("Index", "Receipt", new { id = order.TrackingNumber });

                
            }

                return View();
            
        }
        private string FormatOrderAsHtml(Order order, HttpRequest httpRequest)
        {
            return string.Format(@"
        <div>
            <h1>Pleasure Doing Business!</h1>
            <p>Thanks for order from Jace's Place on {0}. Your order is {1}.  Please check <a href='{2}'>here</a> to track shipping and delivery
            <h2>Order Items</h2>
            <table>
                {3}
            <table>
        </div>",
                DateTime.Now.ToShortDateString(),
                order.TrackingNumber,
                string.Format("{0}://{1}/receipt/index/{2}", httpRequest.Scheme, httpRequest.Host, order.TrackingNumber),
                string.Join("", order.OrderItems.Select(item => string.Format(@"
            <tr>
                <td>{0}</td>
                <td>{1}</td>
            </tr>
        ", item.CardName, item.CardDescription)))
            );
        
        }
    }
}