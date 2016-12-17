using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CardGameXServiceCore
{
    public class GameTable
    {
        List<Player> players;
        CardDeck cardDeck;
        List<Card> talon;
        List<Bid> bids;


        [DataMember]
        public bool IsBiddingTime { get; set; }

        public GameTable()
        {
            Players = new List<Player>();
            cardDeck = new CardDeck();
            talon = new List<Card>();
        }

        [DataMember]
        public List<Player> Players { get; set; }

        [DataMember]
        public List<Card> Talon { get { return talon; } set { } }

      public void DealDeck(List<Player> players)
        {
            while (cardDeck.Deck.Count > 2)
            {
                foreach (var player in players)
                {
                    player.PlayerHand.Add(cardDeck.Deck[cardDeck.Deck.Count - 1]);
                    cardDeck.Deck.RemoveAt(cardDeck.Deck.Count - 1);
                }
            }

            while (cardDeck.Deck.Count != 0)
            {
                talon.Add(cardDeck.Deck[cardDeck.Deck.Count - 1]);
                cardDeck.Deck.RemoveAt(cardDeck.Deck.Count - 1);
            }
        }

        public Player EndTurn(List<Player> players)
        {
            Player player = players.OrderByDescending(p => p.ThrownCard.CardRank).FirstOrDefault();
            player.NumberOfBooks += 1;
            players.ForEach(p => p.ThrownCard = null);
            return player;    
        }
    }
}
