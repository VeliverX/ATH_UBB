﻿using ATH_UBB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ATH_UBB.Service;

namespace IRepositoryService
{
    public interface IRepositoryService<T> where T : IEntity<Guid>
    {
        IQueryable<T> GetAllRecords();

        T GetSingle(Guid id);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        ServiceResult Add(T entity);
        ServiceResult Delete(T entity);
        ServiceResult Edit(T entity);
        ServiceResult Save();
    }
}
