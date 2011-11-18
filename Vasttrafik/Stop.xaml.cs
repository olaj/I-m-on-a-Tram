using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Xml.Linq;
using Vasttrafik.Model;
using System.Net.NetworkInformation;

namespace Vasttrafik {
    public partial class Stop : PhoneApplicationPage {

        public Stop() {
            InitializeComponent();

            if (!NetworkInterface.GetIsNetworkAvailable()) {
                NetworkDisabled.Visibility = System.Windows.Visibility.Visible;
                tripList.Visibility = System.Windows.Visibility.Collapsed;
            } else {
                NetworkDisabled.Visibility = System.Windows.Visibility.Collapsed;
                tripList.Visibility = System.Windows.Visibility.Visible;
            }

            tripList.LoadComplete += new EventHandler(tripList_LoadComplete);
            //GetNextTrips("00005140");
        }

        void tripList_LoadComplete(object sender, EventArgs e) {
            PageHeader.ShowProgresBar = false;
            //progressBar.Visibility = System.Windows.Visibility.Collapsed;
        }

        //            

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e) {
            (App.Current as App).isFromBackButton = true;
            base.OnBackKeyPress(e);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e) {
            base.OnNavigatedTo(e);

            PageHeader.ShowProgresBar = true;

            string stop_id;
            string name;
            if (NavigationContext.QueryString.TryGetValue("stop_id", out stop_id)) {
                tripList.LoadTrips(stop_id);
                //GetNextTrips(stop_id);
            }
            if (NavigationContext.QueryString.TryGetValue("name", out name)) {
                PageHeader.Title = name;
            }
        }


    }
}