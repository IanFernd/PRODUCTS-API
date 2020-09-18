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

        public OrderHelper(IMailer mailer)
        {
            this._mailer = mailer;
        }
        public async void SendNextEmail(LoadBuyRequest request)
        {
            var sendEmailEntity = new SendEmailEntity()
            {
                Email = request.NewClient.Email,
                NameEmail = request.NewClient.Name + request.NewClient.Surname,
                Subject = "Orden de compra n°" + request.IdOrder,
                Body = ""
            };
            await _mailer.SendEmailAsync(sendEmailEntity);
        }
    }
}