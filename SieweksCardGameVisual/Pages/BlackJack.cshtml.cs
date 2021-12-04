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
        Deck deck = new Deck();

        public Player player1;
        public List<Cards> hand1;


        public void OnGet()
        {
            var SessionAddress = HttpContext.Session.GetString("Hand1Address");
            var PlayerAddress = HttpContext.Session.GetString("Player1Address");
            if (SessionAddress != null)
            {
                hand1 = JsonConvert.DeserializeObject<List<Cards>>(SessionAddress);
            }
            if(PlayerAddress != null)
            {
                player1 = JsonConvert.DeserializeObject<Player>(PlayerAddress);
            }
            else
            {
                player1 = new Player();
                hand1 = new List<Cards>();
                deck.builddeck();
            }
        }
        public IActionResult OnPost()
        {
            OnGet();
            deck.getnextcard();
            player1.hit();
            hand1.Add(player1.GetCard());
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString("Hand1Address",
                JsonConvert.SerializeObject(hand1));
                HttpContext.Session.SetString("Player1Address",
                JsonConvert.SerializeObject(player1));
            }
            return Page();
        }
    }
}
