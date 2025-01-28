using iNKORE.UI.WPF.Modern;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using Page = iNKORE.UI.WPF.Modern.Controls.Page;

namespace NavigationViewExample.Pages
{
    /// <summary>
    /// Interaction logic for AppsPage.xaml
    /// </summary>
    public partial class MainControlPage : Page
    {
        public MainControlPage()
        {
            InitializeComponent();
            NvSetPage.Theme_MainControl_New += Theme_png_darkOrlight;
        }
        private void Theme_png_darkOrlight()
        {
            Page_Loaded(null, null);
        }
        //
        public delegate void Media_();
        public static event Media_ Media_Do;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Media_Do?.Invoke();
        }
        //让Lrc.xaml...绑定
        public delegate void LRC_();
        public static event LRC_ LRC_F;
        private void Button_Click_old(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Click_next(object sender, RoutedEventArgs e)
        {

        }

        private void Page_Loaded(object? sender, RoutedEventArgs? e)
        {
            string path=$"{Environment.CurrentDirectory}" + "\\AppD\\setting.txt";
            List<string> setting_homelist = File.ReadAllText(path).Split("${@}").ToList();
            if (setting_homelist[1] == "dark")
            {
                ControlPS.Source = new BitmapImage(new Uri("/photo/light/play.png", UriKind.Relative));
                ControlOld.Source = new BitmapImage(new Uri("/photo/light/old.png", UriKind.Relative));
                ControlNext.Source = new BitmapImage(new Uri("/photo/light/next.png", UriKind.Relative));
            }
            else if (setting_homelist[1] == "light")
            {
                ControlPS.Source = new BitmapImage(new Uri("/photo/dark/play.png", UriKind.Relative));
                ControlOld.Source = new BitmapImage(new Uri("/photo/dark/old.png", UriKind.Relative));
                ControlNext.Source = new BitmapImage(new Uri("/photo/dark/next.png", UriKind.Relative));
            }
        }

        private void Button_Click_list_ing(object sender, RoutedEventArgs e)
        {

        }
        private int T4 = 0;
        private void SD_1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int a = (int)SD_1.Value;
            string a2 = a.ToString();
            T6.Text = a2;
        }
    }
}
