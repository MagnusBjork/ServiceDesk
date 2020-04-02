
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ServiceDesk.WebApp.Domain;

namespace ServiceDesk.WebApp.Services
{
    public class LocalFileRepositoryService<T> : IRepositoryService<T> where T : IDomainEntity
    {
        // private static readonly string _rootFolder = @"C:\temp\_tempRepository";
        private readonly IConfiguration _configuration;

        private JsonSerializerOptions _options;


        public LocalFileRepositoryService(IConfiguration configuration)
        {
            _configuration = configuration;

            _options = new JsonSerializerOptions

            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        private string FolderPath() => $@"{_configuration["LocalFileRepositoryService:RootFolder"]}\{typeof(T).Name}";
        private string FileUri(Guid id) => $@"{FolderPath()}\{id}.json";

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

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = new List<T>();

            foreach (string fileName in Directory.GetFiles(FolderPath()))
            {
                string json = await File.ReadAllTextAsync(fileName);
                T entity = JsonSerializer.Deserialize<T>(json, _options);
                entities.Add(entity);
            }

            return entities;
        }

        public async Task<T> GetSingleAsync()
        {
            var files = Directory.GetFiles(FolderPath());
            if (files.Length == 0)
                return default(T);

            if (files.Length > 1)
                throw new Exception("Only one item allowed!");

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




    }
}