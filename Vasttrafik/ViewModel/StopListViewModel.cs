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
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Vasttrafik.Model;
using System.Xml.Linq;
using System.Linq;
using Vasttrafik.Classes;

namespace Vasttrafik.ViewModel {

    public class StopListViewModel {

        private Data.VasttrafikAPI vasttrafikAPI;
        public event EventHandler LoadComplete;
        public bool ForRoute = false;

        private ObservableCollection<Model.Stop> stopCollection = new ObservableCollection<Model.Stop>();
        public ObservableCollection<Model.Stop> StopCollection {
            get {
                return this.stopCollection;
            }
            protected set {
                this.stopCollection = value;
            }
        }

        public StopListViewModel() :
            this(new Data.VasttrafikAPI()) {
        }

        public StopListViewModel(Data.VasttrafikAPI catalog) {
            vasttrafikAPI = catalog;
            //vasttrafikAPI.StopLoadingComplete += new EventHandler<Data.StopLoadingEventArgs>(vasttrafikAPI_StopLoadingComplete);
        }

        //void vasttrafikAPI_StopLoadingComplete(object sender, Data.StopLoadingEventArgs e) {
        //    Application.Current.RootVisual.Dispatcher.BeginInvoke(() => {
        //        // Clear the list
        //        StopCollection.Clear();

        //        // Add the new games
        //        foreach (Model.Stop s in e.Results) {
        //            StopCollection.Add(s);
        //        };

        //        if (LoadComplete != null) LoadComplete(this, null);
        //    });
        //}

        public void LoadStopsBySearch(string search, bool forRoute) {
            ForRoute = forRoute;

            vasttrafikAPI.GetStopsBySearch(search, (stops) => Deployment.Current.Dispatcher.BeginInvoke(() => {

                if (stops != null) {
                    foreach (Model.Stop s in stops) {
                        StopCollection.Add(s);
                    };
                }

                if (LoadComplete != null) LoadComplete(this, null);
            }));

            //vasttrafikAPI.GetStopsBySearch(search);
        }

        public void LoadStopsByLocation(double latitude, double longitude) {

            vasttrafikAPI.GetStopsByLocation(latitude, longitude, (stops) => Deployment.Current.Dispatcher.BeginInvoke(() => {

                if (stops != null) {
                    foreach (Model.Stop s in stops) {
                        StopCollection.Add(s);
                    };
                }

                if (LoadComplete != null) LoadComplete(this, null);
            }));

            //vasttrafikAPI.GetStopsByLocation(latitude, longitude);
        }

        internal void LoadFavorites() {
            vasttrafikAPI.GetFavorites((stops) => Deployment.Current.Dispatcher.BeginInvoke(() => {
                // Add the new games
                foreach (Model.Stop s in stops) {
                    StopCollection.Add(s);
                };

                if (LoadComplete != null) LoadComplete(this, null);
            }));
        }
    }
}
