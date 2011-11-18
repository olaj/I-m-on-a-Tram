using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace Vasttrafik.Model {
    public class Route : INotifyPropertyChanged {

        private int _from_stop_name;
        public int from_stop_name {
            get { return _from_stop_name; }
            set {
                _from_stop_name = value;
                OnPropertyChanged("from_stop_name");
            }
        }

        private int _to_stop_name;
        public int to_stop_name {
            get { return _to_stop_name; }
            set {
                _to_stop_name = value;
                OnPropertyChanged("to_stop_name");
            }
        }



        protected void OnPropertyChanged(string propertyName) {
            if (PropertyChanged != null) {
                var args = new PropertyChangedEventArgs(propertyName);
                PropertyChanged(this, args);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class RouteItem : INotifyPropertyChanged {

        private DateTime _departure_time;
        public DateTime departure_time {
            get { return _departure_time; }
            set {
                _departure_time = value;
                OnPropertyChanged("departure_time");
            }
        }

        private DateTime _arrival_time;
        public DateTime arrival_time {
            get { return _arrival_time; }
            set {
                _arrival_time = value;
                OnPropertyChanged("arrival_time");
            }
        }

        private int _travel_time;
        public int travel_time {
            get { return _travel_time; }
            set {
                _travel_time = value;
                OnPropertyChanged("travel_time");
            }
        }

        private int _changes;
        public int changes {
            get { return _changes; }
            set {
                _changes = value;
                OnPropertyChanged("changes");
            }
        }





        protected void OnPropertyChanged(string propertyName) {
            if (PropertyChanged != null) {
                var args = new PropertyChangedEventArgs(propertyName);
                PropertyChanged(this, args);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
