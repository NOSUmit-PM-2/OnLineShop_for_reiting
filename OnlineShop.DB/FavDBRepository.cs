using Microsoft.EntityFrameworkCore;
using OnlineShop.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DB
{
    public class FavDBRepository : IFavDBRepository
    {
        private readonly DatabaseContext databaseContext;

        public FavDBRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public void Add(FavItemDB favItem)
        {
            databaseContext.FavItemDBs.Add(favItem);
            databaseContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<FavItemDB> GetAll()
        {
            return databaseContext.FavItemDBs.ToList();
        }

        public FavItemDB TryGetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Updata(FavItemDB favItemDB)
        {
            throw new NotImplementedException();
        }
    }
}
