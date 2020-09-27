using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using ProductsAPI.Data.Request;
using ProductsAPI.Data.Context.Entitys;
using ProductsAPI.Models.Helpers;

namespace ProductsAPI.Models
{

    public class ProductModel
    {
        private ProductDataAccess _productDataAccess;
        private ProductHelper _productHelper;
        public ProductModel()
        {
            _productDataAccess = new ProductDataAccess();
            _productHelper = new ProductHelper();
        }


        #region GET
        

        public GetFiltersResponse GetFilters()
        {
            var getFiltersResponse = new GetFiltersResponse();
            try
            {
                var _dataAccessResponse = _productDataAccess.GetFilters();
                if (_dataAccessResponse != null)
                {
                    getFiltersResponse = _productHelper.CreateCategoryTree(_dataAccessResponse);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ProductModel.GetFilters : ERROR : "+ex.Message);
                throw;
            }
            return getFiltersResponse;
        }

        public GetCatalogResponse GetCatalogAll()
        {   
            var getCatalogResponse = new GetCatalogResponse();
            try
            {
                var _dataAccessResponse = _productDataAccess.GetCatalogAll();
                if (_dataAccessResponse != null)
                {
                    getCatalogResponse = _productHelper.CreateList(_dataAccessResponse);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ProductModel.GetCatalogAll : ERROR : "+ex.Message);
                throw;
            }
            return getCatalogResponse;
        }

        public GetCatalogResponse GetCatalogSearchBar(GetSearchBarRequest request)
        {   
            var getCatalogResponse = new GetCatalogResponse();
            try
            {
                var _dataAccessResponse = _productDataAccess.GetCatalogSearchBar(request);
                if (_dataAccessResponse != null)
                {
                    getCatalogResponse = _productHelper.CreateList(_dataAccessResponse);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ProductModel.GetCatalogSearchBar : ERROR : "+ex.Message);
                throw;
            }
            return getCatalogResponse;
        }

        public GetCatalogResponse GetCatalogByFilter(GetCatalogRequest request)
        {   
            var getCatalogResponse = new GetCatalogResponse();
            try
            {
                var _dataAccessResponse = _productDataAccess.GetCatalogFilter(request);
                if(_dataAccessResponse != null)
                {
                    getCatalogResponse = _productHelper.CatalogFilter(_dataAccessResponse, request);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ProductModel.GetCatalogByFilter : ERROR : "+ex.Message);
                throw;
            }
            return getCatalogResponse;
        }


        #endregion


        #region POST


        public int Post(LoadProductRequest request)
        {   
            try
            {
                _productDataAccess.Insert(request);
                //Retorna 204: La peticion ha sido manejada con exito y la respuesta no tiene contenido
                return 204;  
            }
            catch (Exception ex)
            {
                Console.WriteLine("ProductModel.Post : ERROR : "+ex.Message);
                //Error interno del servidor
                return 500;
            }
        }

        public int LoadCategory(LoadCategoryRequest request)
        {   
            try
            {
                _productDataAccess.LoadCategory(request);
                //Retorna 204: La peticion ha sido manejada con exito y la respuesta no tiene contenido
                return 204;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ProductModel.LoadCategory : ERROR : "+ex.Message);
                //Error interno del servidor
                return 500;
            }
        }

        public int LoadSubCategory(LoadSubCategoryRequest request)
        {   
            try
            {
                _productDataAccess.LoadSubCategory(request);
                //Retorna 204: La peticion ha sido manejada con exito y la respuesta no tiene contenido
                return 204;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ProductModel.LoadSubCategory : ERROR : "+ex.Message);
                //Error interno del servidor
                return 500;
            }
        }

        public int LoadMarca(LoadMarcaRequest request)
        {   
            try
            {
                _productDataAccess.LoadMarca(request);
                //Retorna 204: La peticion ha sido manejada con exito y la respuesta no tiene contenido
                return 204;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ProductModel.LoadMarca : ERROR : "+ex.Message);
                //Error interno del servidor
                return 500;
            }
        }


        #endregion
    }
}
