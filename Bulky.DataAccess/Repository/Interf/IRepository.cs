﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyDataAccess.Repository.Interf
{
    internal interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetFirstIdOrDefaul(Expression<Func<T,bool>>filter);
        void Add(T entity);
        void Remove(T entity);  
        void RemoveRange(IEnumerable<T> entities);
    }
}
