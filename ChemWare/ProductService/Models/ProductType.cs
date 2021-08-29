using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Models
{
    [Table("ProductType")]
    public class ProductType
    {
        [Key]
        public int ProductTypeId { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
