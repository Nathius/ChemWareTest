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

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get(int? max = null, int? page = null, int? productType = null, string productTypeCode = null, string orderBy = null, bool orderByAscending = true)
        {

            return productSource.GetProducts(max, page, productType, productTypeCode, orderBy, orderByAscending);
        }

        //bool UpdateProduct(int productId, Product newProductValues)
        // POST api/values
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
                //TODO  Add more complex custon action result, with properties for returned data, status messages, error messages, etc
                //product doesnt exist
                return true;
            }

            return productSource.DeleteProduct(product, purge);

        }


        //// GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
