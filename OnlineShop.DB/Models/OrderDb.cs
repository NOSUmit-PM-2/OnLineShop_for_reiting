using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DB.Models
{
    public class OrderDb
    {
        public Guid Id { get; set; }
        public CartDB Cart { get; set; }
        public string Address { get; set; }
    }
}
