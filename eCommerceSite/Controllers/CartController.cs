using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eCommerceSite.Data;

namespace eCommerceSite.Controllers
{
    public class CartController : Controller
    {
        private readonly eCommerceContext _context;

        public CartController(eCommerceContext context)
        {
            _context = context;
        }
        private const string ANONYMOUS_IDENTIFIER = "AnonymousIdentifier";
        // GET: Cart/
        public async Task<IActionResult> Index()
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

            
            

            return View(cart);
        }

        public async Task<IActionResult> Remove(int id)
        {
            //Step 1: Get Cart
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
            //Step 2: Find item in cart
            CartItem item = cart.CartItems.FirstOrDefault(x => x.CardID == id);

            //Once you find the item, remove it from the Context , and Save Changes:
            if(item != null)
            {


                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync();
                
            }

            return RedirectToAction("Index", "Cart");


        }

        public async Task<IActionResult> Update(int id, int quantity)
        {
            string username = null;
            string anonymousIdentifier = null;
            Cart cart = null;
            if (User.Identity.IsAuthenticated)   //All controllers and views have a "User" property which I can check.  This returns True if they are logged in, false otherwise
            {
                username = User.Identity.Name;   //I can track carts by user name
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
            CartItem item = cart.CartItems.FirstOrDefault(x => x.CardID == id);

            if (item != null)
            {
                item.Quantity = quantity;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Cart");
        }


    }
}
