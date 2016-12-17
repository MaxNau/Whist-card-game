using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace CardGameXService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ChatService" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ChatService : IChatService
    {

        private readonly Dictionary<Guid, IChatServiceCallback> clients =
       new Dictionary<Guid, IChatServiceCallback>();

        public void SendMessage(Guid clientId, string value)
        {
            BroadcastMessage(clientId, value);
        }

        public Guid Subscribe()
        {
            IChatServiceCallback callback =
            OperationContext.Current.GetCallbackChannel<IChatServiceCallback>();

            Guid clientId = Guid.NewGuid();

            if (callback != null)
            {
                lock (clients)
                {
                    clients.Add(clientId, callback);
                   
                }
            }

            return clientId;
        }

        public void Unsubscribe(Guid clientId)
        {
            lock (clients)
            {
                if (clients.ContainsKey(clientId))
                {
                    clients.Remove(clientId);
                }
            }
        }

        private void BroadcastMessage(Guid clientId, string message)

        {
            ThreadPool.QueueUserWorkItem
            (
                delegate
                {
                    lock (clients)
                    {
                        List<Guid> disconnectedClientGuids = new List<Guid>();

                        foreach (KeyValuePair<Guid, IChatServiceCallback> client in clients)
                        {
                            try
                            {
                                client.Value.HandleMessage(message);
                            }
                            catch (Exception)
                            {
                            disconnectedClientGuids.Add(client.Key);
                            }
                        }

                        foreach (Guid clientGuid in disconnectedClientGuids)
                        {
                            clients.Remove(clientGuid);
                        }
                    }
                }
            );
        }
    }

 
}
