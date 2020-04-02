using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceDesk.WebApp.Services
{
    public interface IRepositoryService<T>
    {
        Task<T> GetAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetSingleAsync();
        Task<Guid> CreateAsync(T entity);
        Task UpdateAsync(T entity);
    }
}