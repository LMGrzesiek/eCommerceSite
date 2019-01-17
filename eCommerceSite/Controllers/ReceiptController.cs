using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceSite.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceSite.Controllers
{
    public class ReceiptController : Controller
    {

        private readonly eCommerceContext _context;

        public ReceiptController(eCommerceContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string id)
        {
            var order = await _context.Orders.Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.TrackingNumber == id);
            return View(order); //We'll wire up the View in a minute
           
        }
    }
}