using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceDesk.WebApp.Domain;
using ServiceDesk.WebApp.Services;

namespace ServiceDesk.WebApp.Repositories
{
    public interface ITicketRepository
    {
        Task<Guid> CreateAsync(Ticket ticket);
        void Delete(Guid id);
        Task<Ticket> GetAsync(Guid id);
        IList<Ticket> GetAll();
        void Update(Ticket ticket);
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
            string json = await _repositoryService.GetAsync(id);


            return new Ticket() { Id = id, TicketNumber = "10001", Subject = "Ticket from repo", CreatedOn = System.DateTime.Now };
        }

        public IList<Ticket> GetAll()
        {
            return new List<Ticket>();
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