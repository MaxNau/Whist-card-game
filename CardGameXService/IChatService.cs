using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CardGameXService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IChatService" in both code and config file together.
    [ServiceContract(CallbackContract = typeof(IChatServiceCallback))]
    public interface IChatService
    {
        [OperationContract]
        Guid Subscribe();

        [OperationContract(IsOneWay = true)]
        void Unsubscribe(Guid clientId);

        [OperationContract(IsOneWay = true)]
        void SendMessage(Guid clientId, string value);
    }

    [ServiceContract]
    public interface IChatServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void HandleMessage(string value);
    }


}
