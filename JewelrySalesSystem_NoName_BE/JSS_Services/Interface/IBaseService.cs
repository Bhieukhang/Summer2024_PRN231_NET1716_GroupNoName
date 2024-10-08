﻿using JSS_BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JSS_Services
{
    public interface IBaseService<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        Task<GenericRespones<TEntity>> GetAsync(int pageIndex = 0, int pageSize = 0, Expression<Func<TEntity, bool>> filter = null,
                                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                        string includeProperties = "");
        Task<TDto> CreateAsync(TDto dto);
        Task<TDto> UpdateAsync(TDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> HideAsync(Guid id, string value);
        Task<bool> HideItem(Guid id, bool value);
        Task<TDto> GetByIdAsync(Guid id);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null);
        Task<TEntity> GetFirst(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "");

    }
}
