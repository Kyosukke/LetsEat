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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace LetsEat
{
    public sealed partial class CustomPopupControl : UserControl
    {
        public CustomPopupControl()
        {
            this.InitializeComponent();
        }

        public void linkParent(Popup popup)
        {
            Popup p = new Popup();
            popup.Height = 200;
            popup.Width = 400;
            popup.VerticalOffset = 100;
            popup.Child = this;
        }
    }
}
