using OnlineShop.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DB
{
    public class FavInMemoryRepository : IFavDBRepository
    {
        public List<FavItemDB>  favItemDBs = new List<FavItemDB>();
        public void Add(FavItemDB favItem)
        {
            favItemDBs.Add(favItem);
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<FavItemDB> GetAll()
        {
            return favItemDBs;
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
