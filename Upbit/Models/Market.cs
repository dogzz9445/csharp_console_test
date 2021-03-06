using System.Collections.Generic;
using System.ComponentModel;

namespace Upbit
{
    public class Market : INotifyPropertyChanged
    {
        public string market;
        public string korean_name;
        public string english_name;
        public string market_warning;
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(sender, args);
        }
    }

}