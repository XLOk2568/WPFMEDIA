using ABI.System.Collections.Generic;
using iNKORE.UI.WPF.Modern.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Runtime.ConstrainedExecution;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Windows.Media.Playback;
using System;
using System.Threading.Tasks;
using Page = iNKORE.UI.WPF.Modern.Controls.Page;
using MediaPlayer = System.Windows.Media.MediaPlayer;

namespace NavigationViewExample.Pages
{
    /// <summary>
    /// Interaction logic for AppsPage.xaml
    /// </summary>
    public partial class VedioPage : Page
    {
        public VedioPage()
        {
            InitializeComponent();
            MainControlPage.Media_Do+= Start_Play;
        }
        private void Start_Play()
        {
            InterfaceLanguage = App.InterfaceLanguage_All;
            Media_Play(null,null);
        }
        public string InterfaceLanguage = App.InterfaceLanguage_All;
        public string Media_F = "";
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InterfaceLanguage = App.InterfaceLanguage_All;
         }
        //播放
        private void Media_Play(object? sender, RoutedEventArgs? e)
        {
            ring.Visibility = Visibility.Visible;
            if (Media_F != "" && File.Exists(Media_F))
            {            
                OnLoadAndPlay();
            }
            else
            {
                ring.Visibility = Visibility.Collapsed;
                List<string> IL_list = File.ReadAllText(App.InterfaceLanguage_All).Split("$@").ToList();
                iNKORE.UI.WPF.Modern.Controls.MessageBox.Show(IL_list[1], "No", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private async void OnLoadAndPlay()
        {
            await Task.Run(() =>
            {
                 Dispatcher.Invoke(() =>
                  {
                      Media_E.Source = new Uri(Media_F);                
                  });
            });
            ring.Visibility = Visibility.Collapsed;
            Media_E.Play();
        }
        //暂停
        private void Media_Pause(object sender, RoutedEventArgs e)
        {
            Media_E.Pause();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
