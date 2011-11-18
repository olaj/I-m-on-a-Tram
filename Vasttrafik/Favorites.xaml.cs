using Microsoft.Phone.Controls;

namespace Vasttrafik {
    public partial class Favorites : PhoneApplicationPage {

        public Favorites() {
            InitializeComponent();
            myList.LoadComplete += new System.EventHandler(myList_LoadComplete);
        }

        void myList_LoadComplete(object sender, System.EventArgs e) {
            PageHeader.ShowProgresBar = false;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e) {
            if (!(App.Current as App).isFromBackButton) {
                PageHeader.ShowProgresBar = true;
                myList.LoadFavorites();
            }

            (App.Current as App).isFromBackButton = false;
        }
    }
}