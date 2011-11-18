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
using System.Collections.ObjectModel;

namespace Vasttrafik.ViewModel {




    public class TripViewModel {
        private Data.VasttrafikAPI vasttrafikAPI;
        public event EventHandler LoadComplete;

        private ObservableCollection<Model.Trip> tripCollection = new ObservableCollection<Model.Trip>();
        public ObservableCollection<Model.Trip> TripCollection {
            get {
                return this.tripCollection;
            }
            protected set {
                this.tripCollection = value;
            }
        }

        public TripViewModel() :
            this(new Data.VasttrafikAPI()) {
        }

        public TripViewModel(Data.VasttrafikAPI vasttrafik) {
            vasttrafikAPI = vasttrafik;
            //vasttrafikAPI.TripLoadingComplete += new EventHandler<Data.TripLoadingEventArgs>(vasttrafikAPI_TripLoadingComplete);
        }

        //void vasttrafikAPI_TripLoadingComplete(object sender, Data.TripLoadingEventArgs e) {

        //    vasttrafikAPI.GetStopsBySearch(search, (stops) => Deployment.Current.Dispatcher.BeginInvoke(() => {
        //        // Add the new games
        //        foreach (Model.Stop s in stops) {
        //            StopCollection.Add(s);
        //        };

        //        if (LoadComplete != null) LoadComplete(this, null);
        //    }));

        //    //Application.Current.RootVisual.Dispatcher.BeginInvoke(() => {
        //    //    // Clear the list
        //    //    tripCollection.Clear();

        //    //    // Add the new games
        //    //    foreach (Model.Trip t in e.Results) tripCollection.Add(t);

        //    //    if (LoadComplete != null) LoadComplete(this, null);
        //    //});
        //}

        public void LoadTrips(string stop_id) {

            vasttrafikAPI.GetNextTrips(stop_id, (trips) => Deployment.Current.Dispatcher.BeginInvoke(() => {
                // Clear the list
                TripCollection.Clear();

                if (trips != null) {
                    foreach (Model.Trip t in trips) TripCollection.Add(t);
                }

                if (LoadComplete != null) LoadComplete(this, null);
            }));

            //vasttrafikAPI.GetNextTrips(stop_id);
        }
    }
}
