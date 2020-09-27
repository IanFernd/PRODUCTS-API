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
    public class OrderHelper
    {
        private readonly IMailer _mailer;
        private OrderDataAccess _orderDataAccess;
        public OrderHelper(IMailer mailer)
        {
            this._mailer = mailer;
            _orderDataAccess = new OrderDataAccess();
        }

        public async void SendNextEmail(GetOrderDetailResponse request)
        {
            var format = _orderDataAccess.GetEmailFormat(request.IdStateOrder);
            var sendEmailEntity = new SendEmailEntity()
            {
                Email = request.ClientEmail,
                NameEmail = request.ClientName + request.ClientSurname,
                Subject = "Orden de compra n° " + request.IdOrder,
                Body = format
            };
            sendEmailEntity.Body = sendEmailEntity.Body.Replace("{OrderNumber}", request.IdOrder.ToString());
            await _mailer.SendEmailAsync(sendEmailEntity);
        }
    }
}