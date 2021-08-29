using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductService.BusinessLayer;
using ProductService.DataAccess;
using ProductService.Models;
using ProductService.ViewModels;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductManager productManager;

        public ProductController(IProductManager prodManager)
        {
            productManager = prodManager;
        }


        [HttpGet]
        public ActionResult<IEnumerable<ProductListViewModel>> Get(int? max = null, int? page = null, int? productType = null, string productTypeCode = null, string orderBy = null, bool orderByAscending = true)
        {
            //Is this an API specifically for our UI, or a more generic API to our underlying data?
            return productManager.GetProductsForListPage(max, page, productType, productTypeCode, orderBy, orderByAscending);
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDetailViewModel> Get(int id)
        {
            //Is this an API specifically for our UI, or a more generic API to our underlying data?
            return productManager.GetProductDetails(id);
        }

        [HttpPost]
        public ActionResult<bool> Post([FromBody] Product newProductValues)
        {
            return productManager.UpdateProduct(newProductValues);
        }


        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id, bool purge)
        {
            return productManager.DeleteProduct(id, purge);
        }


        [HttpPut]
        public ActionResult<int> Put([FromBody] Product newProduct)
        {
            var res = productManager.CreateProduct(newProduct);
            return res;
        }
    }
}
