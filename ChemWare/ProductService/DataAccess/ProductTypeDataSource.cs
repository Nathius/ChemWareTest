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
    public interface IProductTypeDataSource
    {
        //Generic get function for use on main procudt list page
        List<ProductType> GetProductTypes(int? max = null, int? page = null, string orderBy = null, bool orderByAscending = true);

        //Get productTypes by id
        List<ProductType> GetProductTypes(List<int> ids);

        //get specific productType by id
        ProductType GetProductType(int id);

        //Will update the matching record by id to match exactly what is passed in.
        bool UpdateProductType(ProductType newTypeValues);


        //deletes a product, either by setting a soft delete flag or by hard delete / purge or db data
        bool DeleteProductType(ProductType typeToDelete);

        //creates a new product
        int CreateProductType(ProductType newType);
    }

    public class ProductTypeDataSource : IProductTypeDataSource
    {
        private readonly IConfiguration _configuration;

        public ProductTypeDataSource(IConfiguration config)
        {
            _configuration = config;
        }

        private string GetConnectionString()
        {
            return _configuration.GetConnectionString("ChemWare");
        }

        private List<ProductType> RunQuery(string query)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                var results = connection.Query<ProductType>(query).ToList();
                return results;
            }
        }

        //Generic get function for use on main procudt list page
        public List<ProductType> GetProductTypes(int? max = null, int? page = null, string orderBy = null, bool orderByAscending = true)
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
            query += "* FROM ProductType ";

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

        public List<ProductType> GetProductTypes(List<int> ids)
        {
            string idList = string.Join(",", ids);
            string query = string.Format("SELECT * FROM ProductType WHERE ProductTypeId in ({0})", idList);

            return RunQuery(query);
        }

        //get specific productType by id
        public ProductType GetProductType(int id)
        {
            if(id < 0)
            {
                return null;
            }

            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                var result = connection.Get<ProductType>(id);
                return result;
            }
        }


        //Will update the matching record by id to match exactly what is passed in.
        public bool UpdateProductType(ProductType newTypeValues)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                var results = connection.Update<ProductType>(newTypeValues);
                return results;
            }
        }

        //delete a product, either soft delete or hard purge 
        public bool DeleteProductType(ProductType typeToDelete)
        {
            if(typeToDelete == null)
            {
                return true;//already deleted / never existed
            }

            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                    //Hard delete of row
                    var results = connection.Delete<ProductType>(typeToDelete);
                    return results;   
            }
        }

        //creates a new productType record
        public int CreateProductType(ProductType newType)
        {
            //invalidate id, table auto incraments id property
            newType.ProductTypeId = -1;

            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                    var results = connection.Insert<ProductType>(newType);
                    return (int)results;
            }
        }

    }

    
}
