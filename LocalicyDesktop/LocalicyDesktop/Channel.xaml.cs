using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Text;
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
    public sealed partial class Channel : Page
    {
        int id;
        List<Messages> hist;
        User user;

        public Channel(User user, int id)
        {
            this.InitializeComponent();
            this.id = id;
            this.user = user;
            hist = DBConn.GetMessages(user.State, id);
            init();
        }

        private async void ShowStuff(string mess)
        {
            if (user != null) { var dialog = new MessageDialog(mess); await dialog.ShowAsync(); }
        }

        private void init()
        {
            foreach(Messages m in hist)
            {
                //ShowStuff(m.Message);
                StackPanel sp = new StackPanel();
                sp.BorderBrush = new SolidColorBrush(Colors.Black);
                sp.BorderThickness = new Thickness(5);
                sp.Margin = new Thickness(10);
                TextBlock namae = new TextBlock();
                namae.Text = m.Name;
                namae.Foreground = new SolidColorBrush(Colors.Black);
                namae.FontWeight = FontWeights.Bold;
                sp.Children.Add(namae);
                TextBlock messago = new TextBlock();
                messago.Text = m.Message;
                messago.Foreground = new SolidColorBrush(Colors.Black);
                sp.Children.Add(messago);
                spParent.Children.Add(sp);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //post message to sql database and add to the stack panel
            if (tbMessage.Text != "")
            {
                DBConn.PostMessage(user, tbMessage.Text, id);
                StackPanel sp = new StackPanel();
                sp.BorderBrush = new SolidColorBrush(Colors.Black);
                sp.BorderThickness = new Thickness(5);
                sp.Margin = new Thickness(10);
                TextBlock namae = new TextBlock();
                namae.Text = user.Name;
                namae.Foreground = new SolidColorBrush(Colors.Black);
                namae.FontWeight = FontWeights.Bold;
                sp.Children.Add(namae);
                TextBlock messago = new TextBlock();
                messago.Text = tbMessage.Text;
                messago.Foreground = new SolidColorBrush(Colors.Black);
                sp.Children.Add(messago);
                spParent.Children.Add(sp);
                tbMessage.Text = "";
            }
        }
    }
}
