using ProductService.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using Dapper.Contrib;
using System.Data;
using Dapper.Contrib.Extensions;

namespace ProductService.DataAccess
{
    public interface IProductDataSource
    {
        //Generic get function for use on main procudt list page
        List<Product> GetProducts(int? max = null, int? page = null, int? productType = null, string productTypeCode = null, string orderBy = null, bool orderByAscending = true);

        //Will update the matching record by id to match exactly what is passed in.
        bool UpdateProduct(Product newProductValues);
    }

    public class ProductDataSource : IProductDataSource
    {
        private readonly IConfiguration _configuration;

        public ProductDataSource(IConfiguration config)
        {
            _configuration = config;
        }

        private string GetConnectionString()
        {
            return _configuration.GetConnectionString("ChemWare");
        }

        private List<Product> RunQuery(string query)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                var results = connection.Query<Product>(query).ToList();
                return results;
            }
        }

        //Generic get function for use on main procudt list page
        public List<Product> GetProducts(int? max = null, int? page = null, int? productType = null, string productTypeCode = null, string orderBy = null, bool orderByAscending = true)
        {
            string query = "SELECT ";

            //Limit results if TOP param is specified
            // IF page number is specified use pagination in the 'OFFSET' section below
            if (max.HasValue && max.Value > 0
                && page.HasValue == false)
            {
                query += string.Format("TOP ({0}) ", max.Value);
            }

            //select products
            query += "* FROM Product ";


            //Limit results to product type if specified as either ID or type code
            if(productType.HasValue)
            {
                query += string.Format("WHERE productTypeId = {0} ", productType.Value);
            }
            else if(string.IsNullOrEmpty(productTypeCode) == false)
            {
                //TODO possible sql injection, sanatise strings coming from UI
                query += string.Format("WHERE productTypeId = (select productTypeId from ProductType where code = '{0}') ", productTypeCode);
            }

            //order by field and direction if specified
            if(string.IsNullOrEmpty(orderBy) == false)
            {
                if(orderByAscending)
                {
                    query += string.Format("ORDER BY {0} {1} ", orderBy, "asc");
                }
                else
                {
                    query += string.Format("ORDER BY {0} {1} ", orderBy, "desc");
                }
            }
            else
            {
                query += "ORDER BY Name asc ";
            }

            //paginate results if specified
            if(max.HasValue && page.HasValue)
            {
                query += string.Format("OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", (max.Value * page.Value), max.Value );
            }

            return RunQuery(query);
        }

        //Will update the matching record by id to match exactly what is passed in.
        public bool UpdateProduct(Product newProductValues)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                var results = connection.Update<Product>(newProductValues);
                return results;
            }
        }

    }

    
}
