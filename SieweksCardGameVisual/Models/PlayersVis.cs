using SieweksCardGameVisual.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieweksCardGameVisual.Models
{
    public class Playersvis
    {
        public IList<Cards> hand1 { get; set; }
        public IList<Cards> hand2 { get; set; }
    }
}
