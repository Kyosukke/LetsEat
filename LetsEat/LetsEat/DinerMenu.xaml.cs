using LetsEat.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            a = e.Parameter as Answer;
            restaurantName.Text = a.name;
        }

        private void call_Clicked(object sender, RoutedEventArgs e)
        {
            Windows.ApplicationModel.Calls.PhoneCallManager.ShowPhoneCallUI("0123456789", restaurantName.Text);
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
                MessageDialog dial = new MessageDialog("Your choice as been taken.");
                await dial.ShowAsync();
            }

            Frame.Navigate(typeof(Map), a);
        }
    }
}
