using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductsAPI.Data.Request;
using System.Text.Json;
using ProductsAPI.Models;
using Email.Service;
using Email.Service.Entity;
using ProductsAPI.Data.Context;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly ILogger<OrderController> _logger;
        private readonly IMailer _mailer;
        private OrderModel _orderModel;
        public OrderController(ILogger<OrderController> logger, IMailer mailer)
        {
            _logger = logger;
            _orderModel = new OrderModel(mailer);
            _mailer = mailer;
        }


        #region GET

        [HttpGet]
        [Route("getorder")]
        //  Obtiene el cliente, la venta y el detalle
        public GetOrderResponse GetOrder(string request)
        {
            var getOrderRequest = JsonSerializer.Deserialize<int>(request);
            return _orderModel.GetOrder(getOrderRequest);
        } 

        #endregion
        

        #region POST

        [HttpPost]
        [Route("nextstate")]
        public List<string> NextState(string request)
        {
            var loadNextStateOrder = JsonSerializer.Deserialize<LoadNextStateOrder>(request);
            return _orderModel.NextState(loadNextStateOrder);
        }


        [HttpGet]
        [Route("export")]
        public void ExportDate(LoadBuyRequest request)
        {
            MASFARMACIADEVContext context = new MASFARMACIADEVContext();
            var sendEmailSale = new SendEmailEntity()
            {
                Email = "ferndzian@gmail.com",
                NameEmail = "Venta",
                Subject = "Orden de compra n° " + request.IdOrder,
                Body = "Nueva Venta! /br {Obj}"
            };
            sendEmailSale.Body = sendEmailSale.Body.Replace("{OrderNumber}", request.IdOrder.ToString());
            sendEmailSale.Body = sendEmailSale.Body.Replace("{Obj}", request.ToString());
            _mailer.SendEmailAsync(sendEmailSale);
        }
        

        #endregion
    }
}
