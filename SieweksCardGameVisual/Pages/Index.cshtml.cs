using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
        Player player1;
        List<Cards> hand1;
        Player player2;
        List<Cards> hand2;
        Deck deck = new Deck();
        string opfirstcard;
        public void OnGet()
        {
            HttpContext.Session.Clear();
        }

        public IActionResult OnPost()
        {
            player1 = new Player();
            hand1 = new List<Cards>();
            player2 = new Player();
            hand2 = new List<Cards>();
            deck.builddeck();


            deck.getnextcard();
            player1.hit();
            hand1.Add(player1.GetCard());


            deck.getnextcard();
            player2.hit();
            hand2.Add(player2.GetCard());
            opfirstcard = hand2.ElementAt(0).imagepath;
            hand2.ElementAt(0).imagepath = "/images/red_joker.png";
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString("Hand1Address",
                JsonConvert.SerializeObject(hand1));
                HttpContext.Session.SetString("Player1Address",
                JsonConvert.SerializeObject(player1));
                HttpContext.Session.SetString("Hand2Address",
                JsonConvert.SerializeObject(hand2));
                HttpContext.Session.SetString("Player2Address",
                JsonConvert.SerializeObject(player2));
                HttpContext.Session.SetString("deckAddress",
                JsonConvert.SerializeObject(deck));
                HttpContext.Session.SetString("opponentshiddencard",
                JsonConvert.SerializeObject(opfirstcard));
            }
            return RedirectToPage("BlackJack");
        }
    }
}
