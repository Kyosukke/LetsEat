﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace LetsEat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConnectionMenu : Page
    {
        MessageDialog dial;
        //private NavigationHelper navigationHelper { get; set; }

        public ConnectionMenu()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private async void connect_Click(object sender, RoutedEventArgs e)
        {
            bool isSuccess = false;
            string email = this.email.Text;
            string password = this.password.Password;
            UserRP res = null;

            if (email != "" && password != "")
            {
                UserVM user = new UserVM(email, password, email.Split(new[] { '@' })[0]);
                res = await ApiCall.MakeCall("connect", user);

                isSuccess = res.success;
            }

            if (isSuccess)
            {
                GlobalData.email = email;
                GlobalData.token = (res == null) ? ("") : (res.token);

                dial = new MessageDialog("Connection success !");
                await dial.ShowAsync();
                Frame.Navigate(typeof(MainMenu));
            }
            else
            {
                dial = new MessageDialog("Error: Wrong email/Password. Please try again !");
                await dial.ShowAsync();
            }
        }

        private async void signIn_Click(object sender, RoutedEventArgs e)
        {
            bool isSuccess = false;
            string email = createEmail.Text;
            string password = "";
            if (createPassword.Password == verifyPassword.Password)
            {
                password = createPassword.Password;
            }

            if (email != "" && password != "")
            {
                UserVM user = new UserVM(email, password, email.Split(new[] { '@' })[0]);
                UserRP res = await ApiCall.MakeCall("subscribe", user);

                isSuccess = res.success;
            }

            if (isSuccess)
            {
                dial = new MessageDialog("Created account successfully !");
            }
            else
            {
                dial = new MessageDialog("Sorry, account could not be created ! Use another email.");
            }
            await dial.ShowAsync();
        }
    }
}
