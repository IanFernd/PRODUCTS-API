using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ProductsAPI.Data.Request;
using ProductsAPI.Data.Context.Entitys;
using ProductsAPI.Data.Context;
 
namespace ProductsAPI.Models
{
    public class BuyDataAccess
    {
        private MASFARMACIADEVContext context;
        
        public BuyDataAccess()
        {
            context = new MASFARMACIADEVContext();
        }


        #region GET
        

        public GetBuyDetailResponse GetBuy(GetBuyDetailRequest request)
        {
            var getBuyDetailResponse = new GetBuyDetailResponse();
            try
            {
                var query = from b in context.BuysEntity
                            join bd in context.BuysDetailsEntity on b.IdBuy equals bd.IdBuy
                            where (b.IdBuy == request.IdBuy)
                            select new
                            {
                                buyDetails = new BuysDetailsEntity
                                {
                                    IdBuyDetail = bd.IdBuyDetail,
                                    IdProduct = bd.IdProduct,
                                    Quantity = bd.Quantity,
                                    IdBuy = bd.IdBuy
                                },
                                buyheader = new BuysEntity
                                {
                                    IdBuy = b.IdBuy,
                                    UploadDate = b.UploadDate,
                                    TotalAmount = b.TotalAmount,
                                    IdOrder = b.IdOrder,
                                    IdClient = b.IdClient,
                                    IdMeLi = b.IdMeLi
                                }
                            };
                if (query != null)
                {
                    var ListTemp = new List<BuysDetailsEntity>();
                    foreach (var obj in query)
                    {
                        if (getBuyDetailResponse.IdClient == 0)
                        {
                            getBuyDetailResponse.IdBuy = obj.buyheader.IdBuy;
                            getBuyDetailResponse.UploadDate = obj.buyheader.UploadDate;
                            getBuyDetailResponse.TotalAmount = obj.buyheader.TotalAmount;
                            getBuyDetailResponse.IdOrder = obj.buyheader.IdOrder;
                            getBuyDetailResponse.IdClient = obj.buyheader.IdClient;
                            if (obj.buyheader.IdMeLi != null)
                            {
                                getBuyDetailResponse.IdMeLi = obj.buyheader.IdMeLi;
                            }
                        }
                        ListTemp.Add(obj.buyDetails);
                    }
                    getBuyDetailResponse.BuyDetailsEntities = ListTemp;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("BuyDataAccess.GetBuy : ERROR : "+ex.Message);
                throw;
            }
            return getBuyDetailResponse;
        }
    
        public GetBuysSummaryResponse GetBuysSummary()
        {
            var getBuysSummaryResponse = new GetBuysSummaryResponse();
            try
            {
                var query = from b in context.BuysEntity
                            select b;
                getBuysSummaryResponse.BuysEntites = query.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("BuyDataAccess.GetBuysSummary : ERROR : "+ex.Message);
                throw;
            }
            return getBuysSummaryResponse;
        } 


        #endregion
        
        
        #region POST


        public int PostBuy(LoadBuyRequest request)
        {
            int idBuyResponse;
            decimal DecimalAmount = Convert.ToDecimal(request.TotalAmount);
            try
            {
                BuysEntity buysEntity = new BuysEntity()
                {
                    UploadDate = request.UploadDate,
                    TotalAmount = DecimalAmount,
                    IdClient = request.IdClient,
                    IdOrder = request.IdOrder,
                    IdMeLi = request.IdMeLi
                };
                context.BuysEntity.Add(buysEntity);
                context.SaveChanges();
                idBuyResponse = buysEntity.IdBuy;
            }
            catch (Exception ex)
            {
                Console.WriteLine("BuyDataAccess.PostBuy : ERROR : "+ex.Message);
                throw;
            }
            return idBuyResponse;
        }

        public void PostBuyDetail(LoadBuyRequest request)
        {
            try
            {
                foreach (var obj in request.BuyDetail)
                {
                    BuysDetailsEntity buysDetailsEntity = new BuysDetailsEntity()
                    {
                        IdProduct = obj.IdProduct,
                        Quantity = obj.Quantity,
                        IdBuy = request.IdBuy
                    };
                    context.BuysDetailsEntity.Add(buysDetailsEntity);
                    context.SaveChanges();      
                }          
            }
            catch (Exception ex)
            {
                Console.WriteLine("BuyDataAccess.PostBuyDetail : ERROR : "+ex.Message);
                throw;
            }
        }


        #endregion
    }
}
