using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CardGameXServiceCore
{
    [DataContract]
    public class Card
    {
        [DataContract(Name ="CardSuit")]
        public enum Suit
        {
            [EnumMember]
            Spades,
            [EnumMember]
            Hearts,
            [EnumMember]
            Diamonds,
            [EnumMember]
            Clubs
        }

        [DataContract(Name ="CardRank")]
        public enum Rank
        {
            [EnumMember]
            Seven,
            [EnumMember]
            Eight,
            [EnumMember]
            Nine,
            [EnumMember]
            Ten,
            [EnumMember]
            Jack,
            [EnumMember]
            Queen,
            [EnumMember]
            King,
            [EnumMember]
            Ace
        }

        [DataMember]
        public Suit CardSuit { get; set; }

        [DataMember]
        public Rank CardRank { get; set; }

        [DataMember]
        public string SuitImage { get; set; }
    }
}
