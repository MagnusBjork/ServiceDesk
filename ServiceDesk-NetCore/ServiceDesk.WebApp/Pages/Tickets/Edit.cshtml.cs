using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ServiceDesk.WebApp.Pages.Tickets
{
    public class EditModel : PageModel
    {
        private readonly ILogger<EditModel> _logger;

        public EditModel(ILogger<EditModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public TicketViewModel Ticket { get; set; }

        public void OnGet(int? id)
        {
            if (id is null)
                Ticket = new TicketViewModel();
            else
            {
                Ticket = new TicketViewModel();
                Ticket.TicketId = 2;
                Ticket.Subject = "Detta är ett test";
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();


            // Save Ticket to data layer

            return RedirectToPage();
        }
    }
}
