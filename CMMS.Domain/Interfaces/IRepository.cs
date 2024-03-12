using CMMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Domain.Interfaces
{
    public interface IRepository<T>where T : class
    {
        Task <IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);


        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);



    }
}
