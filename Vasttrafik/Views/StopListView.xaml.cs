using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Vasttrafik.ViewModel;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Media;

namespace Vasttrafik.Views {

    public class RouteSelectedEventArgs : EventArgs {
        public int StopId { get; private set; }

        public RouteSelectedEventArgs(int stop_id) {
            StopId = stop_id;
        }
    }

    public partial class StopListView : UserControl {

        StopListViewModel viewModel = null;

        public event EventHandler LoadComplete;
        public event EventHandler<RouteSelectedEventArgs> RouteSelected;

        public StopListView() {
            InitializeComponent();

            //listStops.Items.Clear();

            viewModel = Resources["TheViewModel"] as StopListViewModel;
            viewModel.LoadComplete += new EventHandler(viewModel_LoadComplete);
        }

        void viewModel_LoadComplete(object sender, EventArgs e) {
            if (LoadComplete != null) LoadComplete(this, null);
        }

        public void LoadStopsBySearch(string search){
            viewModel.StopCollection.Clear();
            viewModel.LoadStopsBySearch(search, false);
        }

        public void LoadStopsBySearchForRoute(string search) {
            viewModel.StopCollection.Clear();
            viewModel.LoadStopsBySearch(search, true);
        }

        public void LoadStopsByLocation(double latitude, double longitude) {
            viewModel.StopCollection.Clear();
            viewModel.LoadStopsByLocation(latitude, longitude);
        }

        public void LoadFavorites() {
            if (viewModel.StopCollection.Count == 0) {
                viewModel.StopCollection.Clear();
                viewModel.LoadFavorites();
            }
        }

        private void btnFavorite_Click(object sender, RoutedEventArgs e) {
            var img = (Button)sender;
            var favoriteStop = viewModel.StopCollection.FirstOrDefault(x => x.stop_id == int.Parse(img.Tag.ToString()));
            
            if (favoriteStop.IsFavorite) {
                Storyboard sp = ((Grid)img.Parent).Resources["sbToNotFavorite"] as Storyboard;
                if (sp != null)
                    sp.Begin();
            } else {
                Storyboard sp = ((Grid)img.Parent).Resources["sbToFavorite"] as Storyboard;
                if (sp != null)
                    sp.Begin();
            }

            viewModel.StopCollection.FirstOrDefault(x=>x.stop_id==int.Parse(img.Tag.ToString())).ToggleFavorite();
        }

        private void btnNavigateToStop_Click(object sender, RoutedEventArgs e) {
            var link = (Button)sender;
            var stop = (Model.Stop)link.Tag;

            if (viewModel.ForRoute) {
                link.Foreground = new SolidColorBrush(Colors.Red);
                if (RouteSelected != null) {
                    RouteSelected(this, new RouteSelectedEventArgs(stop.stop_id));
                }
            } else {
                
                ((App.Current as App)).RootFrame.Navigate(new Uri(stop.StopURL, UriKind.RelativeOrAbsolute));
            }
        }

        private void listStops_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            //if (e.AddedItems.Count > 0) {
            //    Model.Stop s = (Model.Stop)e.AddedItems[0];

            //    if (viewModel.ForRoute) {

            //    } else {
            //        ((App.Current as App)).RootFrame.Navigate(new Uri(s.StopURL, UriKind.RelativeOrAbsolute));
            //    }
            //}
        }
    }
}
