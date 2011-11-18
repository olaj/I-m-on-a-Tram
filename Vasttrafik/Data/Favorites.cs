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
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Collections.ObjectModel;
using Vasttrafik.Classes;
using System.Linq;

namespace Vasttrafik.Data {
    public class Favorites : ObservableCollection<Model.Stop> {

        //public List<Model.Stop> UserFavorites = null;

        public Favorites() {
            List<Model.Stop> favs = new List<Model.Stop>();
            List<Model.Stop> tempFavs = new List<Model.Stop>();

            if (!System.ComponentModel.DesignerProperties.IsInDesignTool) {

                if (IsolatedStorageSettings.ApplicationSettings.TryGetValue("FavoriteStops", out favs)) {
                    if (favs != null) {
                        tempFavs = favs;
                    } else {
                        tempFavs = new List<Model.Stop>();
                        IsolatedStorageSettings.ApplicationSettings.Remove("FavoriteStops");
                        IsolatedStorageSettings.ApplicationSettings.Add("FavoriteStops", tempFavs);
                        IsolatedStorageSettings.ApplicationSettings.Save();
                    }
                } else {
                    IsolatedStorageSettings.ApplicationSettings.Add("FavoriteStops", favs);
                    IsolatedStorageSettings.ApplicationSettings.Save();
                }
            }

            foreach (Model.Stop s in tempFavs) this.Add(s);
        }

        public void Save() {
            IsolatedStorageSettings.ApplicationSettings.Remove("FavoriteStops");
            IsolatedStorageSettings.ApplicationSettings.Add("FavoriteStops", this.ToList());
            IsolatedStorageSettings.ApplicationSettings.Save();
        }
    }
}
