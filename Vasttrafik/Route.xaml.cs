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
using Vasttrafik.Views;

namespace Vasttrafik {
    public partial class Route : PhoneApplicationPage {

        public int StopId1 { get; set; }
        public int StopId2 { get; set; }

        public Route() {
            InitializeComponent();
            //listStops.RouteSelected += new EventHandler<RouteSelectedEventArgs>(listStops_RouteSelected);
            //listStops2.RouteSelected += new EventHandler<RouteSelectedEventArgs>(listStops2_RouteSelected);
        }

        //void listStops2_RouteSelected(object sender, RouteSelectedEventArgs e) {
        //    StopId2 = e.StopId;

        //    if (StopId1 != 0) {
        //        var v = new Vasttrafik.Data.VasttrafikAPI();
        //        v.GetRoute(StopId1.ToString(), StopId2.ToString());
        //    }
        //}

        //void listStops_RouteSelected(object sender, RouteSelectedEventArgs e) {
        //    StopId1 = e.StopId;

        //    if (StopId2 != 0) {
        //        var v = new Vasttrafik.Data.VasttrafikAPI();
        //        v.GetRoute(StopId1.ToString(), StopId2.ToString());
        //    }
        //}

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e) {
            base.OnBackKeyPress(e);
        }

        private void txtStop_TextChanged(object sender, TextChangedEventArgs e) {
            if (txtStop.Text.Length > 2) {
                //PageHeader.ShowProgresBar = true;
                listStops.LoadStopsBySearchForRoute(txtStop.Text);
            }
        }

        private void txtStop2_TextChanged(object sender, TextChangedEventArgs e) {
            if (txtStop2.Text.Length > 2) {
                //PageHeader.ShowProgresBar = true;
                listStops2.LoadStopsBySearchForRoute(txtStop2.Text);
            }
        }
    }
}