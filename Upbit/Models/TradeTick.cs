using System.Collections.Generic;
using System.ComponentModel;

namespace Upbit
{
    public class TradeTick : INotifyPropertyChanged
    {
        public string trade_date_utc;
        public string trade_time_utc;
        public long timestamp;
        public double trade_price;
        public double trade_volume;
        public double prev_closing_price;
        public double change_price;
        public string ask_bid;
        public long sequential_id;

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(sender, args);
        }
    }

}