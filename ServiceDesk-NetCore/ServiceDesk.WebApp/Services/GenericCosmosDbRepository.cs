using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using ServiceDesk.WebApp.Domain;

namespace ServiceDesk.WebApp.Services
{
    public class GenericCosmosDbRepository<T> : IGenericRepository<T> where T : IDomainEntity
    {
        private JsonSerializerOptions _options;


        public GenericCosmosDbRepository()
        {
            _options = new JsonSerializerOptions

            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }


        public async Task<T> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> CreateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}