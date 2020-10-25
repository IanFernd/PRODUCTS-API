using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ProductsAPI.Data.Request;
using ProductsAPI.Models.Helpers;
using Email.Service;
using System.Collections;

namespace ProductsAPI.Models
{

    public class OrderModel
    {

        private OrderDataAccess _orderDataAccess;
        private BuyDataAccess _buyDataAccess;
        private ClientDataAccess _clientDataAccess;
        private OrderHelper _orderHelper;
        public OrderModel(IMailer mailer)
        {
            _orderDataAccess = new OrderDataAccess();
            _buyDataAccess = new BuyDataAccess();
            _clientDataAccess = new ClientDataAccess();
            _orderHelper = new OrderHelper(mailer);
        }


        #region GET


        public GetOrderResponse GetOrder(int request)
        {
            var getOrderResponse = new GetOrderResponse();
            try
            {
                //  Datos de la compra
                getOrderResponse = _orderDataAccess.GetOrder(request);

                // Datos del cliente
                var clientResponse = _clientDataAccess.GetById(getOrderResponse.IdClient);
                getOrderResponse.ClientEntity = clientResponse.ClientEntity;
            }
            catch (Exception ex)
            {
                Console.WriteLine("OrderModel.GetOrder : ERROR : "+ex.Message);
                //  Error interno del servidor
                throw;
            }
            return getOrderResponse;
        } 


        #endregion
        

        #region POST
        

        //  Avanza el state de una order
        public List<string> NextState (LoadNextStateOrder request)
        {
            var resultList = new List<string>();
            string result;
            foreach (int order in request.OrderList)
            {
                var getOrderDetailResponse = _orderDataAccess.GetOrderDetail(order);
                var orderState = getOrderDetailResponse.IdState;
                switch (orderState)
                {
                    case 1:
                        getOrderDetailResponse.IdStateOrder = _orderDataAccess.NextStateOrder(getOrderDetailResponse);
                        //  Introducir el email, el nombre y apellido, resumen y cuerpo del mensaje
                        _orderHelper.SendNextEmail(getOrderDetailResponse);
                        result = order.ToString() + " Paso a pedido en proceso y se mando el email";
                        resultList.Add(result);
                        return resultList;
                    case 2:
                        getOrderDetailResponse.IdStateOrder = _orderDataAccess.NextStateOrder(getOrderDetailResponse);
                        //  Segundo email, su compra se encuentra en proceso
                        // _orderHelper.SendNextEmail(getOrderDetailResponse);
                        result = order.ToString() + " Paso a pedido finalizado y se mando el email";
                        resultList.Add(result);
                        return resultList;
                    case 3:
                        result = order.ToString() + " El pedido ya se encuentra en estado finalizado";
                        resultList.Add(result);
                        return resultList;
                    default:

                        result= order.ToString() + " Error, el codigo de orden ingresado es incorrecto";
                        resultList.Add(result);
                        return resultList;
                }
            }
            return resultList;
        }
        

        #endregion
    }
}
