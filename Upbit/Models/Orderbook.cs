using System.Collections.Generic;
using System.ComponentModel;

namespace Upbit
{
    public class Orderbook : INotifyPropertyChanged
    {
        public string market;
        public long timestamp;
        public double total_ask_size;
        public double total_bid_size;
        public List<OrderbookUnit> orderbook_units;

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(sender, args);
        }
    }
    public class OrderbookUnit : INotifyPropertyChanged
    {
        public double ask_price;
        public double bid_price;
        public double ask_size;
        public double bid_size;
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(sender, args);
        }
    }
}