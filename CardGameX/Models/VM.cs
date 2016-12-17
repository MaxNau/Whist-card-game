using CardGameX.CardGameXService;
using CardGameX.CardGameXServiceCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Windows.Media.Imaging;

namespace CardGameX.Models
{
    public class VM : INotifyPropertyChanged
    {

        private Guid clientId;
        private string clientId2;
        private ChatServiceClient client;
        private InstanceContext instanceContext;
        private InstanceContext instanceContext2;
        private CardGameServiceClient client2;
        int i;
        private PlayerTablePosition tablePosition;

        // private CardGameServiceClient client2;
        // string clientId2;
        ObservableCollection<BitmapImage> currentPlayerHand;
        ObservableCollection<BitmapImage> westPlayerHand;
        ObservableCollection<BitmapImage> eastPlayerHand;
        ObservableCollection<BitmapImage> northPlayerHand;
        ObservableCollection<BitmapImage> talon;

        private int eastPlayerNumberOfBooks;
        private int westPlayerNumberOfBooks;
        private int southPlayerNumberOfBooks;

        private bool playerHandEnabled;

        public bool IsBiddingTime { get; set; }

        public bool PlayerHandEnabled
        {
            get { return playerHandEnabled; }
            set
            {
                if (value != playerHandEnabled)
                {
                    playerHandEnabled = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string currentPlayerBid;

        public string CurrentPlayerBid
        {
            get { return currentPlayerBid; }
            set
            {
                if (value != currentPlayerBid)
                {
                    currentPlayerBid = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int EastPlayerNumberOfBooks { get { return eastPlayerNumberOfBooks; }
            set
            {
                if (value != eastPlayerNumberOfBooks)
                {
                    eastPlayerNumberOfBooks = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int SouthPlayerNumberOfBooks
        {
            get { return southPlayerNumberOfBooks; }
            set
            {
                if (value != southPlayerNumberOfBooks)
                {
                    southPlayerNumberOfBooks = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int WestPlayerNumberOfBooks
        {
            get { return westPlayerNumberOfBooks; }
            set
            {
                if (value != westPlayerNumberOfBooks)
                {
                    westPlayerNumberOfBooks = value;
                    RaisePropertyChanged();
                }
            }
        }

        private ObservableCollection<Bid> currentPlayerAvaliableBids;
        public ObservableCollection<Bid> CurrentPlayerAvliableBids
        {
            get { return currentPlayerAvaliableBids; }
            set
            {
                if (value != currentPlayerAvaliableBids)
                {
                    currentPlayerAvaliableBids = value;
                    RaisePropertyChanged();
                }
            }
        }

        private ObservableCollection<Bid> westPlayerAvaliableBids;
        public ObservableCollection<Bid> WestPlayerAvliableBids
        {
            get { return westPlayerAvaliableBids; }
            set
            {
                if (value != westPlayerAvaliableBids)
                {
                    westPlayerAvaliableBids = value;
                    RaisePropertyChanged();
                }
            }
        }

        private ObservableCollection<Bid> southPlayerAvaliableBids;
        public ObservableCollection<Bid> SouthPlayerAvliableBids
        {
            get { return southPlayerAvaliableBids; }
            set
            {
                if (value != southPlayerAvaliableBids)
                {
                    southPlayerAvaliableBids = value;
                    RaisePropertyChanged();
                }
            }
        }

        public VM()
        {
            //Items = new ObservableCollection<Bid>();

            ChatServiceCallback chatServiceCallback = new ChatServiceCallback();
            CardGameServiceCallback callback = new CardGameServiceCallback();
            

            chatServiceCallback.ClientNotified += ChatServiceCallback_ClientNotified;
            callback.ClientNotified += Callback_ClientNotified;
            callback.ThrowCardClientsNotified += Callback_ThrowCardClientsNotified;
            callback.turnEnds += Callback_turnEnds;
            callback.playerBids += Callback_playerBids;

            instanceContext = new InstanceContext(chatServiceCallback);
            instanceContext2 = new InstanceContext(callback);

            client = new ChatServiceClient(instanceContext);
            client2 = new CardGameServiceClient(instanceContext2);

            try
            {
                clientId = client.Subscribe();
                clientId2 = client2.Subscribe();
            }
            catch
            {
            }

            currentPlayerHand = new ObservableCollection<BitmapImage>();
            westPlayerHand = new ObservableCollection<BitmapImage>();
            eastPlayerHand = new ObservableCollection<BitmapImage>();
            northPlayerHand = new ObservableCollection<BitmapImage>();
            talon = new ObservableCollection<BitmapImage>();

            CurrentPlayerAvliableBids = new ObservableCollection<CardGameXServiceCore.Bid>();

            Do();

        }

        private void Callback_playerBids(object sender, PlayerBidsCallbackEventArgs e)
        {
            CurrentPlayerBid = e.Bid.ToString();
            //Do();
            /*if (tablePosition == PlayerTablePosition.South)
            {
                if (e.Position == PlayerTablePosition.West)
                {
                    WestPlayerAvliableBids = e.Bids;
                }
                else if (e.Position == PlayerTablePosition.East)
                {
                    CurrentPlayerAvliableBids = e.Bids;
                }
                else if (e.Position == PlayerTablePosition.South)
                {
                    SouthPlayerAvliableBids = e.Bids;
                }
            }
            else if (tablePosition == PlayerTablePosition.West)
            {
                if (e.Position == PlayerTablePosition.East)
                {
                   WestPlayerAvliableBids = e.Bids;
                }
                else if (e.Position == PlayerTablePosition.South)
                {
                    CurrentPlayerAvliableBids = e.Bids;
                }
                else if (e.Position == PlayerTablePosition.South)
                {
                    SouthPlayerAvliableBids = e.Bids;
                }
            }
            else if (tablePosition == PlayerTablePosition.East)
            {
                if (e.Position == PlayerTablePosition.South)
                {
                    WestPlayerAvliableBids = e.Bids;
                }
                else if (e.Position == PlayerTablePosition.West)
                {
                    CurrentPlayerAvliableBids = e.Bids;
                }
                else if (e.Position == PlayerTablePosition.East)
                {
                    SouthPlayerAvliableBids = e.Bids;
                }
            }*/
            update();
        }

        private async  void Do()
        {
            CurrentPlayerAvliableBids = new ObservableCollection<Bid>(await client2.GetPlayerAvaliableBidsAsync(clientId2));
        }

        private void update()
        {
            int i = 0;
            foreach (Bid b in client2.GetPlayerAvaliableBids(clientId2))
            {
                CurrentPlayerAvliableBids[i++].CanBid = b.CanBid;
            }
        }


        private async void Callback_turnEnds(object sender, TurnEndsCallbackEventArgs e)
        {

            PlayerHandEnabled = await client2.CheckPlayerCurrentTurnAsync(clientId2);

            if (tablePosition == PlayerTablePosition.South)
            {
                if (e.BookedPlayerPosition == PlayerTablePosition.West)
                {
                    WestPlayerNumberOfBooks = e.NumberOfBooks;
                }
                else if (e.BookedPlayerPosition == PlayerTablePosition.East)
                {
                    EastPlayerNumberOfBooks = e.NumberOfBooks;
                }
                else if (e.BookedPlayerPosition == PlayerTablePosition.South)
                {
                    SouthPlayerNumberOfBooks = e.NumberOfBooks;
                }
            }
            else if (tablePosition == PlayerTablePosition.West)
            {
                if (e.BookedPlayerPosition == PlayerTablePosition.East)
                {
                    WestPlayerNumberOfBooks = e.NumberOfBooks;
                }
                else if (e.BookedPlayerPosition == PlayerTablePosition.South)
                {
                    EastPlayerNumberOfBooks = e.NumberOfBooks;
                }
                else if (e.BookedPlayerPosition == PlayerTablePosition.West)
                {
                    SouthPlayerNumberOfBooks = e.NumberOfBooks;
                }
            }
            else if (tablePosition == PlayerTablePosition.East)
            {
                if (e.BookedPlayerPosition == PlayerTablePosition.South)
                {
                    WestPlayerNumberOfBooks = e.NumberOfBooks;
                }
                else if (e.BookedPlayerPosition == PlayerTablePosition.West)
                {
                    EastPlayerNumberOfBooks = e.NumberOfBooks; 
                }
                else if (e.BookedPlayerPosition == PlayerTablePosition.East)
                {
                    SouthPlayerNumberOfBooks = e.NumberOfBooks;
                }
            }

            System.Threading.Thread.Sleep(2000);

            EastPlayerCard = null;
            WestPlayerCard = null;
            CurrentPlayerCard = null;
        }

        private async void Callback_ThrowCardClientsNotified(object sender, ThrowCardCallbackNotifiedEventArgs e)
        {
            PlayerHandEnabled = await client2.CheckPlayerCurrentTurnAsync(clientId2);

            BitmapImage thrownCard;

            if (tablePosition == PlayerTablePosition.West)
            {
                if (e.PlayerPosition == PlayerTablePosition.South)
                {
                    thrownCard = EastPlayerHand.FirstOrDefault(c => c.UriSource.ToString() == e.CardUri);
                    EastPlayerCard = thrownCard;
                    EastPlayerHand.Remove(thrownCard); 
                }
                else if (e.PlayerPosition == PlayerTablePosition.East)
                {
                    thrownCard = WestPlayerHand.FirstOrDefault(c => c.UriSource.ToString() == e.CardUri);
                    WestPlayerCard = thrownCard;
                    WestPlayerHand.Remove(thrownCard);
                }
            }
            else if (tablePosition == PlayerTablePosition.East)
            {
                if (e.PlayerPosition == PlayerTablePosition.South)
                {
                    thrownCard = WestPlayerHand.FirstOrDefault(c => c.UriSource.ToString() == e.CardUri);
                    WestPlayerCard = thrownCard;
                    WestPlayerHand.Remove(thrownCard);
                }
                else if (e.PlayerPosition == PlayerTablePosition.West)
                {
                    thrownCard = EastPlayerHand.FirstOrDefault(c => c.UriSource.ToString() == e.CardUri);
                    EastPlayerCard = thrownCard;
                    EastPlayerHand.Remove(thrownCard);
                }
            }
            else if (tablePosition == PlayerTablePosition.South)
            {
                if (e.PlayerPosition == PlayerTablePosition.East)
                {
                    thrownCard = EastPlayerHand.FirstOrDefault(c => c.UriSource.ToString() == e.CardUri);
                    EastPlayerCard = thrownCard;
                    EastPlayerHand.Remove(thrownCard);
                }
                else if (e.PlayerPosition == PlayerTablePosition.West)
                {
                    thrownCard = WestPlayerHand.FirstOrDefault(c => c.UriSource.ToString() == e.CardUri);
                    WestPlayerCard = thrownCard;
                    WestPlayerHand.Remove(thrownCard);
                }
            }
            //NorthPlayerCard = new BitmapImage(new Uri(e.CardUri, UriKind.RelativeOrAbsolute));

        }

       /* private void Callback_ThrowCard(object sender, ClientCallbackNotifiedEventArgs e)
        {
            Image = new BitmapImage(new Uri(e.Uri, UriKind.RelativeOrAbsolute));
            Image1 = new BitmapImage(new Uri(e.Uri, UriKind.RelativeOrAbsolute));
            Image2 = new BitmapImage(new Uri(e.Uri, UriKind.RelativeOrAbsolute));
            Image3 = new BitmapImage(new Uri(e.Uri, UriKind.RelativeOrAbsolute));
        }*/

        private async void Callback_ClientNotified(object sender, ClientCallbackNotifiedEventArgs e)
        {
            //throw new NotImplementedException();
            SwitchView = e.I;
            if (e.I == 3)
            {
                List<string> playerNames = new List<string>(client2.GetPlayerNames());

                string[] hand = client2.GetPlayerHand(clientId2);
                foreach (string uri in hand)
                {
                    currentPlayerHand.Add(new BitmapImage(new Uri(@uri, UriKind.RelativeOrAbsolute)));
                }

                playerNames.Remove(clientId2);
                tablePosition = client2.GetPlayerPosition(clientId2);

                hand = client2.GetPlayerHand(playerNames[0]);
                if (client2.GetPlayerPosition(clientId2) == PlayerTablePosition.South)
                {
                    if (client2.GetPlayerPosition(playerNames[0]) == PlayerTablePosition.West)
                    {
                        foreach (string uri in hand)
                        {
                            westPlayerHand.Add(new BitmapImage(new Uri(@uri, UriKind.RelativeOrAbsolute)));
                        }
                    }
                    else if (client2.GetPlayerPosition(playerNames[0]) == PlayerTablePosition.East)
                    {
                        foreach (string uri in hand)
                        {
                            eastPlayerHand.Add(new BitmapImage(new Uri(@uri, UriKind.RelativeOrAbsolute)));
                        }
                    }
                }
                else if (client2.GetPlayerPosition(clientId2) == PlayerTablePosition.West)
                {
                    if (client2.GetPlayerPosition(playerNames[0]) == PlayerTablePosition.East)
                    {
                        foreach (string uri in hand)
                        {
                            westPlayerHand.Add(new BitmapImage(new Uri(@uri, UriKind.RelativeOrAbsolute)));
                        }
                    }
                    else if (client2.GetPlayerPosition(playerNames[0]) == PlayerTablePosition.South)
                    {
                        foreach (string uri in hand)
                        {
                            eastPlayerHand.Add(new BitmapImage(new Uri(@uri, UriKind.RelativeOrAbsolute)));
                        }
                    }
                }
                else if (client2.GetPlayerPosition(clientId2) == PlayerTablePosition.East)
                {
                    if (client2.GetPlayerPosition(playerNames[0]) == PlayerTablePosition.South)
                    {
                        foreach (string uri in hand)
                        {
                            westPlayerHand.Add(new BitmapImage(new Uri(@uri, UriKind.RelativeOrAbsolute)));
                        }
                    }
                    else if (client2.GetPlayerPosition(playerNames[0]) == PlayerTablePosition.West)
                    {
                        foreach (string uri in hand)
                        {
                            eastPlayerHand.Add(new BitmapImage(new Uri(@uri, UriKind.RelativeOrAbsolute)));
                        }
                    }
                }

                playerNames.Remove(playerNames[0]);

                hand = client2.GetPlayerHand(playerNames[0]);
                if (client2.GetPlayerPosition(clientId2) == PlayerTablePosition.South)
                {
                    if (client2.GetPlayerPosition(playerNames[0]) == PlayerTablePosition.West)
                    {
                        foreach (string uri in hand)
                        {
                            westPlayerHand.Add(new BitmapImage(new Uri(@uri, UriKind.RelativeOrAbsolute)));
                        }
                    }
                    else if (client2.GetPlayerPosition(playerNames[0]) == PlayerTablePosition.East)
                    {
                        foreach (string uri in hand)
                        {
                            eastPlayerHand.Add(new BitmapImage(new Uri(@uri, UriKind.RelativeOrAbsolute)));
                        }
                    }
                }
                else if (client2.GetPlayerPosition(clientId2) == PlayerTablePosition.West)
                {
                    if (client2.GetPlayerPosition(playerNames[0]) == PlayerTablePosition.East)
                    {
                        foreach (string uri in hand)
                        {
                            westPlayerHand.Add(new BitmapImage(new Uri(@uri, UriKind.RelativeOrAbsolute)));
                        }
                    }
                    else if (client2.GetPlayerPosition(playerNames[0]) == PlayerTablePosition.South)
                    {
                        foreach (string uri in hand)
                        {
                            eastPlayerHand.Add(new BitmapImage(new Uri(@uri, UriKind.RelativeOrAbsolute)));
                        }
                    }
                }
                else if (client2.GetPlayerPosition(clientId2) == PlayerTablePosition.East)
                {
                    if (client2.GetPlayerPosition(playerNames[0]) == PlayerTablePosition.South)
                    {
                        foreach (string uri in hand)
                        {
                            westPlayerHand.Add(new BitmapImage(new Uri(@uri, UriKind.RelativeOrAbsolute)));
                        }
                    }
                    else if (client2.GetPlayerPosition(playerNames[0]) == PlayerTablePosition.West)
                    {
                        foreach (string uri in hand)
                        {
                            eastPlayerHand.Add(new BitmapImage(new Uri(@uri, UriKind.RelativeOrAbsolute)));
                        }
                    }
                }

                PlayerHandEnabled = await client2.CheckPlayerCurrentTurnAsync(clientId2);

                string[] tal = client2.GetTalon();

                foreach (string uri in tal)
                {
                    talon.Add(new BitmapImage(new Uri(@uri, UriKind.RelativeOrAbsolute)));
                }
            }
        }

        private void ChatServiceCallback_ClientNotified(object sender, ClientNotifiedEventArgs e)
        {
            // throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

        public ObservableCollection<Player> Players { get; set; }

        public int SwitchView
        {
            get { return i; }
            set
            {
                if (value != i)
                {
                    i = value;
                    RaisePropertyChanged();
                }
            }
        }

        public void ThrowCard(BitmapImage cardImage)
        {
            //NorthPlayerCard = new BitmapImage(new Uri("/Content/CardFaces/DiamondsSeven.png", UriKind.RelativeOrAbsolute)); 
            // WestPlayerCard = new BitmapImage(new Uri("/Content/CardFaces/DiamondsSeven.png", UriKind.RelativeOrAbsolute));
            // EastPlayerCard = new BitmapImage(new Uri("/Content/CardFaces/DiamondsSeven.png", UriKind.RelativeOrAbsolute));

            if (PlayerHandEnabled == true)
            {
                CurrentPlayerCard = cardImage;
                currentPlayerHand.Remove(cardImage);
                client2.ThrowCard(clientId2, cardImage.UriSource.ToString());
            }
        }

        public void Bid(string bid)
        {
            client2.Bidd(clientId2, bid);
           // update();
        }

        private BitmapImage northPlayerCard;
        public BitmapImage NorthPlayerCard
        {
            get { return northPlayerCard; }
            set
            {
                if (value != northPlayerCard)
                {
                    northPlayerCard = value;
                    RaisePropertyChanged();
                }
            }
        }

        private BitmapImage westPlayerCard;
        public BitmapImage WestPlayerCard
        {
            get { return westPlayerCard; }
            set
            {
                if (value != westPlayerCard)
                {
                    westPlayerCard = value;
                    RaisePropertyChanged();
                }
            }
        }

        private BitmapImage eastPlayerCard;
        public BitmapImage EastPlayerCard
        {
            get { return eastPlayerCard; }
            set
            {
                if (value != eastPlayerCard)
                {
                    eastPlayerCard = value;
                    RaisePropertyChanged();
                }
            }
        }

        private BitmapImage currentPlayerCard;
        public BitmapImage CurrentPlayerCard
        {
            get { return currentPlayerCard; }
            set
            {
                if (value != currentPlayerCard)
                {
                    currentPlayerCard = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<BitmapImage> CurrentPlayerHand {
            get
            {
                return currentPlayerHand;
            }
                set
            {
                if (value != currentPlayerHand)
                {
                    CurrentPlayerHand = value;
                }
            }
        }

        public ObservableCollection<BitmapImage> WestPlayerHand
        {
            get
            {
                return westPlayerHand;
            }
            set
            {
            }
        }

        public ObservableCollection<BitmapImage> EastPlayerHand
        {
            get
            {
                return eastPlayerHand;
            }
            set
            {
            }
        }

        public ObservableCollection<BitmapImage> Talon
        {
            get { return talon; }
            set { }
        }
    }


    public class ChatServiceCallback : IChatServiceCallback
    {
        public delegate void ClientNotifiedEventHandler(object sender, ClientNotifiedEventArgs e);
        public event ClientNotifiedEventHandler ClientNotified;

        public void HandleMessage(string value)
        {
            if (ClientNotified != null)
            {
                ClientNotified(this, new ClientNotifiedEventArgs(value));
            }
        }
    }

    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CardGameServiceCallback : ICardGameServiceCallback
    {
        public delegate void ClientNotifiedEventHandler(object sender, ClientCallbackNotifiedEventArgs e);
        public event ClientNotifiedEventHandler ClientNotified;

        public delegate void ThrowCardEventHandler(object sender, ThrowCardCallbackNotifiedEventArgs e);
        public event ThrowCardEventHandler ThrowCardClientsNotified;

        public delegate void TurnEndsEventHandler(object sender, TurnEndsCallbackEventArgs e);
        public event TurnEndsEventHandler turnEnds;

        public delegate void PlayerBidsEventHandler(object sender, PlayerBidsCallbackEventArgs e);
        public event PlayerBidsEventHandler playerBids;

        public void HandleMessage(string value)
        {
            if (ClientNotified != null)
            {
                ClientNotified(this, new ClientCallbackNotifiedEventArgs(value));
            }
        }

        public void HandleUri(string uri)
        {
            if (ClientNotified != null)
            {
                ClientNotified(this, new ClientCallbackNotifiedEventArgs(uri));
            }
        }

        public void Connected(int i)
        {
            if (ClientNotified != null)
            {
                ClientNotified(this, new ClientCallbackNotifiedEventArgs(i));
            }
        }

        public void HandleCardUri(PlayerTablePosition playerPosition, string cardUri)
        {
            if (ThrowCardClientsNotified != null)
            {
                ThrowCardClientsNotified(this, new ThrowCardCallbackNotifiedEventArgs(playerPosition, cardUri));
            }
        }

        public void TurnEnds(PlayerTablePosition bookedPlayerPosition, int numberOfBooks)
        {
            if (turnEnds != null)
            {
                turnEnds(this, new TurnEndsCallbackEventArgs(bookedPlayerPosition, numberOfBooks));
            }
        }

        public void PlayerBids(BidName bid, Bid[] bids, PlayerTablePosition position)
        {
            if (playerBids != null)
            {
                playerBids(this, new PlayerBidsCallbackEventArgs(bid, bids, position));
            }
        }
    }

    public class ClientNotifiedEventArgs : EventArgs
    {
        private readonly string message;

        public ClientNotifiedEventArgs(string message)
        {
            this.message = message;
        }

        public string Message { get { return message; } }
    }

    
    public class ClientCallbackNotifiedEventArgs : EventArgs
    {
        private readonly string uri;
        private readonly int i;

        public ClientCallbackNotifiedEventArgs(string uri)
        {
            this.uri = uri;
        }

        public ClientCallbackNotifiedEventArgs(int i)
        {
            this.i = i;
        }

        public string Uri { get { return uri; } }
        public int I { get { return i; } }
    }

    public class ThrowCardCallbackNotifiedEventArgs : EventArgs
    {
        private readonly string cardUri;
        private readonly PlayerTablePosition playerPosition;

        public ThrowCardCallbackNotifiedEventArgs(PlayerTablePosition playerPosition, string cardUri)
        {
            this.cardUri= cardUri;
            this.playerPosition = playerPosition;
        }

        public PlayerTablePosition PlayerPosition { get { return playerPosition; } }
        public string CardUri { get { return cardUri; } }
    }

    public class TurnEndsCallbackEventArgs : EventArgs
    {
        private readonly PlayerTablePosition bookedPlayerPosition;
        private readonly int numberOfBooks;

        public TurnEndsCallbackEventArgs(PlayerTablePosition bookedPlayerPosition, int numberOfBooks)
        {
            this.bookedPlayerPosition = bookedPlayerPosition;
            this.numberOfBooks = numberOfBooks;
        }

        public PlayerTablePosition BookedPlayerPosition { get { return bookedPlayerPosition; } }
        public int NumberOfBooks { get { return numberOfBooks; } }
    }

    public class PlayerBidsCallbackEventArgs : EventArgs
    {
        private readonly BidName bid;
        private readonly ObservableCollection<Bid> bids;
        private readonly PlayerTablePosition position;

        public PlayerBidsCallbackEventArgs(BidName bid, Bid[] bids, PlayerTablePosition position)
        {
            this.bid = bid;
            this.bids = new ObservableCollection<Bid>(bids);
            this.position = position;
        }

        public BidName Bid {  get { return bid; } }
        public ObservableCollection<Bid> Bids { get { return bids; } }
        public PlayerTablePosition Position { get { return position; } }
    }

}
