using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulkyDataAccess.Data;
using BulkyDataAccess.Repository.Interf;
using BulkyModels.Models;

namespace BulkyDataAccess.Repository.Impl
{
    public class shoppingCartImpl : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;
        public shoppingCartImpl(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }
        public void Update(ShoppingCart shoppingCart)
        {
            _db.shoppingCarts.Update(shoppingCart);
        }
    }
}
