using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using ProductsAPI.Data.Request;
using ProductsAPI.Data.Context.Entitys;
using ProductsAPI.Data.Context;

namespace ProductsAPI.Models
{

    public class ProductDataAccess
    {
        private MASFARMACIADEVContext context;
        public ProductDataAccess()
        {
            context = new MASFARMACIADEVContext();
        }


        #region GET
        

        public List<ProductDataAccessResponse> GetFilters()
        {
            var _dataAccessResponse = new List<ProductDataAccessResponse>();
            try
            {
                var query = from p in context.ProductsEntity 
                            join cat in context.CategorysEntity on p.IdCategory equals cat.IdCategory
                            join subcat in context.SubCategorysEntity on p.IdSubCategory equals subcat.IdSubCategory
                            join m in context.MarcasEntity on p.IdMarca equals m.IdMarca
                            where (p.Stock > 0)
                            select new
                            {
                                ProductDataAccessResponse = new ProductDataAccessResponse
                                {
                                    CategoryUsed = new CategorysEntity
                                    {
                                        IdCategory = p.IdCategory,
                                        Description = cat.Description
                                    },
                                    SubCategoryUsed = new SubCategorysEntity
                                    {
                                        IdSubCategory = p.IdSubCategory,
                                        Description = subcat.Description,
                                        IdCategory = subcat.IdCategory
                                    },
                                    MarcaUsed = new MarcasEntity
                                    {
                                        IdMarca = p.IdMarca,
                                        Description = m.Description
                                    }
                                }
                            };
                if (query != null)
                {   
                    foreach (var obj in query)
                    {
                        _dataAccessResponse.Add(obj.ProductDataAccessResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ProductDataAccess.GetFilters : ERROR : "+ex.Message);
                throw;
            }
            return _dataAccessResponse;
        }

        public GetProductResponse GetByID(int IdProduct)
        {
            var getProductResponse = new GetProductResponse();
            try
            {
                var query = from p in context.ProductsEntity
                            join m in context.MarcasEntity on p.IdMarca equals m.IdMarca
                            join cat in context.CategorysEntity on p.IdCategory equals cat.IdCategory
                            join subcat in context.SubCategorysEntity on p.IdSubCategory equals subcat.IdSubCategory
                            where (p.IdProduct == IdProduct)
                            select new
                            {
                                    ProductEntity = new GetProductResponse
                                    {
                                        IdProduct = p.IdProduct,
                                        Description = p.Description,
                                        Name = p.Name,
                                        IdMarca = p.IdMarca,
                                        Marca = m.Description,
                                        Price = p.Price,
                                        Stock = p.Stock,
                                        IdCategory = p.IdCategory,
                                        Category = cat.Description,
                                        IdSubCategory = p.IdSubCategory,
                                        SubCategory = subcat.Description,
                                        EAN = p.EAN,
                                        ImgCount = p.ImgCount
                                    },
                            };
                if (query != null)
                {
                    var firstQueryItem = query.FirstOrDefault();
                    getProductResponse = firstQueryItem.ProductEntity;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ProductDataAccess.GetByID : ERROR : "+ex.Message);
                throw;
            }
            return getProductResponse;
        }

        public List<ProductDataAccessResponse> GetCatalogAll()
        {   
            var _dataAccessResponse = new List<ProductDataAccessResponse>();
            try
            {
                var query = from p in context.ProductsEntity
                            join m in context.MarcasEntity on p.IdMarca equals m.IdMarca
                            join cat in context.CategorysEntity on p.IdCategory equals cat.IdCategory
                            join subcat in context.SubCategorysEntity on p.IdSubCategory equals subcat.IdSubCategory
                            where (p.Stock > 0)
                            select new
                            {
                                ProductDataAccessResponse = new ProductDataAccessResponse
                                {
                                    ProductEntity = new GetProductResponse
                                    {
                                        IdProduct = p.IdProduct,
                                        Description = p.Description,
                                        Name = p.Name,
                                        IdMarca = p.IdMarca,
                                        Marca = m.Description,
                                        Price = p.Price,
                                        Stock = p.Stock,
                                        IdCategory = p.IdCategory,
                                        Category = cat.Description,
                                        IdSubCategory = p.IdSubCategory,
                                        SubCategory = subcat.Description,
                                        EAN = p.EAN,
                                        ImgCount = p.ImgCount
                                    },
                                    CategoryUsed = new CategorysEntity
                                    {
                                        IdCategory = p.IdCategory,
                                        Description = cat.Description
                                    },
                                    SubCategoryUsed = new SubCategorysEntity
                                    {
                                        IdSubCategory = p.IdSubCategory,
                                        Description = subcat.Description,
                                        IdCategory = subcat.IdCategory
                                    },
                                    MarcaUsed = new MarcasEntity
                                    {
                                        IdMarca = m.IdMarca,
                                        Description = m.Description
                                    }
                                }
                            };
                if (query != null)
                {
                    foreach (var obj in query)
                    {
                        _dataAccessResponse.Add(obj.ProductDataAccessResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ProductDataAccess.GetCatalogAll : ERROR : "+ex.Message);
                throw;
            }
            return _dataAccessResponse;
        }

        public List<ProductDataAccessResponse> GetCatalogSearchBar(GetSearchBarRequest request)
        {   
            var _dataAccessResponse = new List<ProductDataAccessResponse>(); 
            try
            {
                var query = from p in context.ProductsEntity
                            join m in context.MarcasEntity on p.IdMarca equals m.IdMarca
                            join cat in context.CategorysEntity on p.IdCategory equals cat.IdCategory
                            join subcat in context.SubCategorysEntity on p.IdSubCategory equals subcat.IdSubCategory
                            where (m.Description.Contains(request.SearchBarText) || p.Description.Contains(request.SearchBarText)) && p.Stock > 0
                            select new
                            {
                                ProductDataAccessResponse = new ProductDataAccessResponse
                                {
                                    ProductEntity = new GetProductResponse
                                    {
                                        IdProduct = p.IdProduct,
                                        Description = p.Description,
                                        Name = p.Name,
                                        IdMarca = p.IdMarca,
                                        Marca = m.Description,
                                        Price = p.Price,
                                        Stock = p.Stock,
                                        IdCategory = p.IdCategory,
                                        Category = cat.Description,
                                        IdSubCategory = p.IdSubCategory,
                                        SubCategory = subcat.Description,
                                        EAN = p.EAN,
                                        ImgCount = p.ImgCount
                                    },
                                    CategoryUsed = new CategorysEntity
                                    {
                                        IdCategory = p.IdCategory,
                                        Description = cat.Description
                                    },
                                    SubCategoryUsed = new SubCategorysEntity
                                    {
                                        IdSubCategory = p.IdSubCategory,
                                        Description = subcat.Description,
                                        IdCategory = subcat.IdCategory
                                    },
                                    MarcaUsed = new MarcasEntity
                                    {
                                        IdMarca = p.IdMarca,
                                        Description = m.Description
                                    }
                                }
                            };
                if (query != null)
                {
                    foreach (var obj in query)
                    {
                        _dataAccessResponse.Add(obj.ProductDataAccessResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ProductDataAccess.GetCatalogSearchBar : ERROR : "+ex.Message);
                throw;
            }
            return _dataAccessResponse;
        }

        public List<ProductDataAccessResponse> GetCatalogFilter(GetCatalogRequest request)
        {   
            var _dataAccessResponse = new List<ProductDataAccessResponse>();
            try
            {
                var query = from p in context.ProductsEntity
                            join m in context.MarcasEntity on p.IdMarca equals m.IdMarca
                            join cat in context.CategorysEntity on p.IdCategory equals cat.IdCategory
                            join subcat in context.SubCategorysEntity on p.IdSubCategory equals subcat.IdSubCategory
                            where (p.Price > request.PriceMin && p.Price < request.PriceMax && p.Stock > 0)
                            select new
                            {
                                ProductDataAccessResponse = new ProductDataAccessResponse
                                {
                                    ProductEntity = new GetProductResponse
                                    {
                                        IdProduct = p.IdProduct,
                                        Description = p.Description,
                                        Name = p.Name,
                                        IdMarca = p.IdMarca,
                                        Marca = m.Description,
                                        Price = p.Price,
                                        Stock = p.Stock,
                                        IdCategory = p.IdCategory,
                                        Category = cat.Description,
                                        IdSubCategory = p.IdSubCategory,
                                        SubCategory = subcat.Description,
                                        EAN = p.EAN,
                                        ImgCount = p.ImgCount
                                    },
                                    CategoryUsed = new CategorysEntity
                                    {
                                        IdCategory = p.IdCategory,
                                        Description = cat.Description
                                    },
                                    SubCategoryUsed = new SubCategorysEntity
                                    {
                                        IdSubCategory = p.IdSubCategory,
                                        Description = subcat.Description,
                                        IdCategory = subcat.IdCategory
                                    },
                                    MarcaUsed = new MarcasEntity
                                    {
                                        IdMarca = m.IdMarca,
                                        Description = m.Description
                                    }
                                }
                            };
                if (query != null)
                {
                    foreach (var obj in query)
                    {
                        _dataAccessResponse.Add(obj.ProductDataAccessResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ProductDataAccess.GetCatalogFilter : ERROR : "+ex.Message);
                throw;
            }
            return _dataAccessResponse;
        }


        #endregion


        #region POST
        

        public void Insert(LoadProductRequest request)
        {   
            try
            {
                ProductsEntity productEntity = new ProductsEntity()
                {
                    Description = request.Description,
                    Name = request.Name,
                    IdMarca = request.idMarca,
                    Stock = request.Stock,
                    Price = request.Price,
                    IdCategory = request.IdCategory,
                    IdSubCategory = request.IdSubCategory,
                    Recipe = request.Recipe,
                    EAN = request.EAN,
                    ImgCount = request.ImgCount
                };
                context.ProductsEntity.Add(productEntity);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ProductDataAccess.Insert : ERROR : "+ex.Message);
                throw;
            }
        }

        public void LoadCategory(LoadCategoryRequest request)
        {   
            try
            {
                CategorysEntity categoryEntity = new CategorysEntity()
                {
                    Description = request.Description
                };
                context.CategorysEntity.Add(categoryEntity);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ProductDataAccess.LoadCategory : ERROR : "+ex.Message);
                throw;
            }
        }

        public void LoadSubCategory(LoadSubCategoryRequest request)
        {   
            try
            {
                SubCategorysEntity subCategoryEntity = new SubCategorysEntity()
                {
                    Description = request.Description
                };
                context.SubCategorysEntity.Add(subCategoryEntity);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ProductDataAccess.LoadSubCategory : ERROR : "+ex.Message);
                throw;
            }
        }

        public void LoadMarca(LoadMarcaRequest request)
        {   
            try
            {
                MarcasEntity marcaEntity = new MarcasEntity()
                {
                    Description = request.Description
                };
                context.MarcasEntity.Add(marcaEntity);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ProductDataAccess.LoadMarca : ERROR : "+ex.Message);
                throw;
            }
        }


        #endregion
    }
}
