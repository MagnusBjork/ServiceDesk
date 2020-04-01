using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceDesk.WebApp.Domain;
using ServiceDesk.WebApp.Services;

namespace ServiceDesk.WebApp.Repositories
{
    public interface ITicketRepository
    {
        Task<Ticket> GetAsync(Guid id);
        Task<IEnumerable<Ticket>> GetAllAsync();
        void Update(Ticket ticket);
        Task<Guid> CreateAsync(Ticket ticket);
        void Delete(Guid id);
    }

    public class TicketRepository : ITicketRepository
    {
        private readonly IRepositoryService<Ticket> _repositoryService;

        public TicketRepository(IRepositoryService<Ticket> repositoryService)
        {
            _repositoryService = repositoryService;
        }

        public async Task<Ticket> GetAsync(Guid id)
        {
            return await _repositoryService.GetAsync(id);
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            return await _repositoryService.GetAllAsync();
        }

        public async Task<Guid> CreateAsync(Ticket ticket)
        {
            return await _repositoryService.CreateAsync(ticket);
        }

        public void Update(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

    }
}