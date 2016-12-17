using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CardGameXServiceCore
{
    [DataContract]
    public class Player
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public ObservableCollection<Card> PlayerHand { get; set; }

        [DataMember]
        public Card ThrownCard { get; set; }

        [DataMember]
        public int NumberOfBooks { get; set; }

        [DataMember]
        public PlayerTablePosition TablePosition { get; set; }

        [DataMember]
        public bool IsCurrentTurn { get; set; }

        [DataMember]
        public bool IsDealer { get; set; }

        [DataMember]
        public bool Bided { get; set; }

        [DataMember]
        public bool Overbided { get; set; }

        [DataMember]
        public bool Passed { get; set; }

        [DataMember]
        public ObservableCollection<Bid> Bids { get; set; }

        [DataMember]
        public Bid.BidName Bid { get; set; }

        public Player()
        {
            PlayerHand = new ObservableCollection<Card>();
            Bids = new ObservableCollection<Bid>();

            foreach (Bid.BidName bid in Enum.GetValues(typeof(Bid.BidName)))
            {
                Bids.Add(new Bid() { Bid_ = bid, CanBid = true });
            }
        }

        [DataContract(Name = "PlayerTablePosition")]
        public enum PlayerTablePosition
        {
            [EnumMember]
            South,
            [EnumMember]
            West,
            [EnumMember]
            East,
            [EnumMember]
            Notrh
        }
    }
}
