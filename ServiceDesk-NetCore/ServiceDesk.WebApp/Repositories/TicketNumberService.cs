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
        Task<string> GenerateTicketNumberAsync();
    }

    public class TicketNumberService : ITicketNumberService
    {
        private readonly IRepositoryService<TicketNumber> _repository;

        public TicketNumberService(IRepositoryService<TicketNumber> repository)
        {
            _repository = repository;
        }

        public async Task<string> GenerateTicketNumberAsync()
        {
            var current = await _repository.GetSingleAsync();

            if (current is null)
            {
                // Create a new ticket number counter if it doesn't exists.
                var newTicketNumber = new TicketNumber();
                await _repository.CreateAsync(newTicketNumber);

                // Return ticket number string.
                return newTicketNumber.GetTicketNumber();
            }
            else
            {
                // Load current and step up counter and update.
                var newTicketNumber = new TicketNumber(current);
                newTicketNumber.GenerateNewTicketNumber();
                await _repository.UpdateAsync(newTicketNumber);

                // Return ticket number string.
                return newTicketNumber.GetTicketNumber();
            }

        }
    }
}