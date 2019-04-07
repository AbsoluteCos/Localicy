using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LocalicyDesktop
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        User user;

        public MainPage()
        {
            this.InitializeComponent();
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            Window.Current.SetTitleBar(TitleBar);
            lvNavigationMenu.SelectionChanged += Navigation_Change;
        }

        /*private async void ShowStuff(User user)
        {
            if (user != null) { var dialog = new MessageDialog(user.Name); await dialog.ShowAsync(); }
        }*/


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            user = DBConn.GetUser((int)e.Parameter);
            frMain.Content = new Profile(user);
            //ShowStuff(user);
        }

        private void toggleMenu()
        {
            Thickness closed = new Thickness(-400, 0, 0, 0);
            if (spSlide.Margin == closed)
                spSlide.Margin = new Thickness(0);
            else
                spSlide.Margin = closed;
        }

        private void Navigation_Click(object sender, RoutedEventArgs e)
        {
            toggleMenu();
        }

        private void Navigation_Change(object sender, SelectionChangedEventArgs e)
        {
            ListViewItem lvi = (ListViewItem)lvNavigationMenu.SelectedItem;
            switch (lvi.Name)
            {
                case "lviP":
                    frMain.Content = new Profile(user);
                    break;
                case "lviA":
                    frMain.Content = new Areas();
                    break;
                case "lviC":
                    frMain.Content = new Community(user);
                    break;
                default:
                    throw new Exception();
            }
            toggleMenu();
        }
    }
}
