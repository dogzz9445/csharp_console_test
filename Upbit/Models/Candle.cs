using System.Collections.Generic;
using System.ComponentModel;

namespace Upbit
{
    public class Candle
    {
        public string market;
    }

    public class Candle : INotifyPropertyChanged
    {
        public string market;
        public string candle_date_time_utc;
        public string candle_date_time_utc;

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(sender, args);
        }
    }

}