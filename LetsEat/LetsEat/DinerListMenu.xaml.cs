﻿using System;
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
            var settings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            if ((bool)settings.Values["LocationConsent"] != true)
            {
                return;
            }

            Geolocator geo = new Geolocator();
            geo.DesiredAccuracyInMeters = 50;

            try
            {
                Geoposition pos = await geo.GetGeopositionAsync(
                    maximumAge: TimeSpan.FromMinutes(5),
                    timeout: TimeSpan.FromSeconds(10));

                userPosition.Text = "Your position is " + pos.Coordinate.Point.Position.Latitude.ToString("0.00") + " lat & " + pos.Coordinate.Point.Position.Longitude.ToString("0.00") + " lon.";
                MapLocationFinderResult result = await MapLocationFinder.FindLocationsAsync("restaurant",  pos.Coordinate.Point, 10);
                if (result.Status == MapLocationFinderStatus.Success)
                {
                    foreach (var elem in result.Locations)
                    {
                        dinerInfo.Add(new ListItem(elem.DisplayName));
                    }
                }

            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x80004004)
                {
                    userPosition.Text = "Location is disabled.";
                    Frame.Navigate(typeof(MainMenu));
                }
                else
                {

                    // Fill DinerList with Restaurants found on GoogleMaps
                    dinerInfo.Add(new ListItem("McDonalds"));
                    dinerInfo.Add(new ListItem("Pizza Hut"));
                }
            }
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
            Frame.Navigate(typeof(DinerMenu), dinerList.SelectedValue);
        }
    }
}
