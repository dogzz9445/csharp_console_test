using System.ComponentModel;
using System.Collections.Generic;

namespace Upbit
{
    public class MarketViewModel : INotifyPropertyChanged
    {
        private List<Market> markets;

        public List<Market> Markets 
        { 
            get => markets; 
            set
            {
                markets = value;
                //marketsPropertyChanged += PropertyChanged;
                RaisePropertyChanged(this, new PropertyChangedEventArgs("Markets"));
            } 
        }

        public List<Market> KRWMarkets { get => markets.FindAll(market => (market.market.Contains("KRW"))); }

        public List<string> KRWMarketCodes { get => KRWMarkets.ConvertAll(market => market.market); }


        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(sender, args);
        }
    }
}