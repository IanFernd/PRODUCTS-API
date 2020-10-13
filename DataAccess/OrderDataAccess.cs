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

    public class OrderDataAccess
    {
        private MASFARMACIADEVContext context;

        public OrderDataAccess()
        {
            context = new MASFARMACIADEVContext();
        }

        #region GET

        public GetOrderResponse GetOrder(int request)
        {
            var getOrderResponse = new GetOrderResponse();
            try
            {
                var query = from b in context.BuysEntity
                            join bd in context.BuysDetailsEntity on b.IdBuy equals bd.IdBuy
                            where (b.IdOrder == request)
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
                                    IdClient = b.IdClient
                                }
                            };
                if (query != null)
                {
                    var ListTemp = new List<BuysDetailsEntity>();
                    foreach (var obj in query)
                    {
                        if (getOrderResponse.IdClient == 0)
                        {
                            getOrderResponse.IdBuy = obj.buyheader.IdBuy;
                            getOrderResponse.UploadDate = obj.buyheader.UploadDate;
                            getOrderResponse.TotalAmount = obj.buyheader.TotalAmount;
                            getOrderResponse.IdOrder = obj.buyheader.IdOrder;
                            getOrderResponse.IdClient = obj.buyheader.IdClient;
                        }
                        ListTemp.Add(obj.buyDetails);
                    }
                    getOrderResponse.BuyDetailsEntities = ListTemp;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("OrderDataAccess.GetOrder : ERROR : "+ex.Message);
                throw;
            }
            return getOrderResponse;
        }

        public GetOrderDetailResponse GetOrderDetail(int IdOrder)
        {
            var getOrderDetailResponse = new GetOrderDetailResponse();
            try
            {
                var query = from or in context.OrdersEntity
                            join stat in context.StatesOrdersEntity on or.IdOrder equals stat.IdOrder
                            join buy in context.BuysEntity on or.IdOrder equals buy.IdOrder
                            join cl in context.ClientsEntity on buy.IdClient equals cl.IdClient
                            where (or.IdOrder == IdOrder)
                            select new 
                            {
                                orderDetail = new GetOrderDetailResponse
                                {
                                    ClientName = cl.Name,
                                    ClientSurname = cl.Surname,
                                    ClientEmail = cl.Email,
                                    IdOrder = or.IdOrder,
                                    IdOrderType = or.IdTypeOrder,
                                    IdState = stat.IdState,
                                    IdStateOrder = stat.IdStateOrder
                                }
                            };
                foreach (var obj in query)
                {
                    getOrderDetailResponse = obj.orderDetail;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("OrderDataAccess.GetOrderDetail : ERROR : "+ex.Message);
                throw;
            }
            return getOrderDetailResponse;
        }

        public string GetEmailFormat(int idState)
        {
            string formatEmail = "";
            try
            {
                var query = context.EmailsFormatEntity.Find(idState);
                if (query != null)
                {
                    formatEmail = query.Format;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("OrderDataAccess.GetEmailFormat : ERROR : "+ex.Message);
                throw;
            }
            return formatEmail;
        }


        #endregion
        
        
        #region POST


        public int PostOrder(int newOrder)
        {
            int idOrderResponse;
            try
            {
                OrdersEntity orderEntity = new OrdersEntity()
                {
                    IdTypeOrder = newOrder
                };
                context.OrdersEntity.Add(orderEntity);
                context.SaveChanges();
                idOrderResponse = orderEntity.IdOrder;
            }
            catch (Exception ex)
            {
                Console.WriteLine("OrderDataAccess.PostOrder : ERROR : "+ex.Message);
                throw;
            }
            return idOrderResponse;
        }

        public void PostOrderState(int idOrder)
        {
            try
            {
                StatesOrdersEntity stateOrder = new StatesOrdersEntity()
                {
                    IdOrder = idOrder,
                    IdState = 1
                };
                context.StatesOrdersEntity.Add(stateOrder);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("OrderDataAccess.PostOrderState : ERROR : "+ex.Message);
                throw;
            }
        }

        public int NextStateOrder (GetOrderDetailResponse request)
        {
            int newStateOrder;
            try
            {
                StatesOrdersEntity actualState = context.StatesOrdersEntity.Find(request.IdStateOrder);
                if (actualState != null)
                {
                    actualState.IdState = actualState.IdState + 1;
                    context.SaveChanges();
                }
                newStateOrder = actualState.IdState;
            }
            catch (Exception ex)
            {
                Console.WriteLine("OrderDataAccess.NextStateOrder : ERROR : "+ex.Message);
                throw;
            }
            return newStateOrder;
        }


        #endregion
    }
}

