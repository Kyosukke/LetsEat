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
using Windows.Devices.Geolocation;
using LetsEat.Class;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace LetsEat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GroupMenu : Page
    {
        ObservableCollection<ListItem> UserList = new ObservableCollection<ListItem>();
        ObservableCollection<ListItem> HistoryList = new ObservableCollection<ListItem>();
        MessageDialog dial;
        public GroupMenu()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            groupName.Header = e.Parameter as string;
        }

        private void groupMember_Loaded(object sender, RoutedEventArgs e)
        {
            groupMember.ItemsSource = UserList;

            GroupVM group = new GroupVM();

            group.email = GlobalData.email;

            //GroupRP res = await ApiCall.MakeCall("myGroups", group);

            //if (res.success)
            //{
            //    foreach (Group g in res.groups)
            //    {
            //        items.Add(new ListItem(g.name));
            //    }
            //}
        }

        private async void history_Loaded(object sender, RoutedEventArgs e)
        {
            history.ItemsSource = HistoryList;

            GetRestaurantVM diner = new GetRestaurantVM();

            diner.groupeID = "test";
            GetRestaurantRP res = await ApiCall.MakeCall("getRestaurant", diner);

            if (res.success && res.history.FirstOrDefault() != null)
            {
                foreach (Restauran r in res.history.FirstOrDefault().restaurants)
                {
                    HistoryList.Add(new ListItem(r.name));
                }
            }
        }

        private void addMember_Clicked(object sender, RoutedEventArgs e)
        {
            CustomPopupControl c = new CustomPopupControl();
            c.linkParent(popup);
            popup.IsOpen = true;

            c.popupText.Text = "Choose a username name:";
            c.popupBox.Visibility = Visibility.Visible;

            c.popupButton.Click += async (s, args) =>
            {
                AddMemberVM service = new AddMemberVM();

                service.email = c.popupBox.Text;
                service.groupeID = "test";

                AddMemberRP res = await ApiCall.MakeCall("addMember", service);

                if (res.success)
                {
                    dial = new MessageDialog("Add user success");
                    await dial.ShowAsync();
                    UserList.Add(new ListItem(c.popupBox.Text));
                }
                else
                {
                    dial = new MessageDialog("Error: User not found.");
                    await dial.ShowAsync();
                }
                popup.IsOpen = false;
            };
        }

        private void searchDinerPlace_Clicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(DinerListMenu), groupName.Header);
        }

        private void groupMember_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private async void groupName_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            MessageDialog dial = new MessageDialog("Do you want to delete this user?");
            dial.Commands.Add(new UICommand("no"));
            dial.Commands.Add(new UICommand("yes"));
            var result = await dial.ShowAsync();
            if (result.Label == "yes")
            {
                ListItem item = (ListItem)groupMember.SelectedItem;
                UserList.Remove(item);
            }
        }

        private void groupMember_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {

        }
    }
}
