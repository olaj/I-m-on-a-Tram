using System;
using System.Windows;
using Microsoft.Phone.Controls;
using System.Net.NetworkInformation;

namespace Vasttrafik {
    public partial class MainPage : PhoneApplicationPage {

        // Constructor
        public MainPage() {
            InitializeComponent();
            listStops.LoadComplete += new EventHandler(listStops_LoadComplete);

            if (!NetworkInterface.GetIsNetworkAvailable()) {
                NetworkDisabled.Visibility = System.Windows.Visibility.Visible;
                listStops.Visibility = System.Windows.Visibility.Collapsed;
            } else {
                NetworkDisabled.Visibility = System.Windows.Visibility.Collapsed;
                listStops.Visibility = System.Windows.Visibility.Visible;
            }
        }

        void listStops_LoadComplete(object sender, EventArgs e) {
            PageHeader.ShowProgresBar = false;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e) {
            try {
                if (!(App.Current as App).isFromBackButton) {
                    listStops.LoadFavorites();
                }

                (App.Current as App).isFromBackButton = false;

            } catch (Exception) {
            }

        }

        private void NavigateToStopPage(object sender, RoutedEventArgs e) {
            //MessageBox.Show(sender.ToString());
        }



        private void txtStop_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            if (txtStop.Text.Length > 2) {
                PageHeader.ShowProgresBar = true;
                listStops.LoadStopsBySearch(txtStop.Text);
            }
        }
    }
}