using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServiceDesk.WebApp.Domain;
using ServiceDesk.WebApp.Services;

namespace ServiceDesk.WebApp.Repositories
{
    public interface ITicketNumberService
    {
        Task<string> RetrieveNewTicketNumberAsync();
    }

    public class TicketNumberService : ITicketNumberService
    {
        private readonly IGenericRepository<TicketNumber> _repository;

        public TicketNumberService(IGenericRepository<TicketNumber> repository)
        {
            _repository = repository;
        }

        public async Task<string> RetrieveNewTicketNumberAsync()
        {
            var entities = await _repository.GetAllAsync();
            var ticketNumber = entities.SingleOrDefault();

            if (ticketNumber is null)
            {
                ticketNumber = new TicketNumber();
                ticketNumber.Id = Guid.NewGuid();
                ticketNumber.CreatedOn = DateTime.Now;
            }

            string newTicketNumber = ticketNumber.GenerateNewTicketNumber();
            ticketNumber.ModifiedOn = DateTime.Now;

            await _repository.UpdateAsync(ticketNumber);

            return newTicketNumber;
        }
    }
}