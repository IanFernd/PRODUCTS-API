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
        public async Task<IActionResult> ExportDate()
        {
            MASFARMACIADEVContext context = new MASFARMACIADEVContext();
            var query = context.EmailsFormatEntity.Find(1);
            var email = "ferndzian@gmail.com";
            var nameEmail = "Ian";
            var nameEmail2 = "Fer";
            var idOrder = 707;
            var amount = 200;
            var sendEmailEntity = new SendEmailEntity()
            {
                Email = email,
                NameEmail = nameEmail + nameEmail2,
                Subject = "Orden de compra n°" + idOrder,
                Body = query.Format
            };
            sendEmailEntity.Body = sendEmailEntity.Body.Replace("{OrderNumber}", idOrder.ToString());
            sendEmailEntity.Body = sendEmailEntity.Body.Replace("{TotalAmount}", amount.ToString());
            await _mailer.SendEmailAsync(sendEmailEntity);
            return NoContent();
        }
        

        #endregion
    }
}
