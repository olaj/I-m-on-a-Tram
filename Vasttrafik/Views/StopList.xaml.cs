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
using Vasttrafik.Model;
using System.IO.IsolatedStorage;
using Vasttrafik.Code;
using Microsoft.Expression.Interactivity.Core;
using Microsoft.Expression.Interactivity.Media;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace Vasttrafik {
    public partial class StopList : UserControl {

        public List<TramStop> myFavorites;
        private List<TramStop> currentStops;

        public List<TramStop> myStops {
            set {
                currentStops = value;
                BindStops(value);
            }
            get {
                return currentStops;
            }
        }

        private void BindStops(List<TramStop> stops) {

            myFavorites = GetFavorites();

            try {
                foreach (TramStop stop in stops) {

                    var exist = myFavorites.FirstOrDefault(x => x.stop_id == stop.stop_id);
                    if (exist != null)
                        stop.IsFavorite = true;
                    else
                        stop.IsFavorite = false;
                }
            } catch (Exception) {

            }
            listStops.Visibility = System.Windows.Visibility.Visible;
            listStops.ItemsSource = stops;
        }

        public StopList() {
            
            InitializeComponent();

            #region Storage

            

            #endregion
        }

        public List<TramStop> GetFavorites() {

            List<TramStop> favs = new List<TramStop>();
            List<TramStop> tempFavs = new List<TramStop>();

            if (!System.ComponentModel.DesignerProperties.IsInDesignTool) {

                if (IsolatedStorageSettings.ApplicationSettings.TryGetValue("Favorites", out favs)) {
                    if (favs != null) {
                        tempFavs = favs;
                    } else {
                        tempFavs = new List<TramStop>();
                        IsolatedStorageSettings.ApplicationSettings.Remove("Favorites");
                        IsolatedStorageSettings.ApplicationSettings.Add("Favorites", tempFavs);
                        IsolatedStorageSettings.ApplicationSettings.Save();
                        return tempFavs;
                    }
                } else {
                    IsolatedStorageSettings.ApplicationSettings.Add("Favorites", favs);
                    IsolatedStorageSettings.ApplicationSettings.Save();
                    return tempFavs;
                }
            }
            return tempFavs;
        }

        private void chkFavorite_Click(object sender, RoutedEventArgs e) {
            var img = (Button)sender;



            var favstop = myStops.FirstOrDefault(x => x.stop_id == int.Parse(img.Tag.ToString()));
            List<TramStop> favs = new List<TramStop>();

            if (Math.Round(img.Opacity, 1) == 0.5) {
                Storyboard sp = ((StackPanel)img.Parent).Resources["sbToFavorite"] as Storyboard;
                if (sp != null)
                    sp.Begin();
                myFavorites.Add(favstop);
                img.Content = "/gfx/favorite.png";
            } else {
                Storyboard sp = ((StackPanel)img.Parent).Resources["sbToNotFavorite"] as Storyboard;
                if (sp != null)
                    sp.Begin();
                myFavorites.Remove(favstop);
            }



            IsolatedStorageSettings.ApplicationSettings["Favorites"] = myFavorites;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            var btn = (Button)sender;
            string url = btn.Tag.ToString();

            (App.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri(url, UriKind.RelativeOrAbsolute));
        }

    }
}
