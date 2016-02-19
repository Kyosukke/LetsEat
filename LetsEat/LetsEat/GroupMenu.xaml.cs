﻿using System;
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
        public GroupMenu()
        {
            this.InitializeComponent();
            groupName.Header = "Epitech";
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void groupMember_Loaded(object sender, RoutedEventArgs e)
        {
            groupMember.ItemsSource = UserList;

            UserList.Add(new ListItem("Olivier"));
            UserList.Add(new ListItem("Alexandre"));
            UserList.Add(new ListItem("Benjamin"));
        }

        private void history_Loaded(object sender, RoutedEventArgs e)
        {
            history.ItemsSource = HistoryList;

            HistoryList.Add(new ListItem("McDonalds"));
            HistoryList.Add(new ListItem("Pizza Hut"));
        }

        private void addMember_Clicked(object sender, RoutedEventArgs e)
        {
            CustomPopupControl c = new CustomPopupControl();
            c.linkParent(popup);
            popup.IsOpen = true;

            c.popupText.Text = "Choose a username name:";
            c.popupBox.Visibility = Visibility.Visible;

            c.popupButton.Click += (s, args) =>
            {
                // WS: addUserGroup(c.popupBox.Text, ...);

                popup.IsOpen = false;
            };
        }

        private void searchDinerPlace_Clicked(object sender, RoutedEventArgs e)
        {
            // WS: If admin, Send notification to all users

            ListPopupControl c = new ListPopupControl();
            c.linkParent(popup);
            popup.IsOpen = true;

            c.listView.Loaded += (s, args) =>
            {
                ObservableCollection<ListItem> DinerList = new ObservableCollection<ListItem>();

                c.listView.ItemsSource = DinerList;

                // Fill DinerList with Restaurants found on GoogleMaps

                DinerList.Add(new ListItem("McDonalds"));
                DinerList.Add(new ListItem("Pizza Hut"));
            };

            c.listView.ItemClick += (s, args) =>
            {
                // Diner choosen;
            };

            c.chooseDiner.Click += (s, args) =>
            {
                popup.IsOpen = false;
                Frame.Navigate(typeof(DinerMenu)); // , Diner choosen);
            };
        }

        private   void groupMember_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
    }
}
