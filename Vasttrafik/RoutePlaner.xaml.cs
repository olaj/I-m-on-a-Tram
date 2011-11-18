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

namespace Vasttrafik
{
    public partial class RoutePlaner : PhoneApplicationPage
    {
        public RoutePlaner()
        {
            InitializeComponent();

            
        }

        void listStops2_RouteSelected(object sender, EventArgs e) {
            throw new NotImplementedException();
        }

        void listStops_RouteSelected(object sender, EventArgs e) {
            
            //throw new NotImplementedException();
        }

        private void txtStop_TextChanged(object sender, TextChangedEventArgs e) {
            
        }

        private void txtStop2_TextChanged(object sender, TextChangedEventArgs e) {

        }
    }
}
