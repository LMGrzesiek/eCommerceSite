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

            if (cart == null)
            {
                return NotFound();
            }
            

            return View(cart);
        }
        
    }
}
