using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Windows.Media.Imaging;

namespace CardGameXServiceCore
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(CallbackContract = typeof(ICardGameServiceCallback))]
    public interface ICardGameService
    {
        [OperationContract]
        string Subscribe();

        [OperationContract(IsOneWay = true)]
        void Unsubscribe(string clientId);

        [OperationContract(IsOneWay = true)]
        void SendMessage(string clientId, string value);

        [OperationContract]
        ObservableCollection<string> GetPlayerHand(string pName);

        [OperationContract]
        ObservableCollection<string> GetTalon();

        [OperationContract]
        Player.PlayerTablePosition GetPlayerPosition(string pName);

        [OperationContract]
        string[] GetPlayerNames();

        [OperationContract(IsOneWay = true)]
        void GetUri(string clientId, string value);

        [OperationContract(IsOneWay = true)]
        void ThrowCard(string clientId, string cardUri);

        [OperationContract]
        bool CheckPlayerCurrentTurn(string pName);

        [OperationContract]
        ObservableCollection<Bid> GetPlayerAvaliableBids(string pName);

        [OperationContract(IsOneWay = true)]
        void Bidd(string pName, string bid);
        // TODO: Add your service operations here
    }

    [ServiceContract]
    public interface ICardGameServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void HandleMessage(string value);

        [OperationContract(IsOneWay = true)]
        void HandleUri(string uri);

        [OperationContract]
        void Connected(int i);

        [OperationContract(IsOneWay = true)]
        void HandleCardUri(Player.PlayerTablePosition playerPosition, string cardUri);

        [OperationContract(IsOneWay = true)]
        void TurnEnds(Player.PlayerTablePosition bookedPlayerPosition, int numberOfBooks);

        [OperationContract(IsOneWay = true)]
        void PlayerBids(Bid.BidName bid, ObservableCollection<Bid> bids, Player.PlayerTablePosition position);
    }


}
