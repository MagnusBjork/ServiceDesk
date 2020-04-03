using System.Threading.Tasks;
using ServiceDesk.WebApp.Domain;

namespace ServiceDesk.WebApp.Services
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
            // Load ticket number counter
            var ticketNumber = await _repository.GetSingleAsync();

            if (ticketNumber is null)
            {
                // Create a new ticket number counter if it doesn't exist.
                ticketNumber = new TicketNumber();
                ticketNumber.CreateNewTicketNumberCounter();

                await _repository.CreateAsync(ticketNumber);

                // Return ticket number formated string.
                return ticketNumber.GetTicketNumberString();
            }
            else
            {
                // Get next ticket number and save it.
                ticketNumber.CreateNextTicketNumber();
                await _repository.UpdateAsync(ticketNumber);

                // Return ticket number string.
                return ticketNumber.GetTicketNumberString();
            }

        }
    }
}