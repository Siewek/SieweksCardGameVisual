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
        string opfirstcard;
        Random rnd = new Random();
        public void serializeMyShit()
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

        public void Player2AI()
        {
            if(player2.value <=17)
            {
                deck.getnextcard();
                player2.hit();
                hand2.Add(player2.GetCard());
            }
        }
        public void OnGet()
        {
            var SessionAddress = HttpContext.Session.GetString("Hand1Address");
            var PlayerAddress = HttpContext.Session.GetString("Player1Address");
            var SessionAddress2 = HttpContext.Session.GetString("Hand2Address");
            var PlayerAddress2 = HttpContext.Session.GetString("Player2Address");
            var DeckAddress = HttpContext.Session.GetString("deckAddress");
            var opcardAddress = HttpContext.Session.GetString("opponentshiddencard");

            deck = JsonConvert.DeserializeObject<Deck>(DeckAddress);
            hand1 = JsonConvert.DeserializeObject<List<Cards>>(SessionAddress);
            player1 = JsonConvert.DeserializeObject<Player>(PlayerAddress);              
            hand2 = JsonConvert.DeserializeObject<List<Cards>>(SessionAddress2);
            player2 = JsonConvert.DeserializeObject<Player>(PlayerAddress2);
            opfirstcard = JsonConvert.DeserializeObject<string>(opcardAddress);
      
        }
        public IActionResult OnPost(string action)
        {
            OnGet();
            if (action == "hit")
            {
              
                deck.getnextcard();
                player1.hit();
                hand1.Add(player1.GetCard());
                if (player1.value > 21)
                {
                    if (ModelState.IsValid)
                    {
                        serializeMyShit();
                    }
                    return RedirectToPage("Result");
                }
                Player2AI();
                if(player2.value>21)
                {
                    if (ModelState.IsValid)
                    {
                        serializeMyShit();
                    }
                    return RedirectToPage("Result");
                }
            }
            if(action == "fold")
            {
                while(player2.value <= 17)
                {
                    deck.getnextcard();
                    player2.hit();
                    hand2.Add(player2.GetCard());
                }
                    if (ModelState.IsValid)
                    {
                        serializeMyShit();
                    }
                    return RedirectToPage("Result");
            }
            if (ModelState.IsValid)
            {
                serializeMyShit();
            }
            return Page();
        }
    }
}
