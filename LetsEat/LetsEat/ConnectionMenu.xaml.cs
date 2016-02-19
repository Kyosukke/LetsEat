using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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

        private void connect_Click(object sender, RoutedEventArgs e)
        {
            string email = this.email.Text;
            string password = this.password.Password;

            // WS: connectAccount();

            if (true)// if success
            {
                Frame.Navigate(typeof(MainMenu));
            }
            else
            {
                CustomPopupControl c = new CustomPopupControl();
                c.linkParent(popup);
                popup.IsOpen = true;

                c.popupText.Text = "Error: Wrong email/Password. Please try again !";

                c.popupButton.Click += (s, args) =>
                {
                    popup.IsOpen = false;
                };
            }
        }

        private void signIn_Click(object sender, RoutedEventArgs e)
        {
            bool isSuccess = true;
            string email = this.createEmail.Text;
            string password = "";
            if (this.password.Password == this.verifyPassword.Password)
            {
                password = this.password.Password;
            }

            // WS: isSuccess = createAccount();

            CustomPopupControl c = new CustomPopupControl();
            c.linkParent(popup);
            popup.IsOpen = true;

            if (isSuccess)
            {
                c.popupText.Text = "Created account successfully !";
            }
            else
            {
                c.popupText.Text = "Sorry, account could not be created ! Use another email.";
            }

            c.popupButton.Click += (s, args) =>
            {
                popup.IsOpen = false;
                if (isSuccess)
                    Frame.Navigate(typeof(MainMenu));
            };

        }
    }
}
