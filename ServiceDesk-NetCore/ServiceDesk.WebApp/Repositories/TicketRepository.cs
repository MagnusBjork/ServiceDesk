using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceDesk.WebApp.Domain;
using ServiceDesk.WebApp.Services;

namespace ServiceDesk.WebApp.Repositories
{
    public interface ITicketRepository
    {
        Task<Ticket> GetTicketAsync(Guid id);
        Task<IEnumerable<Ticket>> GetAllTicketsAsync();
        Task UpdateTicketAsync(Ticket ticket);
        Task<Guid> CreateTicketAsync(Ticket ticket);
        Task DeleteTicketAsync(Guid id);
    }

    public class TicketRepository : ITicketRepository
    {
        private readonly IGenericRepository<Ticket> _repository;
        private readonly ITicketNumberService _ticketNumberService;

        public TicketRepository(IGenericRepository<Ticket> repository, ITicketNumberService ticketNumberService)
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
            return await _repository.GetAllAsync();
        }

        public async Task<Guid> CreateTicketAsync(Ticket ticket)
        {
            ticket.Id = Guid.NewGuid();
            ticket.TicketNumber = await _ticketNumberService.RetrieveNewTicketNumberAsync();
            ticket.CreatedOn = DateTime.Now;
            ticket.ModifiedOn = ticket.CreatedOn;

            return await _repository.CreateAsync(ticket);
        }

        public async Task UpdateTicketAsync(Ticket ticket)
        {
            // Måste hämta entiteten först eftersom vissa fält inte får uppdateras
            var current = await _repository.GetAsync(ticket.Id);
            ticket.Id = current.Id;
            ticket.TicketNumber = current.TicketNumber;
            ticket.CreatedOn = current.CreatedOn;
            ticket.ModifiedOn = DateTime.Now;

            await _repository.UpdateAsync(ticket);
        }

        public async Task DeleteTicketAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}