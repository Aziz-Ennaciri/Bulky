using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulkyDataAccess.Data;
using BulkyDataAccess.Repository.Interf;

namespace BulkyDataAccess.Repository.Impl
{
    public class UnitOfWorkImpl : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public ICategoryRepository CategoryRepo { get; private set; }
        public IProductRepository ProductRepo { get; private set; }
        public ICompanyRepository CompanyRepo { get; private set; } 
        public IShoppingCartRepository ShoppingCartRepo { get; private set; }
        public IApplicationUserRepository ApplicationUserRepo { get; private set; }
        public UnitOfWorkImpl(ApplicationDbContext db) 
        {
            _db = db;
            CategoryRepo = new CategoryImpl(_db);
            ProductRepo = new ProductRepositoryImpl(_db);
            CompanyRepo = new CompanyImpl(db);
            ShoppingCartRepo = new shoppingCartImpl(_db);
            ApplicationUserRepo = new ApplicationUserImpl(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
