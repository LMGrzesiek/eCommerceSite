using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceSite.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceSite.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly eCommerceContext _context;

        public CheckoutController(eCommerceContext context)
        {
            _context = context;
        }

        private const string ANONYMOUS_IDENTIFIER = "AnonymousIdentifier";


        public async Task <IActionResult> Index()
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

            if (Request.Method == "POST")
            {
                Order order = new Order
                {
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
                return RedirectToAction("Index", "Receipt", new { id = order.TrackingNumber });
            }
            return View();
        }
            

            
        
    }
}