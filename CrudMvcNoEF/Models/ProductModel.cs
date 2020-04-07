using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace CrudMvcNoEF.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }

        [DisplayName("Product Name")]
        public string ProdcutName { get; set; }
        [DisplayName("Price")]
        public decimal ProductPrice { get; set; }
        [DisplayName("Count")]
        public int ProductCount { get; set; }
    }
}