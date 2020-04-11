using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceDesk.WebApp.Domain;
using ServiceDesk.WebApp.Services;

namespace ServiceDesk.WebApp.Services.DomainRepositories
{
    public interface ITicketRepository
    {
        Task<Ticket> GetTicketAsync(Guid id);
        Task<IEnumerable<Ticket>> GetAllTicketsAsync();
        Task UpdateTicketAsync(Guid id, Ticket ticket);
        Task<Guid> CreateTicketAsync(Ticket ticket);
        Task DeleteTicketAsync(Guid id);
    }

    public class TicketRepository : ITicketRepository
    {
        private readonly IRepositoryService<Ticket> _repository;
        private readonly ITicketNumberService _ticketNumberService;

        public TicketRepository(IRepositoryService<Ticket> repository, ITicketNumberService ticketNumberService)
        {
            _repository = repository;
            _ticketNumberService = ticketNumberService;
        }

        public async Task<Ticket> GetTicketAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            string queryString = "SELECT * FROM c";

            return await _repository.GetAllAsync(queryString);
        }

        public async Task<Guid> CreateTicketAsync(Ticket ticket)
        {
            ticket.Id = Guid.NewGuid();
            ticket.TicketNumber = await _ticketNumberService.GenerateTicketNumberAsync();
            ticket.CreatedOn = DateTime.Now;
            ticket.ModifiedOn = ticket.CreatedOn;

            return await _repository.CreateAsync(ticket);
        }

        public async Task UpdateTicketAsync(Guid id, Ticket ticket)
        {
            // First, get the current object and then overload only the properties that are allowed to be updated.
            var current = await _repository.GetAsync(id);
            ticket.Id = current.Id;
            ticket.TicketNumber = current.TicketNumber;
            ticket.CreatedOn = current.CreatedOn;
            ticket.ModifiedOn = DateTime.Now;

            await _repository.UpdateAsync(ticket);
        }

        public async Task DeleteTicketAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}