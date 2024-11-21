using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulkyModels.Models;

namespace BulkyDataAccess.Repository.Interf
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);
    }
}
