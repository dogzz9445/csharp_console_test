using System.Collections.Generic;
using System.ComponentModel;

namespace Upbit
{
    public class Candle : INotifyPropertyChanged
    {
        public string market;
        public string candle_date_time_utc;
        public string candle_date_time_kst;
        public double opening_price;
        public double high_price;
        public double low_price;
        public double trade_price;
        public long timestamp;
        public double candle_acc_trade_pirce;
        public double candle_acc_trade_volume;
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(sender, args);
        }
    }

    public class MinuteCandle : Candle
    {
        public int unit;
    }

    public class DayCandle : Candle
    {
        public double prev_closing_pirce;
        public double change_price;
        public double change_rate;
        public double converted_trade_price;
    }
    
    public class WeekCandle : Candle
    {
        public string first_day_of_period;
    }

    public class MonthCandle : Candle
    {
        public string first_day_of_period;
    }
}