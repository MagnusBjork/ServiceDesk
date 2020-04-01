
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using ServiceDesk.WebApp.Domain;

namespace ServiceDesk.WebApp.Services
{
    public interface IRepositoryService<T>
    {
        Task<string> GetAsync(Guid id);
        Task<Guid> CreateAsync(T entity);
        Task UpdateAsync(T entity);
    }

    public class FileRepositoryService<T> : IRepositoryService<T> where T : IDomainEntity
    {
        private static readonly string _rootFolder = @"C:\temp\_tempRepository";

        private JsonSerializerOptions _options;


        public FileRepositoryService()
        {
            _options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        private string FileUri(Guid id) => $@"{_rootFolder}\{id}.json";

        public async Task<string> GetAsync(Guid id)
        {
            if (File.Exists(FileUri(id)))
            {
                //string json = await File.ReadAllTextAsync (fileUri);
                //return JObject.Parse (json);
                return await File.ReadAllTextAsync(FileUri(id));
            }
            else
                throw new FileNotFoundException();
        }

        public async Task<Guid> CreateAsync(T entity)
        {
            entity.Id = Guid.NewGuid();

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