using System;
using System.Collections.Generic;

namespace ProductsAPI.Data.Context.Entitys
{
    public partial class ProductsEntity
    {
        public int IdProduct { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int IdMarca { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int IdCategory { get; set; }
        public int IdSubCategory { get; set; }
        public int? Recipe { get; set; }
        public int ImgCount { get; set; }
        public string EAN { get; set; }
    }
}
