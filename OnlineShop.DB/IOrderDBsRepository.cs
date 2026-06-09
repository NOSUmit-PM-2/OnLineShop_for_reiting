using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DB
{
    public interface IOrderDBsRepository
    {
        void Add(CartDB product, string address);
    }
}
