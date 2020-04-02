using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using ServiceDesk.WebApp.Domain;

namespace ServiceDesk.WebApp.Services
{
    public class CosmosDbRepositoryService<T> : IRepositoryService<T> where T : IDomainEntity
    {
        private JsonSerializerOptions _options;


        public CosmosDbRepositoryService()
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

        public Task<T> GetSingleAsync()
        {
            throw new NotImplementedException();
        }
    }
}