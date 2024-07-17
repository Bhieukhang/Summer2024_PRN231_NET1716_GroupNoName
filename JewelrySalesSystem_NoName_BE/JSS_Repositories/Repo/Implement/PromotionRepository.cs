using JSS_BusinessObjects.Models;
using JSS_DataAccessObjects;
using JSS_Repositories.Repo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;  
using System.Threading.Tasks;

namespace JSS_Repositories.Repo.Implement
{
    public class PromotionRepository : GenericRepository<Promotion> , IPromotionRepository
    {
        public PromotionRepository(JewelrySalesSystemContext dbContext) : base(dbContext)
        {
        }
    }
}
