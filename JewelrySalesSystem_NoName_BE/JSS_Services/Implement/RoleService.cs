using JSS_BusinessObjects.Models;
using JSS_DataAccessObjects;
using JSS_Repositories;
using JSS_Services.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_Services.Implement
{
    public class RoleService : BaseService<RoleService>, IRoleService
    {
        public RoleService(IUnitOfWork<JewelrySalesSystemContext> unitOfWork, ILogger<RoleService> logger) : base(unitOfWork, logger)
        {
        }

        public async Task<Role> GetRoleByIdAsync(Guid? id)
        {
            return await _unitOfWork.GetRepository<Role>().FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
