using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SieweksCardGameVisual.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieweksCardGameVisual.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        Deck deck = new Deck();
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            return RedirectToPage("BlackJack");
        }
    }
}
