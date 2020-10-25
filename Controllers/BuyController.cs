using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductsAPI.Models;
using ProductsAPI.Data.Request;
using System.Text.Json;
using Email.Service;


namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuyController : ControllerBase
    {
        private readonly ILogger<BuyController> _logger;
        private BuyModel _buyModel;

        public BuyController(ILogger<BuyController> logger, IMailer mailer)
        {
            _logger = logger;
            _buyModel = new BuyModel(mailer);
        }


        #region GET
        

        [HttpGet]
        [Route("getbuy")]
        //  Obtiene el cliente, la venta y el detalle
        public GetBuyDetailResponse GetBuy(string request)
        {
            var getBuyDetailRequest = JsonSerializer.Deserialize<GetBuyDetailRequest>(request);
            return _buyModel.GetBuy(getBuyDetailRequest);
        } 

        [HttpGet]
        [Route("getbuyssummary")]
        //  Obtiene todos los headers de compra
        public GetBuysSummaryResponse GetBuysSummary()
        {
            return _buyModel.GetBuysSummary();
        } 


        #endregion
        
        
        #region POST


        [HttpPost]
        [Route("postbuy")]
        public int PostBuy(string request)
        {
            var loadBuyRequest = JsonSerializer.Deserialize<LoadBuyRequest>(request);
            return _buyModel.PostBuy(loadBuyRequest);
        }


        #endregion
    }
}