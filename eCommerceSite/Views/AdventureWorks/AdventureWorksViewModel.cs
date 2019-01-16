using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTemple.DotNet.ShovelStore.Models.AdventureWorks
{
    public class TopSaleByQuantity
    {
        public int OrderQuantity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class TopSaleByDollar
    {
        public decimal TotalMoney { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class TopSaleReport
    {
        public TopSaleReport()
        {
            TopSaleByDollars = new List<TopSaleByDollar>();
            TopSaleByQuantities = new List<TopSaleByQuantity>();
        }

        public List<TopSaleByDollar> TopSaleByDollars { get; set; }
        public List<TopSaleByQuantity> TopSaleByQuantities { get; set; }
    }
}