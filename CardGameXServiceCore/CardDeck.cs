using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CardGameXServiceCore
{
    [DataContract]
    public class CardDeck : Card
    {
        private const int DeckSize = 32;
        private List<Card> cardDeck;

        public CardDeck()
        {
            cardDeck = new List<Card>();
            SetUpDeck();
            ShuffleDeck();
        }

        [DataMember]
        public List<Card> Deck
        {
            get { return cardDeck; }
        }

       public void SetUpDeck()
        {
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    cardDeck.Add(new Card()
                    {
                        CardSuit = suit,
                        CardRank = rank,
                        SuitImage = "/Content/CardFaces/" + suit + rank + ".png"
                        //suit.ToString() + rank.ToString()))
                    });

                }
            }
        }

        public void ShuffleDeck()
        {
            Random rng = new Random();
            int n = cardDeck.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = cardDeck[k];
                cardDeck[k] = cardDeck[n];
                cardDeck[n] = value;
            }
        }
        

    }
}
