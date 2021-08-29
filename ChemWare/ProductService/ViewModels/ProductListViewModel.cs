using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.ViewModels
{
    public class ProductListViewModel
    {
        public int ProductId { get; set; }

        public string Name { get; set; }
        public float Price { get; set; }
        public bool Active { get; set; }
        public int ProductTypeId { get; set; }

        public string ProductTypeName { get; set; }
        public string ProductTypeDescription { get; set; }


    }
}
