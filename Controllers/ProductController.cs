using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using ProductsAPI.Models;
using ProductsAPI.Data.Request;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly ILogger<ProductController> _logger;

        private ProductModel _productModel;

        public ProductController(ILogger<ProductController> logger)
        {
            _productModel = new ProductModel();
            _logger = logger;
        }


        #region GET
        

        [HttpGet]
        [Route("getfilters")]
        //  Obtiene los filtros que luego se utilizaran en el catalogo
        public GetFiltersResponse GetFilters()
        {
            return _productModel.GetFilters();
        }

        [HttpGet]
        [Route("getcatalogall")]
        //  Obtiene todo el catalogo sin filtros
        public GetCatalogResponse GetCatalogAll()
        {   
            return _productModel.GetCatalogAll();
        }

        [HttpGet]
        [Route("getcatalogsearchbar")]
        //  Obtiene todo el catalogo con filtro de marca, description y stock de la searchbar
        public GetCatalogResponse GetCatalogSearchBar(string request)
        {
            var getCatalogRequest = JsonSerializer.Deserialize<GetSearchBarRequest>(request);
            return _productModel.GetCatalogSearchBar(getCatalogRequest);
        }

        [HttpGet]
        [Route("getcatalogbyfilter")]
        //  Obtiene todo el catalogo con filtro de categoria, subcategoria, marca y precio
        public GetCatalogResponse GetCatalogByFilter(string request)
        {
            var getCatalogRequest = JsonSerializer.Deserialize<GetCatalogRequest>(request);
            return _productModel.GetCatalogByFilter(getCatalogRequest);
        }


        #endregion


        #region POST


        [HttpPost]
        [Route("post")]
        //  Carga un producto
        public int Post(string request)
        {   
            var loadProductRequest = JsonSerializer.Deserialize<LoadProductRequest>(request);
            return _productModel.Post(loadProductRequest);
        }
        
        [HttpPost]
        [Route("loadcategory")]
        //  Carga una categoria
        public int LoadCategory(string request)
        {   
            var loadCategoryRequest = JsonSerializer.Deserialize<LoadCategoryRequest>(request);
            return _productModel.LoadCategory(loadCategoryRequest);
        }

        [HttpPost]
        [Route("loadsubcategory")]
        //  Carga una subcategoria
        public int LoadSubCategory(string request)
        {   
            var loadSubCategoryRequest = JsonSerializer.Deserialize<LoadSubCategoryRequest>(request);
            return _productModel.LoadSubCategory(loadSubCategoryRequest);
        }

        [HttpPost]
        [Route("loadmarca")]
        //  Carga una subcategoria
        public int LoadMarca(string request)
        {   
            var loadMarcaRequest = JsonSerializer.Deserialize<LoadMarcaRequest>(request);
            return _productModel.LoadMarca(loadMarcaRequest);
        }


        #endregion
    }
}
