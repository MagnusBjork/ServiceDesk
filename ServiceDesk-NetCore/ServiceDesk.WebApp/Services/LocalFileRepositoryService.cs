
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ServiceDesk.WebApp.Domain;

namespace ServiceDesk.WebApp.Services
{
    public class LocalFileRepositoryService<T> : IRepositoryService<T> where T : IDomainEntity
    {
        private readonly IConfiguration _configuration;

        private JsonSerializerOptions _options;


        public LocalFileRepositoryService(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;

            if (env.IsDevelopment())
                Setup();

            _options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        private string EntityFolder() => $@"{_configuration["LocalFileRepositoryService:RootFolder"]}\{typeof(T).Name}";
        private string FileUri(Guid id) => $@"{EntityFolder()}\{id}.json";


        private void Setup()
        {
            // Create directory for entity if not exist.
            if (!Directory.Exists(EntityFolder()))
                Directory.CreateDirectory(EntityFolder());
        }



        public async Task<T> GetAsync(Guid id)
        {
            if (File.Exists(FileUri(id)))
            {
                string json = await File.ReadAllTextAsync(FileUri(id));
                return JsonSerializer.Deserialize<T>(json, _options);
            }
            else
                throw new FileNotFoundException();
        }

        public async Task<IEnumerable<T>> GetAllAsync(string queryString)
        {
            var entities = new List<T>();

            foreach (string fileName in Directory.GetFiles(EntityFolder()))
            {
                string json = await File.ReadAllTextAsync(fileName);
                T entity = JsonSerializer.Deserialize<T>(json, _options);
                entities.Add(entity);
            }

            return entities;
        }

        public async Task<T> GetSingleAsync()
        {
            var files = Directory.GetFiles(EntityFolder());
            if (files.Length == 0)
                return default(T);

            if (files.Length > 1)
                throw new Exception("Only a single item allowed!");

            string json = await File.ReadAllTextAsync(files[0]);
            return JsonSerializer.Deserialize<T>(json, _options);
        }

        public async Task<Guid> CreateAsync(T entity)
        {
            string jsonData = JsonSerializer.Serialize(entity, _options);
            await File.WriteAllTextAsync(FileUri(entity.Id), jsonData);

            return entity.Id;
        }

        public async Task UpdateAsync(T entity)
        {
            string jsonData = JsonSerializer.Serialize(entity, _options);
            await File.WriteAllTextAsync(FileUri(entity.Id), jsonData);
        }

        public async Task DeleteAsync(Guid id)
        {
            // There seems to be no DeleteAsync implementation, use FileStream instead
            using (var stream = new FileStream(FileUri(id), FileMode.Truncate,
                FileAccess.ReadWrite, FileShare.Delete, 1, FileOptions.DeleteOnClose))
            {
                await stream.FlushAsync();
            }
        }
    }
}