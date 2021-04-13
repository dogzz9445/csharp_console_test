using System;
using Upbit;

namespace example_dotnet
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            bool bResponse = false;

            UpbitAPI api = new UpbitAPI();
            api.MessageReceived += (s, e) => 
            {
                bResponse = true;
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
