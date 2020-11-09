using System;
using System.Collections.Generic;
using ProductsAPI.Data.Context.Entitys;

namespace ProductsAPI.Data.Request
{
    public class LoadBuyRequest
    {
        public LoadClientRequest NewClient { get; set; }
        public DateTime UploadDate { get; set; }
        public string TotalAmount { get; set; }
        public int IdClient { get; set; }
        public int IdTypeOrder { get; set; }
        public int IdOrder { get; set; }
        public int IdBuy { get; set; } 
        public int? IdMeLi { get; set; }
        public List  <LoadBuyDetailRequest> BuyDetail { get; set; }
    }
}