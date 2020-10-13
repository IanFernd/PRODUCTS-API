using ProductsAPI.Data.Context.Entitys;
using ProductsAPI.Data.Request;
using System.Linq;
using System;
using System.Collections.Generic;
using Email.Service.SmtpSetting;
using Email.Service;
using Email.Service.Entity;

namespace ProductsAPI.Models.Helpers
{
    public class BuyHelper
    {
        private readonly IMailer _mailer;
        private OrderDataAccess _orderDataAccess;
        private ProductDataAccess _productDataAccess;

        public BuyHelper(IMailer mailer)
        {
            _orderDataAccess = new OrderDataAccess();
            _productDataAccess = new ProductDataAccess();
            this._mailer = mailer;
        }
        public async void SendFirstEmail(LoadBuyRequest request)
        {
            //  Valores del primer email al cliente
            var format = _orderDataAccess.GetEmailFormat(1);
            var sendEmailClient = new SendEmailEntity()
            {
                Email = request.NewClient.Email,
                NameEmail = request.NewClient.Name + request.NewClient.Surname,
                Subject = "Orden de compra n° " + request.IdOrder,
                Body = format
            };
            sendEmailClient.Body = sendEmailClient.Body.Replace("{OrderNumber}", request.IdOrder.ToString());
            sendEmailClient.Body = sendEmailClient.Body.Replace("{TotalAmount}", "$" + request.TotalAmount.ToString());            
            await _mailer.SendEmailAsync(sendEmailClient);

            //  Valores del primer email a farma
            List <GetProductResponse> products = new List<GetProductResponse>();
            foreach (var product in request.BuyDetail)
            {
                var productInfo = _productDataAccess.GetByID(product.IdProduct);
                products.Add(productInfo);
            }
            var sendEmailSale = new SendEmailEntity()
            {
                Email = "ferndzian@gmail.com",
                NameEmail = "Venta",
                Subject = "Orden de compra n° " + request.IdOrder,
                Body = "Venta a: " + request.NewClient + "/br Monto Total: " + request.TotalAmount + "/br Realizada el: " + request.UploadDate + "/brDetalle: " + products,
            };
            sendEmailSale.Body = sendEmailSale.Body.Replace("{OrderNumber}", request.IdOrder.ToString());
            sendEmailSale.Body = sendEmailSale.Body.Replace("{Client}", request.NewClient.Name + request.NewClient.Surname);
            await _mailer.SendEmailAsync(sendEmailSale);
        }
    }
}