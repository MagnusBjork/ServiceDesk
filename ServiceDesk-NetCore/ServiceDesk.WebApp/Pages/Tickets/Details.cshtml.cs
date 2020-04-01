using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ServiceDesk.WebApp.Domain;
using ServiceDesk.WebApp.Repositories;

namespace ServiceDesk.WebApp.Pages.Tickets
{
    public class DetailsModel : PageModel
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ILogger<DetailsModel> _logger;

        public DetailsModel(ITicketRepository ticketRepository, ILogger<DetailsModel> logger)
        {
            _ticketRepository = ticketRepository;
            _logger = logger;
        }

        public TicketViewModel Ticket { get; private set; }

        public async Task OnGet(Guid id)
        {
            var ticketEntity = await _ticketRepository.GetAsync(id);
            this.Ticket = new TicketViewModel(ticketEntity);
        }
    }
}
