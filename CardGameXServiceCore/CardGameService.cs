using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;

namespace CardGameXServiceCore
{

  /*  public class PlayerMadeTurnEventArgs : EventArgs
    {
        public string cardUri;
    }*/

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CardGameService : ICardGameService
    {
        GameTable gameTable = new GameTable();

        private readonly Dictionary<string, ICardGameServiceCallback> clients =
       new Dictionary<string, ICardGameServiceCallback>();

        public ObservableCollection<string> GetPlayerHand(string pName)
        {
            Player player;
            player = gameTable.Players.FirstOrDefault(x => x.Name == pName);

            return new ObservableCollection<string>(player.PlayerHand.OrderBy(s => s.CardSuit).ThenBy(r => r.CardRank).Select(s => s.SuitImage));
        }

        public ObservableCollection<Bid> GetPlayerAvaliableBids(string pName)
        {
            Player player;
            player = gameTable.Players.FirstOrDefault(x => x.Name == pName);

            return player.Bids;
        }

        public ObservableCollection<string> GetTalon()
        {
            return new ObservableCollection<string>(gameTable.Talon.Select(s => s.SuitImage));
        }

        public void SendMessage(string playerName, string value)
        {
            BroadcastMessage(playerName, value);
        }

        public string Subscribe()
        {
            ICardGameServiceCallback callback =
            OperationContext.Current.GetCallbackChannel<ICardGameServiceCallback>();

            Player player = new Player();
            player.Name = Guid.NewGuid().ToString();
            if (clients.Count == 0)
                player.TablePosition = Player.PlayerTablePosition.South;
            else if (clients.Count == 1)
                player.TablePosition = Player.PlayerTablePosition.West;
            else if (clients.Count == 2)
                player.TablePosition = Player.PlayerTablePosition.East;
            else if (clients.Count == 3)
                player.TablePosition = Player.PlayerTablePosition.Notrh;
            gameTable.Players.Add(player);

            if (callback != null)
            {
                lock (clients)
                {
                    clients.Add(player.Name, callback);
                }
            }

            if (clients.Count == 3)
            {
                gameTable.DealDeck(gameTable.Players);
                gameTable.Players[0].IsDealer = true;
                gameTable.Players[0].IsCurrentTurn = true;
                
            }

            BroadcastConnected(player.Name);

            return player.Name;
        }

        public void Unsubscribe(string playerName)
        {
            lock (clients)
            {
                if (clients.ContainsKey(playerName))
                {
                    clients.Remove(playerName);
                }
            }
        }

        private void BroadcastMessage(string playerName, string message)
        {
            ThreadPool.QueueUserWorkItem
            (
                delegate
                {
                    lock (clients)
                    {
                        List<string> disconnectedClientGuids = new List<string>();

                        foreach (KeyValuePair<string, ICardGameServiceCallback> client in clients)
                        {
                            try
                            {
                                client.Value.HandleMessage(message);
                            }
                            catch (Exception)
                            {
                                disconnectedClientGuids.Add(playerName);
                            }
                        }

                        foreach (string pName in disconnectedClientGuids)
                        {
                            clients.Remove(pName);
                        }
                    }
                }
            );
        }

        private void BroadcastUri(string clientId, string message)
        {
            ThreadPool.QueueUserWorkItem
            (
                delegate
                {
                    lock (clients)
                    {
                        List<string> disconnectedClientGuids = new List<string>();

                        foreach (KeyValuePair<string, ICardGameServiceCallback> client in clients)
                        {
                            try
                            {
                                client.Value.HandleUri(message);
                            }
                            catch (Exception)
                            {
                                disconnectedClientGuids.Add(client.Key);
                            }
                        }

                        foreach (string pName in disconnectedClientGuids)
                        {
                            clients.Remove(pName);
                        }
                    }
                }
            );
        }

        private void CardUri(Player.PlayerTablePosition playerPosition, string cardUri)
        {
            ThreadPool.QueueUserWorkItem
            (
                delegate
                {
                    lock (clients)
                    {
                        List<string> disconnectedClientGuids = new List<string>();

                        foreach (KeyValuePair<string, ICardGameServiceCallback> client in clients)
                        {
                            try
                            {
                                client.Value.HandleCardUri(playerPosition, cardUri);
                            }
                            catch (Exception)
                            {
                                disconnectedClientGuids.Add(client.Key);
                            }
                        }

                        foreach (string pName in disconnectedClientGuids)
                        {
                            clients.Remove(pName);
                        }
                    }
                }
            );
        }

        public void BroadcastConnected(string clientId)
        {
            ThreadPool.QueueUserWorkItem
           (
               delegate
               {
                   lock (clients)
                   {
                       List<string> disconnectedClientGuids = new List<string>();

                       foreach (KeyValuePair<string, ICardGameServiceCallback> client in clients)
                       {
                           try
                           {
                               client.Value.Connected(clients.Count);
                           }
                           catch (Exception)
                           {
                               disconnectedClientGuids.Add(client.Key);
                           }
                       }

                       foreach (string pName in disconnectedClientGuids)
                       {
                           clients.Remove(pName);
                       }
                   }
               }
           );
        }

        public void GetUri(string clientId, string value)
        {
            BroadcastUri(clientId, value);
        }

        private void StartGame()
        {
            
        }

        public Player.PlayerTablePosition GetPlayerPosition(string pName)
        {
            Player player;
            player = gameTable.Players.FirstOrDefault(x => x.Name == pName);

            return player.TablePosition;
        }

        public void Bidd(string pName, string bid)
        {
            Player player;
            player = gameTable.Players.FirstOrDefault(p => p.Name == pName);
            int playerIndex = gameTable.Players.IndexOf(player);
            player.Bid = (Bid.BidName)Enum.Parse(typeof(Bid.BidName), bid);
            player.Bided = true;
            int index = player.Bids.IndexOf(player.Bids.Where(b => b.Bid_ == player.Bid).FirstOrDefault());
            
            for (int i = 0; i <= index; i++)
            {
                player.Bids[i].CanBid = false;
            }


            if (playerIndex == 2)
            {
                gameTable.Players[1].Bids = player.Bids;
                gameTable.Players[0].Bids = player.Bids;
                gameTable.Players[1].Bids[index].CanBid = true;
            }
            else if (playerIndex == 1)
            {
                gameTable.Players[0].Bids = player.Bids;
                gameTable.Players[2].Bids = player.Bids;
                gameTable.Players[0].Bids[index].CanBid = true;
            }
            else if (playerIndex == 0)
            {
                for (int i = 0; i <= index; i++)
                {
                    gameTable.Players[2].Bids[i].CanBid = false;
                    gameTable.Players[1].Bids[i].CanBid = false;
                }
                gameTable.Players[2].Bids[index].CanBid = true;
            }
        
            if (gameTable.Players.Any(p => p.Bid < player.Bid))
            {
                player.Overbided = true;
            }
            BroadcastPlayerBids(player.Bid, player.Bids, player.TablePosition);
        }

        public string[] GetPlayerNames()
        {
            return gameTable.Players.Select(p => p.Name).ToArray();
        }

        public void ThrowCard(string clientId, string cardUri)
        {
            Player player;
            player = gameTable.Players.FirstOrDefault(x => x.Name == clientId);
            int playerIndex = gameTable.Players.IndexOf(player) + 1;
            Card thrownCard = player.PlayerHand.FirstOrDefault(c => c.SuitImage == cardUri);
            CardUri(player.TablePosition, thrownCard.SuitImage);
            player.ThrownCard = thrownCard;
            player.IsCurrentTurn = false;

            if (playerIndex > 2)
                playerIndex = 0;

            gameTable.Players[playerIndex].IsCurrentTurn = true;

            int thrownCards = gameTable.Players.Count(p => p.ThrownCard != null);
            if (thrownCards == 3)
            {
                Player bookedPlayer = gameTable.EndTurn(gameTable.Players);
                BroadcastTurnEnds(bookedPlayer.TablePosition, bookedPlayer.NumberOfBooks);
            }

        }

        private void BroadcastTurnEnds(Player.PlayerTablePosition bookedPlayerPosition, int numberOfBooks)
        {
            ThreadPool.QueueUserWorkItem
            (
                delegate
                {
                    lock (clients)
                    {
                        List<string> disconnectedClientGuids = new List<string>();

                        foreach (KeyValuePair<string, ICardGameServiceCallback> client in clients)
                        {
                            try
                            {
                                client.Value.TurnEnds(bookedPlayerPosition, numberOfBooks);
                            }
                            catch (Exception)
                            {
                                disconnectedClientGuids.Add(client.Key);
                            }
                        }

                        foreach (string pName in disconnectedClientGuids)
                        {
                            clients.Remove(pName);
                        }
                    }
                }
            );
        }

        private void BroadcastPlayerBids(Bid.BidName bid, ObservableCollection<Bid> bids, Player.PlayerTablePosition position)
        {
            try
            {
                ThreadPool.QueueUserWorkItem
                (
                    delegate
                    {
                        lock (clients)
                        {
                            List<string> disconnectedClientGuids = new List<string>();

                            foreach (KeyValuePair<string, ICardGameServiceCallback> client in clients)
                            {
                                try
                                {
                                    client.Value.PlayerBids(bid, bids, position);
                                }
                                catch (Exception)
                                {
                                    disconnectedClientGuids.Add(client.Key);
                                }
                            }

                            foreach (string pName in disconnectedClientGuids)
                            {
                                clients.Remove(pName);
                            }
                        }
                    }
                );
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public bool CheckPlayerCurrentTurn(string pName)
        {
            Player player;
            player = gameTable.Players.FirstOrDefault(x => x.Name == pName);
            return player.IsCurrentTurn;
        }
    }
}
