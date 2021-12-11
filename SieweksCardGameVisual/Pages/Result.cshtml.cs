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
        public string Message,name,Message2;
        string opfirstcard;
        public int dc, tries, balance, whostarts;
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
            HttpContext.Session.SetString("diffuculty",
              JsonConvert.SerializeObject(dc));
            HttpContext.Session.SetString("tries",
           JsonConvert.SerializeObject(tries));
            HttpContext.Session.SetString("name",
              JsonConvert.SerializeObject(name));
            HttpContext.Session.SetString("money",
            JsonConvert.SerializeObject(balance));
        }
        public void OnGet()
        {
            var SessionAddress = HttpContext.Session.GetString("Hand1Address");
            var PlayerAddress = HttpContext.Session.GetString("Player1Address");
            var SessionAddress2 = HttpContext.Session.GetString("Hand2Address");
            var PlayerAddress2 = HttpContext.Session.GetString("Player2Address");
            var DeckAddress = HttpContext.Session.GetString("deckAddress");
            var opcardAddress = HttpContext.Session.GetString("opponentshiddencard");
            var dcAddress = HttpContext.Session.GetString("diffuculty");
            var triesAddress = HttpContext.Session.GetString("tries");
            var moneyAddress = HttpContext.Session.GetString("money");
            var nameAddress = HttpContext.Session.GetString("name");

            deck = JsonConvert.DeserializeObject<Deck>(DeckAddress);
            hand1 = JsonConvert.DeserializeObject<List<Cards>>(SessionAddress);
            player1 = JsonConvert.DeserializeObject<Player>(PlayerAddress);
            hand2 = JsonConvert.DeserializeObject<List<Cards>>(SessionAddress2);
            player2 = JsonConvert.DeserializeObject<Player>(PlayerAddress2);
            opfirstcard = JsonConvert.DeserializeObject<string>(opcardAddress);
            dc = JsonConvert.DeserializeObject<int>(dcAddress);
            tries = JsonConvert.DeserializeObject<int>(triesAddress);
            balance = JsonConvert.DeserializeObject<int>(moneyAddress);
            name = JsonConvert.DeserializeObject<string>(nameAddress);

            hand2.ElementAt(0).imagepath = opfirstcard;
            if(tries == 2)
            {
                Message = "You got caught cheating and You Lose";
                balance -= 100;
            }
            else if (player1.value > player2.value && player1.value <= 21 || player2.value > 21)
            {
                Message = "You Win";
                balance += 100;
            }
            else if (player1.value < player2.value && player2.value <= 21 || player1.value > 21)
            {
                Message = "You Lose";
                balance -= 100;
            }
            else Message = "It's a Tie";
        }
        public IActionResult OnPost(string action)
        {
            OnGet();
            if(action == "back")
            {
                return RedirectToPage("Index");
            }

            if (action == "quick")
            {
                if (balance > 0)
                {
                    Random rnd = new Random();
                    whostarts = rnd.Next(2);
                    player1 = new Player();
                    hand1 = new List<Cards>();
                    player2 = new Player();
                    hand2 = new List<Cards>();
                    dc = 10;
                    tries = 0;
                    deck.builddeck();


                    deck.getnextcard();
                    player1.hit();
                    hand1.Add(player1.GetCard());


                    deck.getnextcard();
                    player2.hit();
                    hand2.Add(player2.GetCard());
                    opfirstcard = hand2.ElementAt(0).imagepath;
                    if (whostarts == 1)
                    {
                        deck.getnextcard();
                        player2.hit();
                        hand2.Add(player2.GetCard());
                    }
                    hand2.ElementAt(0).imagepath = "/images/red_joker.png";
                    serializeMyShit();
                    return RedirectToPage("BlackJack");
                }
                else
                {
                    Message2 = "You can't restart if you have no money";
                    balance = 0;
                }

            }
            if (ModelState.IsValid)
            {
                serializeMyShit();
            }
            return Page();
        }
    }
}
