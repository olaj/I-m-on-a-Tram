using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Vasttrafik
{
	public partial class PageHeader : UserControl
	{

        public string Title {
            get { return PageTitle.Text; }
            set { PageTitle.Text = value; }
        }
        

        public bool ShowProgresBar {
            get {
                if (progressBar.Visibility == System.Windows.Visibility.Collapsed)
                    return false;

                return true;
            }
            set {
                if (value)
                    progressBar.Visibility = System.Windows.Visibility.Visible;
                else
                    progressBar.Visibility = System.Windows.Visibility.Collapsed;
            }
        }


		public PageHeader()
		{
			// Required to initialize variables
			InitializeComponent();
		}
	}
}