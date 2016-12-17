using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CardGameX.Models
{
    public class CardDeck : Card
    {
        private const int DeckSize = 32;
        private Card[] cardDeck;

        public CardDeck()
        {
            cardDeck = new Card[DeckSize];
        }

        public Card[] Deck
        {
            get { return cardDeck; }
        }

        public void SetUpDeck()
        {
            int i = 0;
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    cardDeck[i] = new Card()
                    {
                        CardSuit = suit,
                        CardRank = rank,
                       // SuitImage = new BitmapImage(
                       // new Uri("C:/Users/Rionis/Documents/visual studio 2015/Projects/CardGame/CardGame/Content/CardFaces/" +
                        //suit.ToString() + rank.ToString()))
                    };
                    i++;
                }
            }
        }
    }
}
