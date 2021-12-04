using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SieweksCardGameVisual.Classes;
using SieweksCardGameVisual.Models;
using Newtonsoft.Json;

namespace SieweksCardGameVisual.Pages
{
    public class BlackJackModel : PageModel
    {
        Deck deck;

        public Player player1;
        public List<Cards> hand1;
        public Player player2;
        public List<Cards> hand2;
        public void OnGet()
        {
            var SessionAddress = HttpContext.Session.GetString("Hand1Address");
            var PlayerAddress = HttpContext.Session.GetString("Player1Address");
            var SessionAddress2 = HttpContext.Session.GetString("Hand2Address");
            var PlayerAddress2 = HttpContext.Session.GetString("Player2Address");
            var DeckAddress = HttpContext.Session.GetString("deckAddress");

            deck = JsonConvert.DeserializeObject<Deck>(DeckAddress);
            hand1 = JsonConvert.DeserializeObject<List<Cards>>(SessionAddress);
            player1 = JsonConvert.DeserializeObject<Player>(PlayerAddress);              
            hand2 = JsonConvert.DeserializeObject<List<Cards>>(SessionAddress2);
            player2 = JsonConvert.DeserializeObject<Player>(PlayerAddress2);

        }
        public IActionResult OnPost()
        {
            OnGet();
            deck.getnextcard();
            player1.hit();
            hand1.Add(player1.GetCard());
            if(player1.value > 21)
            {
                return RedirectToPage("Index");
            }
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
            }
            return Page();
        }
    }
}
