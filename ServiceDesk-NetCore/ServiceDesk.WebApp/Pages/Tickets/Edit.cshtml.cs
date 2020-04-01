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
    public class EditModel : PageModel
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ILogger<EditModel> _logger;

        public EditModel(ITicketRepository ticketRepository, ILogger<EditModel> logger)
        {
            _ticketRepository = ticketRepository;
            _logger = logger;
        }

        [BindProperty]
        public TicketViewModel Ticket { get; set; }

        public async Task OnGetAsync(Guid id)
        {
            if (id.Equals(Guid.Empty))
                Ticket = new TicketViewModel();
            else
            {
                var ticketEntity = await _ticketRepository.GetAsync(id);
                Ticket = new TicketViewModel(ticketEntity);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var ticketEntity = new Ticket();
            ticketEntity.Id = Ticket.Id;
            ticketEntity.Subject = Ticket.Subject;
            ticketEntity.Description = Ticket.Description;
            ticketEntity.Category = Ticket.Category;
            ticketEntity.Severity = Ticket.Severity;
            ticketEntity.From = Ticket.From;

            if (Ticket.Id.Equals(Guid.Empty))
            {
                var id = await _ticketRepository.CreateAsync(ticketEntity);
            }
            else
            {
                _ticketRepository.Update(ticketEntity);
            }

            // Här behöver jag id:t tillbaka.
            return RedirectToPage();
        }
    }
}
