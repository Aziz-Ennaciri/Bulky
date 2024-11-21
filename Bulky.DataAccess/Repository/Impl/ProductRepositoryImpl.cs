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
    public class ProductRepositoryImpl : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepositoryImpl(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }
        public void Update(Product product)
        {
            _db.Update(product);
        }
    }
}
