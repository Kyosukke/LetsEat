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

        Group group;

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
            group = e.Parameter as Group;

            GlobalData.groupeID = group._id;
            groupName.Header = group.name;
        }

        private void groupMember_Loaded(object sender, RoutedEventArgs e)
        {
            groupMember.ItemsSource = UserList;

            foreach (Member m in group.members)
            {
                UserList.Add(new ListItem(m._id));
            }
        }

        private async void history_Loaded(object sender, RoutedEventArgs e)
        {
            history.ItemsSource = HistoryList;

            GetRestaurantVM diner = new GetRestaurantVM();

            diner.groupeID = group._id;
            GetRestaurantRP res = await ApiCall.MakeCall("getRestaurant", diner);

            if (res.success && res.history != null)
            {
                foreach (Restauran r in res.history.restaurants)
                {
                    HistoryList.Add(new ListItem(r.name + " " + r.date));
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
                service.groupeID = group._id;

                AddMemberRP res = await ApiCall.MakeCall("addMember", service);

                if (res.success)
                {
                    dial = new MessageDialog("Add user success");
                    await dial.ShowAsync();
                    UserList.Add(new ListItem(c.popupBox.Text));
                }
                else
                {
                    dial = new MessageDialog("Error: " + res.message);
                    await dial.ShowAsync();
                }
                popup.IsOpen = false;
            };
        }

        private void searchDinerPlace_Clicked(object sender, RoutedEventArgs e)
        {
            GlobalData.groupeNumber = UserList.Count();
            Frame.Navigate(typeof(DinerListMenu), group);
        }

        private async void groupMember_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (group.admin != GlobalData.id)
                return;

            MessageDialog dial = new MessageDialog("Do you want to delete this user?");
            dial.Commands.Add(new UICommand("no"));
            dial.Commands.Add(new UICommand("yes"));
            var result = await dial.ShowAsync();
            if (result.Label == "yes")
            {
                ListItem item = (ListItem)groupMember.SelectedItem;
                DeleteMemberVM service = new DeleteMemberVM();

                service.groupeID = group._id;
                service.email = item.title;

                DeleteMemberRP res = await ApiCall.MakeCall("deleteMember", service);

                if (res.success)
                    UserList.Remove(item);
            }
        }
    }
}
