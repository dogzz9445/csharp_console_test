using System;
using Upbit;

namespace example_dotnet
{
    class Program
    {
        static void Main(string[] args)
        {
            bool bResponse = false;

            UpbitAPI api = new UpbitAPI();
            api.MessageReceived += (s, e) => 
            {
                bResponse = true;
                var message = Convert.ChangeType(e.ResponseMessage, e.ResponseType);
                Console.WriteLine(message);
            };
            
            api.TestGet();

            while(true)
            {
                if (true == bResponse)
                {
                    Console.WriteLine("Received");
                    break;
                }
            }
        }
    }
}
