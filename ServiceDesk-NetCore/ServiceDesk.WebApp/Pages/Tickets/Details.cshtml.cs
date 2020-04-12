using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ServiceDesk.WebApp.Services;

namespace ServiceDesk.WebApp.Pages.Tickets
{
    public class DetailsModel : PageModel
    {
        private readonly ITicketService _ticketRepository;
        private readonly IUserService _userRepository;
        private readonly ILogger<DetailsModel> _logger;

        public DetailsModel(ITicketService ticketRepository, IUserService userRepository, ILogger<DetailsModel> logger)
        {
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        public TicketViewModel Ticket { get; private set; }

        public async Task OnGet(Guid id)
        {
            var ticketEntity = await _ticketRepository.GetTicketAsync(id);
            this.Ticket = new TicketViewModel(ticketEntity);

            // Lookup related data: users name
            if (!string.IsNullOrEmpty(ticketEntity.AssignedTo))
                Ticket.AssignedToText = _userRepository.GetUserName(Guid.Parse(ticketEntity.AssignedTo));
        }
    }
}
