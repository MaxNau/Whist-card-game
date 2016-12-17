using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CardGameX.Models
{
    public class Card
    {
        public enum Suit
        {
            Spades,
            Hearts,
            Diamonds,
            Clubs
        }

        public enum Rank
        {
            Seven = 7, Eight, Nine, Ten,
            Jack, Queen, King, Ace
        }

        public Suit CardSuit { get; set; }
        public Rank CardRank { get; set; }
        public string SuitImage { get; set; }
    }
}
