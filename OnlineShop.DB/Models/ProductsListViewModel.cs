using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DB.Models
{
    internal class ProductsListViewModel
    {
        public List<ProductDB> Products { get; set; }
        public ProductCategory? SelectedCategory { get; set; }
        public List<ProductCategory> Categories { get; set; }

    }
}
