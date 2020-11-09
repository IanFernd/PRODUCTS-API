using System;
using System.Collections.Generic;

namespace ProductsAPI.Data.Request
{
    public class LoadBuyDetailRequest
    {
        public int IdProduct { get; set; }
        public int Quantity { get; set; }
        public string EAN { get; set; }
    }
}