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
        public string name;
        Random rnd = new Random();
        public int dc, tries,whostarts;
        public string cheatmessage;
        int balance;
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
            var dcAddress = HttpContext.Session.GetString("diffuculty");
            var triesAddress = HttpContext.Session.GetString("tries");
            var nameAddress = HttpContext.Session.GetString("name");
            var moneyAddress = HttpContext.Session.GetString("money");

            deck = JsonConvert.DeserializeObject<Deck>(DeckAddress);
            hand1 = JsonConvert.DeserializeObject<List<Cards>>(SessionAddress);
            player1 = JsonConvert.DeserializeObject<Player>(PlayerAddress);              
            hand2 = JsonConvert.DeserializeObject<List<Cards>>(SessionAddress2);
            player2 = JsonConvert.DeserializeObject<Player>(PlayerAddress2);
            opfirstcard = JsonConvert.DeserializeObject<string>(opcardAddress);
            dc = JsonConvert.DeserializeObject<int>(dcAddress);
            tries = JsonConvert.DeserializeObject<int>(triesAddress);
            name = JsonConvert.DeserializeObject<string>(nameAddress);
            balance = JsonConvert.DeserializeObject<int>(moneyAddress);
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
            if(action == "peek")
            {
                int roll;
                roll = rnd.Next(0, 20);
                if(roll >=dc || tries == -1)
                {
                    hand2.ElementAt(0).imagepath = opfirstcard;
                    cheatmessage = "Success, opponents card has been revealed";                   
                    if(tries == -1)
                    {
                        cheatmessage = "You already succeeded in this action this round";
                    }
                    tries = -1;
                }
                else if(roll <dc)
                {
                    tries++;
                    dc++;
                    cheatmessage = "Opponent: Hey, what do you think you're doing? Don't try that again";
                }
                if(tries ==2)
                {
                    serializeMyShit();
                    return RedirectToPage("Result");
                }
            }
            if(action == "back")
            {
                return RedirectToPage("Index");
            }
            if (ModelState.IsValid)
            {
                serializeMyShit();
            }
            return Page();
        }
    }
}
