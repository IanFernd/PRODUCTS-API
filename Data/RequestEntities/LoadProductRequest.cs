using System;
using System.Collections.Generic;

namespace ProductsAPI.Data.Request
{
    public class LoadProductRequest
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public int idMarca { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int IdCategory { get; set; }
        public int IdSubCategory { get; set; }
        public int? Recipe { get; set; }
        public int ImgCount { get; set; }
        public string EAN { get; set; }
    }
}