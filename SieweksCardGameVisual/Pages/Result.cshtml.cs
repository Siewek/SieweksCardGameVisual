using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SieweksCardGameVisual.Classes;

namespace SieweksCardGameVisual.Pages
{
    public class ResultModel : PageModel
    {
        Deck deck;
        public Player player1;
        public List<Cards> hand1;
        public Player player2;
        public List<Cards> hand2;
        public string Message;
        string opfirstcard;
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

            hand2.ElementAt(0).imagepath = opfirstcard;
            if (player1.value > player2.value && player1.value <= 21 || player2.value > 21)
            {
                Message = "You Win";
            }
            else if (player1.value < player2.value && player2.value <= 21 || player1.value > 21)
            {
                Message = "You Lose";
            }
            else Message = "It's a Tie";
        }
    }
}
