using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DB
{
    public class OrderDBsRepository : IOrderDBsRepository
    {
        private readonly DatabaseContext dbContext;
        public OrderDBsRepository(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(CartDB cart, string address)
        {
            dbContext.OrderDBs.Add(new Models.OrderDb { Id = new Guid(), Cart = cart, Address = address});
            dbContext.SaveChanges();
        }
    }
}
