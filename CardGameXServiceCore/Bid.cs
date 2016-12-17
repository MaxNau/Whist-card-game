using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CardGameXServiceCore
{
    public class Bid
    {
        public BidName Bid_ { get; set; }
        public bool CanBid { get; set; }

        [DataContract(Name = "BidName")]
        public enum BidName
        {
            [EnumMember]
            S7,
            [EnumMember]
            C7,
            [EnumMember]
            D7,
            [EnumMember]
            H7,
            [EnumMember]
            NT7,
            [EnumMember]
            S8,
            [EnumMember]
            C8,
            [EnumMember]
            D8,
            [EnumMember]
            H8,
            [EnumMember]
            NT8,
            [EnumMember]
            S9,
            [EnumMember]
            C9,
            [EnumMember]
            D9,
            [EnumMember]
            H9,
            [EnumMember]
            NT9,
            [EnumMember]
            S10,
            [EnumMember]
            C10,
            [EnumMember]
            D10,
            [EnumMember]
            H10,
            [EnumMember]
            NT10
        }
    }
}
