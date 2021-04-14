using System;
using RestClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;

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
    
    public class UpbitAPI : RestClient.RestClient
    {
        public UpbitAPI() : base(600, 10)
        {
        }

        public async Task<List<Market>> GetAsyncMarketList()
        {
            return await GetAsync<List<Market>>(UpbitURL.MarketAllURL);
        }

        public async Task<List<Orderbook>> GetAsyncOrderbookList(string marketCodes)
        {
            var builder = new UriBuilder(UpbitURL.OrderBookURL);
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["markets"] = marketCodes;
            builder.Query = query.ToString();

            return await GetAsync<List<Orderbook>>(builder.Uri.ToString());
        }

        public async Task<List<MinuteCandle>> GetAsyncMinuteCandles(string marketCode, int unit, string to = "", int count = 0)
        {
            var builder = new UriBuilder(UpbitURL.CandleMinutesURL + unit);
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["market"] = marketCode;
            if (to != "")
            {
                query["to"] = to;
            }
            if (count != 0)
            {
                query["count"] = count.ToString();
            }
            builder.Query = query.ToString();

            return await GetAsync<List<MinuteCandle>>(builder.Uri.ToString());
        }
        
        public async Task<List<DayCandle>> GetAsyncDayCandles(string marketCode, string to = "", int count = 0, string convrtingPriceUnit = "")
        {
            var builder = new UriBuilder(UpbitURL.CandleDaysURL);
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["market"] = marketCode;
            if (to != "")
            {
                query["to"] = to;
            }
            if (count != 0)
            {
                query["count"] = count.ToString();
            }
            if (to != "")
            {
                query["convrtingPriceUnit"] = convrtingPriceUnit;
            }
            builder.Query = query.ToString();

            return await GetAsync<List<DayCandle>>(builder.Uri.ToString());
        }

        public async Task<List<WeekCandle>> GetAsyncWeekCandles(string marketCode, string to = "", int count = 0, string convrtingPriceUnit = "")
        {
            var builder = new UriBuilder(UpbitURL.CandleWeeksURL);
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["market"] = marketCode;
            if (to != "")
            {
                query["to"] = to;
            }
            if (count != 0)
            {
                query["count"] = count.ToString();
            }
            builder.Query = query.ToString();

            return await GetAsync<List<WeekCandle>>(builder.Uri.ToString());
        }

        public async Task<List<MonthCandle>> GetAsyncMonthCandles(string marketCode, string to = "", int count = 0, string convrtingPriceUnit = "")
        {
            var builder = new UriBuilder(UpbitURL.CandleMonthsURL);
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["market"] = marketCode;
            if (to != "")
            {
                query["to"] = to;
            }
            if (count != 0)
            {
                query["count"] = count.ToString();
            }
            builder.Query = query.ToString();

            return await GetAsync<List<MonthCandle>>(builder.Uri.ToString());
        }

        public async Task<List<TradeTick>> GetAsyncTradeTicks(string marketCode, string to = "", int count = 0, string cursor = "", int daysAgo = 0)
        {
            var builder = new UriBuilder(UpbitURL.TradeTicksURL);
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["market"] = marketCode;
            if (to != "")
            {
                query["to"] = to;
            }
            if (count != 0)
            {
                query["count"] = count.ToString();
            }
            if (cursor != "")
            {
                query["cursor"] = cursor;
            }
            if (daysAgo != 0)
            {
                query["daysAgo"] = daysAgo.ToString();
            }
            builder.Query = query.ToString();

            return await GetAsync<List<TradeTick>>(builder.Uri.ToString());
        }

        public async Task<List<Ticker>> GetAsyncTicker(string marketCodes)
        {
            var builder = new UriBuilder(UpbitURL.TickerURL);
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["markets"] = marketCodes;
            builder.Query = query.ToString();

            return await GetAsync<List<Ticker>>(builder.Uri.ToString());
        }

        public void TestGet()
        {
            List<Task> taskList = new List<Task>();
            
            taskList.Add(GetAsyncMarketList());
            taskList.Add(GetAsyncOrderbookList("KRW-BTC"));
            taskList.Add(GetAsyncMinuteCandles("KRW-BTC", 1));
            taskList.Add(GetAsyncWeekCandles("KRW-BTC"));
            taskList.Add(GetAsyncMonthCandles("KRW-BTC"));
            taskList.Add(GetAsyncTradeTicks("KRW-BTC"));
            taskList.Add(GetAsyncTicker("KRW-BTC"));

            Task.WaitAll(taskList.ToArray());
            Console.WriteLine("Complete...");
        }
    }
}