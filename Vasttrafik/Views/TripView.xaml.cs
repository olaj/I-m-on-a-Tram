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
using Vasttrafik.ViewModel;

namespace Vasttrafik.Views {
    public partial class TripView : UserControl {

        public event EventHandler LoadComplete;
        TripViewModel viewModel = null;

        public TripView() {
            InitializeComponent();

            InitializeComponent();
            viewModel = Resources["TheViewModel"] as TripViewModel;
            viewModel.LoadComplete += new EventHandler(viewModel_LoadComplete);
        }

        public void LoadTrips(string stop_id) {
            viewModel.LoadTrips(stop_id);
        }

        void viewModel_LoadComplete(object sender, EventArgs e) {
            if (LoadComplete != null) LoadComplete(this, null);
        }
    }
}
