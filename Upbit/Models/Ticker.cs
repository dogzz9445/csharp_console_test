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

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(sender, args);
        }
    }

}