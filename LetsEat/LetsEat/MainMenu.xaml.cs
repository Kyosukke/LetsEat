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
using LetsEat.Class;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace LetsEat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainMenu : Page
    {
        ObservableCollection<ListItem> items = new ObservableCollection<ListItem>();
        List<Group> groups = null;

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

            ProfilRP res = await ApiCall.MakeCall<ProfilVM, ProfilRP>("profil", user);

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

            GroupRP res = await ApiCall.MakeCall<GroupVM, GroupRP>("myGroups", group);

            if (res.success)
            {
                groups = res.groups;
                foreach (Group g in groups)
                {
                    items.Add(new ListItem(g.name));
                }
            }
        }

        private async void edit_Click(object sender, RoutedEventArgs e)
        {
            bool isSuccess = false;

            MessageDialog result;

            if (username.Text != "" && email.Text != "" && password.Password != "")
            {
                UserVM user = new UserVM(email.Text, password.Password, username.Text);
                user.userID = GlobalData.id;
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
            Popup popup = new Popup();
            popup.Height = 300;
            popup.Width = 400;
            popup.VerticalOffset = 100;
            PopUP control = new PopUP();
            popup.Child = control;
            popup.IsOpen = true;
            popup.HorizontalAlignment = HorizontalAlignment.Center;
            popup.VerticalAlignment = VerticalAlignment.Center;
            Button btnOK = (global::Windows.UI.Xaml.Controls.Button)control.FindName("btnOK");
            TextBox txt_Remarks = (global::Windows.UI.Xaml.Controls.TextBox)control.FindName("tbx");
            MessageDialog result;
            btnOK.Click += async (s, args) =>
            {
               popup.IsOpen = false;
               string t = txt_Remarks.Text;
                CreateGroupVM group = new CreateGroupVM();

               if (t != "")
                {
                    group.email = GlobalData.email;
                   group.name = t;

                    CreateGroupRP res = await ApiCall.MakeCall<CreateGroupVM, CreateGroupRP>("createGroup", group);

                    if (res.success)
                    {
                        result = new MessageDialog("Your group was created successfully !");
                        await result.ShowAsync();
                        listView_Loaded(s, args);
                    }
                    popup.IsOpen = false;
                }
                else
                {
                    result = new MessageDialog("Error: The group wasn't able to be created.");
                    await result.ShowAsync();
                }
            };
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedIndex > -1)
            {
                MessageDialog dial = new MessageDialog("Do you want to delete this group?");
                dial.Commands.Add(new UICommand("no"));
                dial.Commands.Add(new UICommand("yes"));
                var result = await dial.ShowAsync();
                if (result.Label == "yes")
                {
                    int i = listView.SelectedIndex;
                    string name = items[i].title;

                    DeleteGroupVM service = new DeleteGroupVM();

                    foreach (Group g in groups)
                    {
                        if (g.name == name)
                        {
                            service.groupeID = g._id;

                            DeleteGroupRP res = await ApiCall.MakeCall<DeleteGroupVM, DeleteGroupRP>("deleteGroup", service);

                            if (res.success)
                                items.Remove(items[i]);
                            break;
                        }
                    }
                }
            }
        }



        private void l_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            string name = ((ListItem)listView.SelectedItem).title;

            foreach (Group g in groups)
            {
                if (g.name.Equals(name))
                Frame.Navigate(typeof(GroupMenu), g);
            }
        }
    }
}
