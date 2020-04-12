using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Azure.Cosmos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ServiceDesk.WebApp.Domain;

namespace ServiceDesk.WebApp.Services
{
    public class GenericCosmosDbRepository<T> : IGenericRepository<T> where T : IDomainEntity
    {
        private readonly IConfiguration _configuration;


        private CosmosClient _cosmosClient;

        private CosmosContainer _container;

        private readonly string _databaseId = "ServiceDesk";
        private readonly string _containerId = typeof(T).Name;


        public GenericCosmosDbRepository(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;

            string endpointUri = "https://localhost:8081";
            string primaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

            _cosmosClient = new CosmosClient(endpointUri, primaryKey);

            _container = _cosmosClient.GetContainer(_databaseId, _containerId);

            if (env.IsDevelopment())
                Setup();
        }

        private void Setup()
        {
            var database = _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseId).GetAwaiter().GetResult();
            _container = database.Database.CreateContainerIfNotExistsAsync(_containerId, "/id").GetAwaiter().GetResult();
        }

        public async Task<T> GetAsync(Guid id)
        {
            try
            {
                ItemResponse<T> response = await _container.ReadItemAsync<T>(id.ToString(), new PartitionKey(id.ToString()));
                return response;
            }
            catch (CosmosException ex) when (ex.Status == 404)
            {
                return default(T);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<T>(new QueryDefinition(queryString));
            var entities = new List<T>();

            var queryResponseAsync = query.GetAsyncEnumerator();

            while (await queryResponseAsync.MoveNextAsync())
            {
                entities.Add(queryResponseAsync.Current);
            }

            return entities;
        }

        public async Task<Guid> CreateAsync(T entity)
        {
            entity.Id = Guid.NewGuid();
            await _container.CreateItemAsync<T>(entity, new PartitionKey(entity.Id.ToString()));

            return entity.Id;
        }

        public async Task UpdateAsync(T entity)
        {
            await _container.UpsertItemAsync<T>(entity, new PartitionKey(entity.Id.ToString()));
        }

        public async Task<T> GetSingleAsync()
        {
            var result = await GetAllAsync("");
            return result.FirstOrDefault();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _container.DeleteItemAsync<T>(id.ToString(), new PartitionKey(id.ToString()));
        }
    }
}