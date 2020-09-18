using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ProductsAPI.Data.Request;
using ProductsAPI.Models.Helpers;
using Email.Service;

namespace ProductsAPI.Models
{

    public class OrderModel
    {

        private OrderDataAccess _orderDataAccess;
        private OrderHelper _orderHelper;
        public OrderModel(IMailer mailer)
        {
            _orderDataAccess = new OrderDataAccess();
            _orderHelper = new OrderHelper(mailer);
        }


        #region GET


        #endregion
        

        #region POST
        

        //  Avanza el state de una order
        public string NextState (int idOrder)
        {
            // TODO
            var getOrderDetailResponse = _orderDataAccess.GetOrderDetail(idOrder);
            var orderState = getOrderDetailResponse.IdState;
            switch (orderState)
            {
                case 1:
                    _orderDataAccess.NextStateOrder(getOrderDetailResponse);
                    //  introducir el email, el nombre y apellido, resumen y cuerpo del mensaje
                    //_orderHelper.SendNextEmail();
                    return "";
                case 2:
                    _orderDataAccess.NextStateOrder(getOrderDetailResponse);
                    //  state 1 a 2
                    //  Segundo email, su compra se encuentra en proceso
                    //_orderHelper.SendNextEmail();
                    return "";
                case 3:
                    return "El pedido ya se encuentra en estado finalizado";
                default:
                    return "Error, el codigo ingresado es incorrecto";
            }
        }
        

        #endregion
    }
}
