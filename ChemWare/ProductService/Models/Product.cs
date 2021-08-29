using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public string Name { get; set; }
        public float Price { get; set; }
        public bool Active { get; set; }
        public int ProductTypeId { get; set; }
        public bool IsDeleted { get; set; }
        

    }
}
