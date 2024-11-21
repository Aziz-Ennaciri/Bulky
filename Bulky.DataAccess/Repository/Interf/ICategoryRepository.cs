﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulkyModels.Category;

namespace BulkyDataAccess.Repository.Interf
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category obj);
    }
}