using OnlineShop.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DB
{
    public interface IFavDBRepository
    {
        List<FavItemDB> GetAll();
        FavItemDB TryGetById(Guid id);
        void Add(FavItemDB favItem);
        void Updata(FavItemDB favItemDB);
        void Delete(Guid id);
    }
}
