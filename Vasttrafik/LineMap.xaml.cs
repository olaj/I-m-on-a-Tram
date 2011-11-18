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
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Net.NetworkInformation;

namespace Vasttrafik {
    public partial class LineMap : PhoneApplicationPage {
        public LineMap() {
            InitializeComponent();

            PageHeader.ShowProgresBar = true;

            Vasttrafik.Data.VasttrafikAPI api = new Data.VasttrafikAPI();
            if (NetworkInterface.GetIsNetworkAvailable()) {
                api.GetImage(image => Deployment.Current.Dispatcher.BeginInvoke(() => {
                    BitmapImage img = new BitmapImage();
                    StreamResourceInfo Resource = new StreamResourceInfo(image, null);
                    img.SetSource(Resource.Stream);
                    mymap.Source = img;
                    PageHeader.ShowProgresBar = false;
                }));
            } else {
                PageHeader.ShowProgresBar = false;
            }
        }
    }
}