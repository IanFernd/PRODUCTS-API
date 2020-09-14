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

        public BuyHelper(IMailer mailer)
        {
            _orderDataAccess = new OrderDataAccess();
            this._mailer = mailer;
        }
        public async void SendFirstEmail(LoadBuyRequest request)
        {
            //  Valores del primer email
            var format = _orderDataAccess.GetEmailFormat(1);
            var sendEmailEntity = new SendEmailEntity()
            {
                Email = request.NewClient.Email,
                NameEmail = request.NewClient.Name + request.NewClient.Surname,
                Subject = "Orden de compra n° " + request.IdOrder,
                Body = format
            };
            sendEmailEntity.Body = sendEmailEntity.Body.Replace("{OrderNumber}", request.IdOrder.ToString());
            sendEmailEntity.Body = sendEmailEntity.Body.Replace("{TotalAmount}", request.TotalAmount.ToString());            
            await _mailer.SendEmailAsync(sendEmailEntity);
        }
    }
}