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
    public class CardTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CardTypesController(ApplicationDbContext context)
        {
            _context = context;


            if (context.CardTypes.Count() == 0)
            {
                context.CardTypes.Add(new CardType { Name = "Ebb/Flow", Description = "New flip card", ImageUrl = "https://i.redd.it/65jdkeziw9521.png", Price = 14.99m, Quantity = 1 });
                context.SaveChanges();
            }
        }

        private const string ANONYMOUS_IDENTIFIER = "AnonymousIdentifier";

        // GET: CardTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.CardTypes.ToListAsync());
        }

        public IActionResult add(int quantity, int id)
        {

            //Three fundamental questions: Who is the user? ( If anonymous, how do I track that?), Does the cart exist?, does the item exist?
            string username = null;
            string anonymousIdentifier = null;
            Cart cart = null;
            if (User.Identity.IsAuthenticated) //All controllers and views have a "user" property which I can check.  This returns true if they are logged in, false otherwise
            {
                username = User.Identity.Name;
                cart = _context.Carts.Include(x => x.CartItems).ThenInclude(x => x.CardType).FirstOrDefault(c => c.UserName == username);
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
                cart = _context.Carts.FirstOrDefault(c => c.AnonymousIdentifier == anonymousIdentifier);
            }

            if (cart == null)
            {
                cart = new Cart
                {
                    AnonymousIdentifier = anonymousIdentifier,
                    UserName = username
                };
                _context.Carts.Add(cart);
                _context.SaveChanges();
            }
            //Once I get past that last block, I will have a cart (either a completely new one, or an existing one)
            //I now need to see if the cart contains this product

            CartItem cartItem = _context.CartItems.FirstOrDefault(x => x.CartID == cart.ID && x.CardTypeID == id);
            //If that line returns NULL, it's a new CartItem
            if(cartItem == null)
            {
                cartItem = new CartItem
                {
                    CartID = cart.ID,
                    Quantity = 0,
                    CardTypeID = id
                };
                _context.CartItems.Add(cartItem);
            }
            cartItem.Quantity += quantity;
            _context.SaveChanges();
            //return Content((username ?? anonymousIdentifier) + " added " + quantity.ToString() + " of " + _context.CardTypes.Find(id).Name);
            return RedirectToAction("Index", "Cart");
        }
    }
}

       
       


        
