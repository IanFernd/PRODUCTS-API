using System.Collections.Generic;
using ProductsAPI.Data.Context.Entitys;

namespace ProductsAPI.Data.Request
{
    public class ProductDataAccessResponse
    {
        public ProductCardResponse ProductCard { get; set; }
        public CategorysEntity CategoryUsed { get; set; }
        public SubCategorysEntity SubCategoryUsed { get; set; }
        public MarcasEntity MarcaUsed { get; set; }
    }
}