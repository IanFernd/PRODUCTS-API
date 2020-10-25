using System;
using System.Collections.Generic;

namespace ProductsAPI.Data.Context.Entitys
{
    public partial class BuysEntity
    {
        public int IdBuy { get; set; }
        public DateTime UploadDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int IdClient { get; set; }
        public int IdOrder { get; set; }
        public int? IdMeLi { get; set; }
    }
}
