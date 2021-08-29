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
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeDataSource typeSource;

        public ProductTypeController(IProductTypeDataSource prod)
        {
            typeSource = prod;
        }


        [HttpGet]
        public ActionResult<IEnumerable<ProductType>> Get(int? max = null, int? page = null, string orderBy = null, bool orderByAscending = true)
        {
            return typeSource.GetProductTypes(max, page, orderBy, orderByAscending);
        }
        

        [HttpPost]
        public ActionResult<bool> Post([FromBody] ProductType newTypeValues)
        {
            return typeSource.UpdateProductType(newTypeValues);
        }


        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            //TODO move any more complicated logic into a middle "productLogic / manager" type class
            var productType = typeSource.GetProductType(id);
            if(productType == null)
            {
                //product doesnt exist
                //Could Add a more complex custom action result, with properties for returned data, status messages, error messages, etc
                return true;
            }

            return typeSource.DeleteProductType(productType);
        }


        [HttpPut]
        public ActionResult<int> Put([FromBody] ProductType newType)
        {
            var res = typeSource.CreateProductType(newType);
            return res;
        }
    }
}
