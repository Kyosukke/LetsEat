using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
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
using Windows.Services.Maps;
using System.Threading.Tasks;
using LetsEat.Class;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace LetsEat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DinerListMenu : Page
    {
        ObservableCollection<ListItem> dinerInfo = new ObservableCollection<ListItem>();
        Group group;
        List<Answer> ans = new List<Answer>();

        public DinerListMenu()
        {
            this.InitializeComponent();
        }
        void DoMyList()
        {
            Answer a = new Answer();

            a.name = "Au Porte Bonheur";
            a.adresse = "78 Boulevard de Stalingrad, 94400";
            a.number = "0146720238";
            ans.Add(a);
            a = new Answer();
            a.name = "Amarante";
            a.adresse = "89 Boulevard de Stalingrad, 94400 Vitry-sur-Seine";
            a.number = "0146711917";
            ans.Add(a);
            a = new Answer();
            a.name = "Shiba's France";
            a.adresse = "95 Rue Jules Lagaisse, 94400";
            a.number = "0146711053";
            ans.Add(a);
            foreach (Answer i in ans)
            {
                dinerInfo.Add(new ListItem(i.name));
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            group = e.Parameter as Group;
            DoMyList();
            dinerList.ItemsSource = dinerInfo;
        }

        private void dinerList_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void dinerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Answer i in ans)
            {
                if (i.name == ((ListItem)dinerList.SelectedValue).title)
                Frame.Navigate(typeof(DinerMenu), i);
            }
        }
    }
}
