using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ServiceDesk.WebApp.Pages.Tickets
{
    public class DetailsModel : PageModel
    {
        private readonly ILogger<DetailsModel> _logger;

        public DetailsModel(ILogger<DetailsModel> logger)
        {
            _logger = logger;
        }

        public TicketViewModel Ticket { get; private set; }

        public void OnGet(int id)
        {
            Ticket = new TicketViewModel();
            Ticket.TicketId = id;
            Ticket.Subject = "Detta är ett test";
        }
    }
}
