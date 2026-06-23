using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DB.Models
{
    public class FavItemDB
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
    }
}
