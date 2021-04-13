using System;
using WebAPIClient;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Upbit
{
    static class UpbitURL
    {
        public const string BaseURL = "https://api.upbit.com/";
        public const string VersionURL = "v1/";
        public const string MarketAllURL = BaseURL + VersionURL + "market/all?isDetails=false";
        public const string CandleMinutesURL = BaseURL + VersionURL + "candles/minutes/";
        public const string CandleDaysURL = BaseURL + VersionURL + "candles/days/";
        public const string CandleWeeksURL = BaseURL + VersionURL + "candles/weeks/";
        public const string CandleMonthsURL = BaseURL + VersionURL + "candles/months/";
        public const string TradeTicksURL = BaseURL + VersionURL + "trades/ticks";
        public const string TickerURL = BaseURL + VersionURL + "ticker";
        public const string OrderBookURL = BaseURL + VersionURL + "orderbook";

    }

    
    public class UpbitAPI : WebAPIClient.WebAPIClient
    {
        public UpbitAPI()
        {
        }

        public async void TestGet()
        {
            var task1 = GetAsync<List<Market>>(UpbitURL.MarketAllURL);
            Task.WaitAll(task1);
            var result1 = task1.Result;
            foreach (var market in result1)
            {
                Console.WriteLine(market.korean_name);
            }
        }
    }
}