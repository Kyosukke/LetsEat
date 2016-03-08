using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Services.Maps;
using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace LetsEat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DinerListMenu : Page
    {
        ObservableCollection<ListItem> dinerInfo = new ObservableCollection<ListItem>();
        string groupName;

        public DinerListMenu()
        {
            this.InitializeComponent();

           FindDinerList();
      
            //this.geolocator = new Geolocator();
        }
        async void FindDinerList()
        { 
            dinerInfo.Add(new ListItem("macdo"));
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            groupName = e.Parameter as string;
            FindDinerList();
            dinerList.ItemsSource = dinerInfo;
        }

        private void dinerList_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void dinerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Frame.Navigate(typeof(DinerMenu), dinerList.SelectedItem);
        }
    }
}
