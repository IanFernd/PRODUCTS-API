using System;
using System.Collections.Generic;
using ProductsAPI.Data.Context.Entitys;

namespace ProductsAPI.Data.Request
{
    public class GetOrderResponse
    {
        public ClientsEntity ClientEntity { get; set; }
        public int IdBuy { get; set; } 
        public DateTime UploadDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int IdOrder { get; set; }
        public int  IdClient { get; set; } 
        public List  <BuysDetailsEntity> BuyDetailsEntities { get; set; }
    }
}