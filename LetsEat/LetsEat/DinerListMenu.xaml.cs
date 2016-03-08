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
            //var settings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            //if ((bool)settings.Values["LocationConsent"] != true)
            //{
            //    return;
            //}

            //Geolocator geo = new Geolocator();
            //geo.DesiredAccuracyInMeters = 1;

            //try
            //{
            //    Geoposition pos = await geo.GetGeopositionAsync(
            //        maximumAge: TimeSpan.FromMinutes(5),
            //        timeout: TimeSpan.FromSeconds(10));

            //    userPosition.Text = "Your position is " + pos.Coordinate.Point.Position.Latitude.ToString("0.00") + " lat & " + pos.Coordinate.Point.Position.Longitude.ToString("0.00") + " lon.";
            //    MapLocationFinderResult result = await MapLocationFinder.FindLocationsAsync("7 rue guilleminot",  pos.Coordinate.Point, 10);
            //    if (result.Status == MapLocationFinderStatus.Success)
            //    {
            //        foreach (var elem in result.Locations)
            //        {
            //            dinerInfo.Add(new ListItem(elem.Address.District.ToString()));
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    if ((uint)ex.HResult == 0x80004004)
            //    {
            //        userPosition.Text = "Location is disabled.";
            //        Frame.Navigate(typeof(MainMenu));
            //    }
            //    else
            //    {

            //        // Fill DinerList with Restaurants found on GoogleMaps
            //        dinerInfo.Add(new ListItem("McDonalds"));
            //        dinerInfo.Add(new ListItem("Pizza Hut"));
            //    }
            //}
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
