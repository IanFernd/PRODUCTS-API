using System;
using System.Collections.Generic;
using ProductsAPI.Data.Context.Entitys;

namespace ProductsAPI.Data.Request
{
    public class LoadNextStateOrder
    {
        public List<int> OrderList { get; set; }
    }
}