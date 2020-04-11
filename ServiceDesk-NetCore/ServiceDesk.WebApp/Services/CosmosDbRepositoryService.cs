using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ServiceDesk.WebApp.Domain;

namespace ServiceDesk.WebApp.Services
{
    public class CosmosDbRepositoryService<T> : IRepositoryService<T> where T : IDomainEntity
    {
        private readonly IConfiguration _configuration;


        private CosmosClient _cosmosClient;

        // private Database _database;

        private Container _container;

        private string _databaseId = "ServiceDesk";
        private string _containerId = "Tickets";


        public CosmosDbRepositoryService(IConfiguration configuration, IWebHostEnvironment env)
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
            Database database = _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseId).GetAwaiter().GetResult();
            _container = database.CreateContainerIfNotExistsAsync(_containerId, "/id").GetAwaiter().GetResult();
        }

        public async Task<T> GetAsync(Guid id)
        {
            try
            {
                ItemResponse<T> response = await _container.ReadItemAsync<T>(id.ToString(), new PartitionKey(id.ToString()));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return default(T);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(string queryString)
        {
            queryString = "SELECT * FROM c";

            var query = _container.GetItemQueryIterator<T>(new QueryDefinition(queryString));
            var entities = new List<T>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                entities.AddRange(response.ToList());
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
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _container.DeleteItemAsync<T>(id.ToString(), new PartitionKey(id.ToString()));
        }
    }
}