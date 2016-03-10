using LetsEat.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace LetsEat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DinerMenu : Page
    {
        Answer a;

        public DinerMenu()
        {
            this.InitializeComponent();

            //dinerName.Text = "MY Diner";
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected  async override void OnNavigatedTo(NavigationEventArgs e)
        {
            a = e.Parameter as Answer;
            restaurantName.Text = a.name;

            Geolocator geo = new Geolocator();
            geo.DesiredAccuracyInMeters = 1;

            Geoposition pos = await geo.GetGeopositionAsync(
                maximumAge: TimeSpan.FromMinutes(5),
                timeout: TimeSpan.FromSeconds(10));


            //userPosition.Text = "Your position is " + pos.Coordinate.Point.Position.Latitude.ToString("0.00") + " lat & " + pos.Coordinate.Point.Position.Longitude.ToString("0.00") + " lon.";
            MapLocationFinderResult result = await MapLocationFinder.FindLocationsAsync(a.adresse, pos.Coordinate.Point, 2);
            if (result.Status == MapLocationFinderStatus.Success)
            {
                myMap.Center = result.Locations[0].Point;
                myMap.ZoomLevel = 16;

                //AJOUT DE POINT
                Ellipse myCircle = new Ellipse();
                myCircle.Fill = new SolidColorBrush(Colors.Blue);
                myCircle.Height = 20;
                myCircle.Width = 20;
                myCircle.Opacity = 50;
                Ellipse myCircle2 = new Ellipse();
                myCircle2.Fill = new SolidColorBrush(Colors.Red);
                myCircle2.Height = 20;
                myCircle2.Width = 20;
                myCircle2.Opacity = 50;
                MapControl.SetLocation(myCircle, result.Locations[0].Point);
                myMap.Children.Add(myCircle);
                MapControl.SetLocation(myCircle2, pos.Coordinate.Point);
                myMap.Children.Add(myCircle2);
        }
        }

        private async void CheckRandom()
        {
            CanRandomVM service = new CanRandomVM();

            service.groupeID = GlobalData.groupeID;
            service.numberMembre = GlobalData.groupeNumber;

            CanRandomRP res = await ApiCall.MakeCall("canRandom", service);

            if (res.success)
            {
                Random rdm = new Random();
                int i = rdm.Next();

                i = i % res.objet.answers.Count();

                Answer final = res.objet.answers.ElementAt(i);
                MessageDialog dial = new MessageDialog("The choice is: " + final.name);
                await dial.ShowAsync();
                ValidateDiner(final);
            }
        }

        private async void ValidateDiner(Answer final)
        {
            AddRestaurantVM service = new AddRestaurantVM();

            service.groupeID = GlobalData.groupeID;
            service.restaurantName = final.name;
            service.date = DateTime.Now.ToString();

            AddRestaurantRP res = await ApiCall.MakeCall("addRestaurant", service);

            if (res.success)
            {
                // Send email
                MessageDialog dial = new MessageDialog("Choice sent !");
                await dial.ShowAsync();
            }
        }

        private void call_Clicked(object sender, RoutedEventArgs e)
        {
            Windows.ApplicationModel.Calls.PhoneCallManager.ShowPhoneCallUI(a.number, a.name);
        }

        private async void go_Click(object sender, RoutedEventArgs e)
        {
            RestaurantChoiceVM service = new RestaurantChoiceVM();

            service.email = GlobalData.email;
            service.groupeID = GlobalData.groupeID;
            service.restaurantName = a.name;
            service.restaurantAdresse = a.adresse;
            service.restaurantNumber = a.number;

            RestaurantChoiceRP res = await ApiCall.MakeCall("restaurantChoice", service);

            if (res.success)
            {
                MessageDialog dial = new MessageDialog("Your choice has been taken.");
                await dial.ShowAsync();
                CheckRandom();
            }
            else
            {
                MessageDialog dial = new MessageDialog("Error: Your choice has already been taken.");
                await dial.ShowAsync();
            }

            //ITINERAIRE
            //BasicGeoposition startLocation = new BasicGeoposition();
            //startLocation.Latitude = pos.Coordinate.Latitude;
            //startLocation.Longitude = pos.Coordinate.Longitude;
            //Geopoint startPoint = new Geopoint(startLocation);
            //BasicGeoposition endLocation = new BasicGeoposition();
            //endLocation.Latitude = result.Locations[0].Point.Position.Latitude;
            //endLocation.Longitude = result.Locations[0].Point.Position.Longitude;
            //Geopoint endPoint = new Geopoint(endLocation);
            //// Get the route between the points.
            //MapRouteFinderResult routeResult =
            //await MapRouteFinder.GetDrivingRouteAsync(
            //  startPoint,
            //  endPoint,
            //  MapRouteOptimization.Time,
            //  MapRouteRestrictions.None,
            //  290);
            //Frame.Navigate(typeof(Map), a);
        }
    }
}
