﻿using JSS_BusinessObjects.Models;
using JSS_DataAccessObjects;
using JSS_Repositories.Repo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_Repositories.Repo.Implement
{
    public class DiscountRepository : GenericRepository<Discount> , IDiscountRepository
    {
        public DiscountRepository(JewelrySalesSystemContext dbContext) : base(dbContext)
        {
        }
    }
}
