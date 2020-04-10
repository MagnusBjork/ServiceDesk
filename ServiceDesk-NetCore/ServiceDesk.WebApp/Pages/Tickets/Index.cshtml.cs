using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ServiceDesk.WebApp.Services.DomainRepositories;

namespace ServiceDesk.WebApp.Pages.Tickets
{
    public class IndexModel : PageModel
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ILogger<EditModel> _logger;

        [TempData]
        public string Message { get; set; }

        public IList<TicketViewModel> Tickets { get; private set; }


        public IndexModel(ITicketRepository ticketRepository, ILogger<EditModel> logger)
        {
            _ticketRepository = ticketRepository;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            var tickets = await _ticketRepository.GetAllTicketsAsync();

            Tickets = tickets.Select(t => new TicketViewModel(t)).ToList();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            var itemToDelete = id;

            Message = "Ticket deleted successfully!";

            return RedirectToPage();
        }
    }
}
