using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SieweksCardGameVisual.Classes;
using SieweksCardGameVisual.Models;

namespace SieweksCardGameVisual.Pages
{
    public class BlackJackModel : PageModel
    {
        Deck deck = new Deck();

        // Playersvis player { get; set; }
        Player player1 = new Player();
        Player player2 = new Player();
        public void OnGet()
        {
            deck.builddeck(); 
        }
        public IActionResult OnPost()
        {
            deck.getnextcard();
            player1.hit();
            player1.hit_for_reals_this_time();
            return Page();
        }
    }
}
