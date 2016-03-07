using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Windows.UI.Popups;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using System.Windows;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace LetsEat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainMenu : Page
    {
        ObservableCollection<ListItem> items = new ObservableCollection<ListItem>();

        public MainMenu()
        {
            this.InitializeComponent();

            updateProfile();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            var settings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            if (settings.Values.ContainsKey("LocationConsent"))
            {
                return;
            }
            else
            {
                MessageDialog result = new MessageDialog("The application wants to access your location. Is that ok ?");

                result.Commands.Add(new UICommand("OK", new UICommandInvokedHandler(CommandHandlers)));
                result.Commands.Add(new UICommand("Cancel", new UICommandInvokedHandler(CommandHandlers)));

                await result.ShowAsync();
            }
        }

        public void CommandHandlers(IUICommand command)
        {
            var Actions = command.Label;
            var settings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            switch (Actions)
            {
                case "OK":
                    settings.Values["LocationConsent"] = true;
                    break;
                case "Cancel":
                    settings.Values["LocationConsent"] = false;
                    break;
                default:
                    break;
            }
        }

        public async void updateProfile()
        {
            ProfilVM user = new ProfilVM();

            user.email = GlobalData.email;

            ProfilRP res = await ApiCall.MakeCall("profil", user);

            if (res.success)
            {
                username.Text = res.user.pseudo;
                email.Text = res.user.email;
                GlobalData.id = res.user._id;
            }
        }

        private async void listView_Loaded(object sender, RoutedEventArgs e)
        {
            listView.ItemsSource = items;

            GroupVM group = new GroupVM();

            group.email = GlobalData.email;

            GroupRP res = await ApiCall.MakeCall("myGroups", group);

            if (res.success)
            {
                foreach (Group g in res.groups)
                {
                    items.Add(new ListItem(g.name));
                }
            }
        }

        private void listView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(GroupMenu), e.ClickedItem);
        }

        private async void edit_Click(object sender, RoutedEventArgs e)
        {
            bool isSuccess = false;

            MessageDialog result;

            if (username.Text != "" && email.Text != "" && password.Password != "")
            {
                UserVM user = new UserVM(email.Text, password.Password, username.Text);
                user._id = GlobalData.id;
                UserRP res = await ApiCall.MakeCall("editProfil", user);

                isSuccess = res.success;
            }

            if (isSuccess)
            {
                result = new MessageDialog("Your account has been modified successfully !");
            }
            else
            {
                result = new MessageDialog("Error: Your information couldn't be modified.");
            }
            await result.ShowAsync();
        }

        private void addGroup_Clicked(object sender, RoutedEventArgs e)
        {
            CustomPopupControl c = new CustomPopupControl();
            c.linkParent(popup);
            popup.IsOpen = true;
            MessageDialog result;

            c.popupText.Text = "Choose a group name:";
            c.popupBox.Visibility = Visibility.Visible;

            c.popupButton.Click += async (s, args) =>
            {
                CreateGroupVM group = new CreateGroupVM();

                if (c.popupBox.Text != "")
                {
                    group.email = GlobalData.email;
                    group.name = c.popupBox.Text;

                    CreateGroupRP res = await ApiCall.MakeCall("createGroup", group);

                    if (res.success)
                    {
                        result = new MessageDialog("Your group was created successfully !");
                        await result.ShowAsync();
                    }

                    popup.IsOpen = false;
                    Frame.Navigate(typeof(GroupMenu), c.popupBox.Text);
                }
                else
                {
                    result = new MessageDialog("Error: Your information couldn't be modified.");
                    await result.ShowAsync();
                }
            };
        }

        private async void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageDialog dial = new MessageDialog("Do you want to delete this user?");
            await dial.ShowAsync();
            Frame.Navigate(typeof(GroupMenu));
        }

        private async void listView_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            MessageDialog dial = new MessageDialog("Do you want to delete this user?");
            dial.Commands.Add(new UICommand("no"));
            dial.Commands.Add(new UICommand("yes"));
            var result = await dial.ShowAsync();
            if (result.Label == "yes")
            {
                ListItem item = (ListItem)listView.SelectedItem;
                items.Remove(item);
            }
        }
    }
}
