using System;
using System.ServiceModel;
using CardGameXService;

namespace CardGameXServer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(CardGameXService.ChatService)))
            {
                try
                {
                    host.Open();
                    Console.WriteLine("Host started...");
                    Console.ReadLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

        }
    }
}
