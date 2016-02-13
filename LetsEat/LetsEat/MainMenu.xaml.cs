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
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void listView_Loaded(object sender, RoutedEventArgs e)
        {
            listView.ItemsSource = items;
            items.Add(new ListItem("Test"));
            items.Add(new ListItem("Hello"));
        }

        private void listView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(GroupMenu), e.ClickedItem);
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private   void addGroup_Clicked(object sender, RoutedEventArgs e)
        {
            MessageDialog dial = new MessageDialog("Which user do you want to add?");
        }

         private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ListItem item = (ListItem)listView.SelectedItem;
            //MessageDialog p = new MessageDialog(item.title);
            //p.ShowAsync();
        }

    }
}
