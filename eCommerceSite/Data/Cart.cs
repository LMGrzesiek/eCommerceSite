using eCommerceSite.Data;
using System;
using System.Collections.Generic;

namespace eCommerceSite.data
{
    //YouTube tutorial. Probably doesn't make sense.
    private readonly ApplicationDbContext _applicationDbContext;

    private Cart(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public partial class Cart
    {
        public int CartId { get; set; }

        public List<CartItem> CartItems { get; set; }

    }

    public static Cart GetCart(IServiceProvider services)
    {
        //???
    }
}
