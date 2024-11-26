using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyDataAccess.Repository.Interf
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepo{ get; }
        ICategoryRepository CategoryRepo { get; }
        ICompanyRepository CompanyRepo { get; }
        IShoppingCartRepository ShoppingCartRepo { get; }
        IApplicationUserRepository ApplicationUserRepo { get; }
        void Save();
    }
}
