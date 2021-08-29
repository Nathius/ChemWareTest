using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductService.DataAccess;
using ProductService.Models;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductDataSource productSource;

        public ProductController(IProductDataSource prod)
        {
            productSource = prod;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get(int? max = null, int? page = null, int? productType = null, string productTypeCode = null, string orderBy = null, bool orderByAscending = true)
        {
            return productSource.GetProducts(max, page, productType, productTypeCode, orderBy, orderByAscending);
        }
        

        [HttpPost]
        public ActionResult<bool> Post([FromBody] Product newProductValues)
        {
            return productSource.UpdateProduct(newProductValues);
        }


        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id, bool purge)
        {
            //TODO move any more complicated logic into a middle "productLogic / manager" type class
            var product = productSource.GetProduct(id);
            if(product == null)
            {
                //product doesnt exist
                //Could Add a more complex custom action result, with properties for returned data, status messages, error messages, etc
                return true;
            }

            return productSource.DeleteProduct(product, purge);
        }


        [HttpPut]
        public ActionResult<int> Put([FromBody] Product newProduct)
        {
            var res = productSource.CreateProduct(newProduct);
            return res;
        }
    }
}
