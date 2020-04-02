using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public List<SelectListItem> Categories { get; set; }

        public async Task OnGetAsync(Guid id)
        {



            Categories = new List<SelectListItem>() {
                new SelectListItem() { Text = "Order", Value = "1000"  } ,
                new SelectListItem() { Text = "Bug", Value = "1001"  } ,
                new SelectListItem() { Text = "User Admin", Value = "1002", Disabled=true } ,
                new SelectListItem() { Text = "Requirement", Value = "1003", }};


            if (id.Equals(Guid.Empty))
                Ticket = new TicketViewModel();
            else
            {
                var ticketEntity = await _ticketRepository.GetTicketAsync(id);
                Ticket = new TicketViewModel(ticketEntity);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // Map fields that can be created/updated from UI
            var ticketEntity = new Ticket();
            ticketEntity.Id = Ticket.Id;
            ticketEntity.Subject = Ticket.Subject;
            ticketEntity.Description = Ticket.Description;
            ticketEntity.Category = Ticket.Category;
            ticketEntity.Severity = Ticket.Severity;
            ticketEntity.From = Ticket.From;

            if (Ticket.Id.Equals(Guid.Empty))
                Ticket.Id = await _ticketRepository.CreateTicketAsync(ticketEntity);
            else
                await _ticketRepository.UpdateTicketAsync(ticketEntity);

            return RedirectToPage("/tickets/index");
        }
    }
}
