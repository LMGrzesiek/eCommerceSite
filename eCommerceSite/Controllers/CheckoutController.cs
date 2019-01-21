using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid;

namespace eCommerceSite.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly eCommerceContext _context;
        private readonly ISendGridClient _sendGridClient;

        public CheckoutController(eCommerceContext context, ISendGridClient sendGridClient)
        {
            _context = context;
            _sendGridClient = sendGridClient;
        }

        
        private const string ANONYMOUS_IDENTIFIER = "AnonymousIdentifier";

        
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Index(CheckoutViewModel model)
        {
            string username = null;
            string anonymousIdentifier = null;
            Cart cart = null;
            if (User.Identity.IsAuthenticated) //All controllers and views have a "user" property which I can check.  This returns true if they are logged in, false otherwise
            {
                username = User.Identity.Name;
                cart = _context.Carts.Include(x => x.CartItems).ThenInclude(x => x.Card).FirstOrDefault(c => c.User.UserName == username);
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

                //TODO: Before converting from a cart to an order, I should validate address + payment info against
                //third party APIs (Braintree + SmartyStreets). If eithe rof these 3rd party APIs cannot process info
                //successfully, I should add a ModelError and re-display the form.
                Order order = new Order
                {
                    ContactEmail =model.Email,
                    ContactPhoneNumber = model.PhoneNumber,
                    ShippingStreet1 = model.Street1,
                    ShippingStreet2 = model.Street2,
                    ShippingCity = model.City,
                    ShippingState = model.State,
                    ShippingPostalCode = model.PostalCode,
                    ShippingRecipient = model.Recipient,
                    ShippingInstructions = model.Instructions,

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
                if (User.Identity.IsAuthenticated)
                {
                    var user = _context.Users.Single(x => x.UserName == User.Identity.Name);
                    user.CartId = null;
                    user.Cart = null;
                }
                _context.Orders.Add(order);
                _context.Carts.Remove(cart);
                Response.Cookies.Delete(ANONYMOUS_IDENTIFIER);
                _context.SaveChanges();

                var message = new SendGrid.Helpers.Mail.SendGridMessage
                {
                    From = new SendGrid.Helpers.Mail.EmailAddress("admin@jacesplace.com", "Card Admins"),
                    Subject = "Receipt for order #" + order.TrackingNumber,
                    HtmlContent = "Thanks for your order!"
                };
                message.AddTo(model.Email);

                var result = await _sendGridClient.SendEmailAsync(message);
                //This can be helpful debug code, but we wont display it out to the user:
                var responseBody = await result.DeserializeResponseBodyAsync(result.Body);
                if (responseBody != null)
                {
                    foreach (var body in responseBody)
                    {
                        Console.WriteLine(body.Key + ":" + body.Value);
                    }
                }

                //TODO: send out an email to the user who placed this order with their order details
                //We'll use a third-party API, SendGrid, for this.
                return RedirectToAction("Index", "Receipt", new { id = order.TrackingNumber });
            }
            return View();
        }
            

            
        
    }
}