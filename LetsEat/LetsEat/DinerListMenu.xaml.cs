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
using LetsEat.Class;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace LetsEat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DinerListMenu : Page
    {
        ObservableCollection<ListItem> dinerInfo = new ObservableCollection<ListItem>();
        Group group;

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
            group = e.Parameter as Group;
            FindDinerList();
            dinerList.ItemsSource = dinerInfo;
        }

        private void dinerList_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void dinerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Answer a = new Answer();

            a.name = (dinerList.SelectedItem as ListItem).title;
            a.adresse = "quelque part";
            a.number = "0987654321";

            Frame.Navigate(typeof(DinerMenu), a);
        }
    }
}
