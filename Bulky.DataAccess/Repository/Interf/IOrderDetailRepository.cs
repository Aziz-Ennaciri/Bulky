using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulkyModels.Models;

namespace BulkyDataAccess.Repository.Interf
{
    public interface IOrderDetailRepository :IRepository<OrderDetail>
    {
        void Update(OrderDetail orderDetail);
    }
}
