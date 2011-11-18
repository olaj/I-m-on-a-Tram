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
using System.Collections;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using System.Linq;

namespace Vasttrafik.Model {

    public class Stop : INotifyPropertyChanged {

        private int _stop_id;
        public int stop_id {
            get { return _stop_id; }
            set {
                _stop_id = value;
                OnPropertyChanged("stop_id");
                OnPropertyChanged("StopURL");
            }
        }

        private string _firendly_name;
        public string friendly_name {
            get { return _firendly_name; }
            set {
                _firendly_name = value;
                OnPropertyChanged("friendly_name");
            }
        }

        private string _name;
        public string name {
            get { return _name; }
            set {
                _name = value;
                OnPropertyChanged("name");
                OnPropertyChanged("Displayname");
                OnPropertyChanged("StopURL");
            }
        }

        private string _county;
        public string county {
            get { return _county; }
            set {
                _county = value;
                OnPropertyChanged("county");
                OnPropertyChanged("Displayname");
            }
        }

        public string Displayname {
            get {
                return name + ", " + county;
            }
        }

        public string FavoriteUI {
            get {
                if (IsFavorite) {
                    return "1";
                } else {
                    return "0.5";
                }
            }
        }

        public string StopURL {
            get {
                return "/Stop.xaml?stop_id=" + stop_id + "&name=" + name;
            }
        }

        public void ToggleFavorite() {
            if (IsFavorite) {
                ((App.Current as App).UserFavorites).Remove(this);

            } else {
                ((App.Current as App).UserFavorites).Add(this);
            }

            ((App.Current as App).UserFavorites).Save();

            OnPropertyChanged("IsFavorite");
            OnPropertyChanged("FavoriteUI");
        }

        public bool IsFavorite {
            get {
                if ((App.Current as App).UserFavorites.Where(x => x.stop_id == this.stop_id).Count() > 0)
                    return true;

                return false;
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
