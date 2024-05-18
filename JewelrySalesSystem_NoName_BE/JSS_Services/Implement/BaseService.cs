using AutoMapper;
using JSS_BusinessObjects;
using JSS_DataAccessObjects;
using JSS_Repositories;
using JSS_Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JSS_Services
{
    public abstract class BaseService<T> where T : class
    {
        protected IUnitOfWork<JewelrySalesSystemContext> _unitOfWork;
        protected ILogger<T> _logger;


        public BaseService(IUnitOfWork<JewelrySalesSystemContext> unitOfWork, ILogger<T> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // protected string GetUsernameFromJwt()
        // {
        //     string username = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //     return username;
        // }
        //
        // protected string GetRoleFromJwt()
        // {
        //     string role = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
        //     return role;
        // }
        //
        //
        // protected string GetBrandIdFromJwt()
        // {
        //     return _httpContextAccessor?.HttpContext?.User?.FindFirstValue("brandId");
        // }
        //
        // protected string GetStoreIdFromJwt()
        // {
        //     return _httpContextAccessor?.HttpContext?.User?.FindFirstValue("storeId");
        // }
    }
}
