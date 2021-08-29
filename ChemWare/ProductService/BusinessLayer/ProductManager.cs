using ProductService.DataAccess;
using ProductService.Models;
using ProductService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.BusinessLayer
{
    public interface IProductManager
    {
        List<ProductListViewModel> GetProductsForListPage(int? max = null, int? page = null, int? productType = null, string productTypeCode = null, string orderBy = null, bool orderByAscending = true);

        ProductDetailViewModel GetProductDetails(int productId);

        bool UpdateProduct(Product newProductValues);

        bool DeleteProduct(int productId, bool purge = false);

        int CreateProduct(Product newProduct);
    }

    public class ProductManager : IProductManager // #dontCallThings'Manager' #everyoneDoesAnyway
    {
        private readonly IProductDataSource _productDataSource;
        private readonly IProductTypeDataSource _productTypeDataSource;

        public ProductManager(IProductDataSource productSource, IProductTypeDataSource typeSauce )
        {
            _productDataSource = productSource;
            _productTypeDataSource = typeSauce;
        }

        //queries and builds the view model for the product list page
        public List<ProductListViewModel> GetProductsForListPage(int? max = null, int? page = null, int? productType = null, string productTypeCode = null, string orderBy = null, bool orderByAscending = true)
        {
            //load the product details
            var dataRes = _productDataSource.GetProducts(max, page, productType, productTypeCode, orderBy, orderByAscending);

            //return early if there are no products found
            if(dataRes == null || dataRes.Count <= 0)
            {
                return null;
            }

            //load the type details for the products
            //could be pushed lower to a sql table join on select
            //or could be pushed higher for the website to populate
            List<int> typeIds = dataRes.Select(x => x.ProductTypeId).Distinct().ToList();
            var typeRes = _productTypeDataSource.GetProductTypes(typeIds);

            //create the new view models for the UI with details from both queries
            List<ProductListViewModel> productDetails = dataRes.Select(x => new ProductListViewModel()
            {
                ProductId = x.ProductId,
                Name = x.Name,
                Price = (float)Math.Round((double)x.Price, 2), //ui should only show 2 decimal places
                Active = x.Active,
                ProductTypeId = x.ProductTypeId,
                //ui should show redable type information
                ProductTypeName = typeRes.Where(y => y.ProductTypeId == x.ProductTypeId).Select(y => y.Name).FirstOrDefault(),
                ProductTypeDescription = typeRes.Where(y => y.ProductTypeId == x.ProductTypeId).Select(y => y.Description).FirstOrDefault()
            }
            ).ToList<ProductListViewModel>();

            return productDetails;
        }

        public ProductDetailViewModel GetProductDetails(int productId)
        {
            //load the product details
            var dataRes = _productDataSource.GetProduct(productId);

            //return early if there are no products found
            if (dataRes == null)
            {
                return null;
            }

            var typeRes = _productTypeDataSource.GetProductType(dataRes.ProductTypeId);

            //create the new view models for the UI with details from both queries
            ProductDetailViewModel productDetails = new ProductDetailViewModel()
            {
                ProductId = dataRes.ProductId,
                Name = dataRes.Name,
                Price = (float)Math.Round((double)dataRes.Price, 2), //ui should only show 2 decimal places
                Active = dataRes.Active,
                ProductTypeId = dataRes.ProductTypeId,
                //ui should show redable type information
                ProductTypeName = typeRes.Name,
                ProductTypeDescription = typeRes.Description
            };

            return productDetails;
        }

        //Will update the matching record by id to match exactly what is passed in.
        public bool UpdateProduct(Product newProductValues)
        {
            //TODO insert any specific required logic here

            //just pass thru to underlying repository
            return _productDataSource.UpdateProduct(newProductValues);
        }


        //deletes a product, either by setting a soft delete flag or by hard delete / purge or db data
        public bool DeleteProduct(int productId, bool purge = false)
        {
            //convert product id to ORM entity, and pass to delete method if it exists
            var product = _productDataSource.GetProduct(productId);

            if(product == null)
            {
                return true; // product doesn't exist already
            }

            //just pass thru to underlying repository
            return _productDataSource.DeleteProduct(product, purge);
        }

        //creates a new product
        public int CreateProduct(Product newProduct)
        {
            //TODO insert any specific required logic here

            //just pass thru to underlying repository
            return _productDataSource.CreateProduct(newProduct);
        }

    }
}
