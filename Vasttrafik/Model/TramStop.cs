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

namespace Vasttrafik.Model {
    public class TramStop {
        public int stop_id { get; set; }
        public string friendly_name { get; set; }
        public string name { get; set; }
        public string stopUrl { get; set; }
        public string county { get; set; }

        public string Displayname {
            get {
                return name + ", " + county;
            }
        }

        public bool IsFavorite { get; set; }
        public string FavoriteImage {
            get {
                if (IsFavorite) {
                    return "/gfx/favorite.png";
                } else {
                    return "/gfx/not_favorite.png";
                }
            }
        }

        public string FavoriteUI {
            get {
                if (IsFavorite) {
                    return "1";
                } else {
                    return "0.5";
                }
            }
        }

        public string StopURL {
            get {
                return "/Stop.xaml?stop_id=" + stop_id + "&name=" + name;
            }
        }
    }
}
