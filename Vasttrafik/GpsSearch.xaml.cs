using System;
using System.Device.Location;
using System.Windows;
using Microsoft.Phone.Controls;
//using MixDemoHelpers.Location;
using Microsoft.Phone.Controls.Maps;
using System.Net.NetworkInformation;

namespace Vasttrafik {
    public partial class GpsSearch : PhoneApplicationPage {

        bool GpsIsDisbaled = false;

        GeoCoordinateWatcher watcher;
        //private IsolatedStorageSettings userSettings = IsolatedStorageSettings.ApplicationSettings; 

        public GpsSearch() {
            InitializeComponent();

            myList.LoadComplete += new EventHandler(myList_LoadComplete);

            //LinneLocationData.Setup();
        }

        void myList_LoadComplete(object sender, EventArgs e) {
            PageHeader.ShowProgresBar = false;
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e) {
            // Stop the location when we leave the page.
            try {
                if (watcher != null)
                    watcher.Stop();
            } catch (Exception) {
                
                //throw;
            }

        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e) {

            PageHeader.ShowProgresBar = true;

            //if (watcher == null) {
            //    GeoCoordinateEventMock[] events = new GeoCoordinateEventMock[] {
            //        new  GeoCoordinateEventMock { Latitude=57.69248275552961, Longitude=11.952738761901855, Time=new TimeSpan(0,0,0) }
            //    };

            //    watcher = new EventListGeoLocationMock(events);
            //    watcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(watcher_StatusChanged);
            //    watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);
            //}
            if (NetworkInterface.GetIsNetworkAvailable()) {
                if (!(App.Current as App).isFromBackButton) {
                    Deployment.Current.Dispatcher.BeginInvoke(() => {

                        if (watcher == null) {
                            // MIX10: Ask for low accuracy (don't bother waiting for GPS);

                            //GeoCoordinateWatcher.InitialPosition = new GeoCoordinate(36.091624402234245, -115.17439086242677, "Mandalay Bay");

                            watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);
                            watcher.StatusChanged += watcher_StatusChanged;
                            watcher.PositionChanged += watcher_PositionChanged;

                            //resolver = new CivicAddressResolver();
                            //resolver.ResolveAddressCompleted += ResolveAddressCompleted;
                        }

                        watcher.Start();
                    });
                } else {
                    PageHeader.ShowProgresBar = false;
                }
            } else {
                GpsDisabled.Visibility = System.Windows.Visibility.Visible;
                myList.Visibility = System.Windows.Visibility.Collapsed;
                PageHeader.ShowProgresBar = false;
                GpsIsDisbaled = true;
            }

            (App.Current as App).isFromBackButton = false;
        }


        void watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e) {
            Deployment.Current.Dispatcher.BeginInvoke(() => MyStatusChanged(e));
        }

        void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e) {
            Deployment.Current.Dispatcher.BeginInvoke(() => MyPositionChanged(e));
        }

        void MyStatusChanged(GeoPositionStatusChangedEventArgs e) {
            switch (e.Status) {
                case GeoPositionStatus.Disabled:
                    GpsDisabled.Visibility = System.Windows.Visibility.Visible;
                    myList.Visibility = System.Windows.Visibility.Collapsed;
                    PageHeader.ShowProgresBar = false;
                    GpsIsDisbaled = true;
                    watcher.Stop();
                    break;
                case GeoPositionStatus.Initializing:
                    // The location service is initializing. 
                    // Disable the Start Location button 
                    break;
                case GeoPositionStatus.NoData:
                    // The location service is working, but it cannot get location data 
                    //GpsDisabled.Visibility = System.Windows.Visibility.Visible;
                    //myList.Visibility = System.Windows.Visibility.Collapsed;
                    //PageHeader.ShowProgresBar = false;
                    GpsIsDisbaled = true;
                    //watcher.Stop();
                    break;
                case GeoPositionStatus.Ready:
                    // The location service is working and is receiving location data 
                    // Show the current position and enable the Stop Location button 
                    GpsDisabled.Visibility = System.Windows.Visibility.Collapsed;
                    myList.Visibility = System.Windows.Visibility.Visible;
                    GpsIsDisbaled = false;
                    break;
            }
        }


        void MyPositionChanged(GeoPositionChangedEventArgs<GeoCoordinate> e) {

            if (!GpsIsDisbaled) {

                Deployment.Current.Dispatcher.BeginInvoke(() => {

                    System.Device.Location.GeoCoordinate geo = new System.Device.Location.GeoCoordinate(e.Position.Location.Latitude, e.Position.Location.Longitude);

                    map1.Mode = new Microsoft.Phone.Controls.Maps.AerialMode(true);
                    //map1.over

                    map1.SetView(geo, 16);

                    myList.LoadStopsByLocation(e.Position.Location.Latitude, e.Position.Location.Longitude);

                    watcher.Stop();
                });
            }
        }
    }
}