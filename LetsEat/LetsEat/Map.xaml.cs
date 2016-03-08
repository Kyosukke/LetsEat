using LetsEat.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Geolocation;
using Windows.UI.Popups;
using Windows.Services.Maps;
using Windows.UI.Xaml.Shapes;
using Windows.UI;
using Windows.UI.Xaml.Controls.Maps;
using LetsEat.Class;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace LetsEat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Map : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        Answer a;

        public Map()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }



        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            a = e.Parameter as Answer;
            title.Text = a.name;
            Geolocator geo = new Geolocator();
            geo.DesiredAccuracyInMeters = 1;

            Geoposition pos = await geo.GetGeopositionAsync(
                maximumAge: TimeSpan.FromMinutes(5),
                timeout: TimeSpan.FromSeconds(10));


            //userPosition.Text = "Your position is " + pos.Coordinate.Point.Position.Latitude.ToString("0.00") + " lat & " + pos.Coordinate.Point.Position.Longitude.ToString("0.00") + " lon.";
            MapLocationFinderResult result = await MapLocationFinder.FindLocationsAsync("7 rue guilleminot", pos.Coordinate.Point, 2);
            if (result.Status == MapLocationFinderStatus.Success)
            {
                myMap.Center = result.Locations[0].Point;
                myMap.ZoomLevel = 16;

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
                myMap.Children.Add(myCircle2);
                myMap.Children.Add(myCircle);
                MapControl.SetLocation(myCircle, result.Locations[0].Point);
                MapControl.SetLocation(myCircle2, pos.Coordinate.Point);
            }

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

            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {

            this.navigationHelper.OnNavigatedFrom(e);

        }

        #endregion
    }
}
