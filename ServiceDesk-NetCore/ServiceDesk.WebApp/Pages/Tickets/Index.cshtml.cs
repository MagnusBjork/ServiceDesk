﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ServiceDesk.WebApp.Services;

namespace ServiceDesk.WebApp.Pages.Tickets
{
    public class IndexModel : PageModel
    {
        private readonly ITicketService _ticketRepository;
        private readonly ILogger<EditModel> _logger;

        [TempData]
        public string Message { get; set; }

        public IList<TicketViewModel> Tickets { get; private set; }


        public IndexModel(ITicketService ticketRepository, ILogger<EditModel> logger)
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
            await _ticketRepository.DeleteTicketAsync(id);

            Message = "Ticket deleted successfully!";

            return RedirectToPage();
        }
    }
}
