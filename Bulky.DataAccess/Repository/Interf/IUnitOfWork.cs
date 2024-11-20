using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyDataAccess.Repository.Interf
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepo { get; }
        void Save();
    }
}
