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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LocalicyDesktop
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Community : Page
    {
        User user;

        public Community(User user)
        {
            this.InitializeComponent();
            lvChannels.SelectionChanged += ChannelChanged;
            this.user = user;
            frChannel.Content = new Channel(user, 1);
        }

        private void ChannelChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewItem curr = (ListViewItem)lvChannels.SelectedItem;
            frChannel.Content = new Channel(user, Int32.Parse(curr.Name.Substring(1)));
        }

        private void ListViewItem_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
