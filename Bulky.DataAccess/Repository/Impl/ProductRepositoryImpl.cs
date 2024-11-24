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
            /*_db.Update(product);*/
            var objFrmDb = _db.Products.FirstOrDefault(u => u.Id == product.Id);
            if (objFrmDb != null)
            {
                objFrmDb.Title = product.Title;
                objFrmDb.ISBN = product.ISBN;
                objFrmDb.Price = product.Price;
                objFrmDb.Price50 = product.Price50;
                objFrmDb.ListPrice = product.ListPrice;
                objFrmDb.Price100 = product.Price100;
                objFrmDb.Description = product.Description;
                objFrmDb.categoryId = product.categoryId;
                objFrmDb.Author = product.Author;
                if (product.imageUrl != null)
                {
                    objFrmDb.imageUrl= product.imageUrl;
                }
            }
        }
    }
}
