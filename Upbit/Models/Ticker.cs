using System.Collections.Generic;
using System.ComponentModel;

namespace Upbit
{
    public class Ticker : INotifyPropertyChanged
    {
        public string market;
        public string trade_date;
        public string trade_time;
        public string trade_date_kst;
        public string trade_time_kst;
        public double opening_price;
        public double high_price;
        public double low_price;
        public double trade_price;
        public double prev_closing_price;
        public string change;
        public double change_price;
        public double change_rate;
        public double signed_change_price;
        public double signed_change_rate;
        public double trade_volume;
        public double acc_trade_price;
        public double acc_trade_price_24h;
        public double acc_trade_volume;
        public double acc_trade_volume_24h;
        public double highest_52_week_price;
        public string highest_52_week_date;
        public double lowest_52_week_price;
        public string lowest_52_week_date;
        public long timestamp;

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(sender, args);
        }
    }

}