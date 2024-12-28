using iNKORE.UI.WPF.Modern;
using iNKORE.UI.WPF.Modern.Controls;

using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Page = iNKORE.UI.WPF.Modern.Controls.Page;

namespace NavigationViewExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        public string setting_path = $"{Environment.CurrentDirectory}" + "\\AppD\\setting.txt";//其他窗口也用这个
        public List<string> setting_homelist = new List<string>();
        public int setting_homelist_count = 0;
        public MainWindow()
        {
            InitializeComponent();
        }
        //NV控制页面切换
        public Pages.AppsPage Page_Apps = new Pages.AppsPage();
        public Pages.HomePage Page_Home = new Pages.HomePage();
        public Pages.NvSetPage Page_Set = new Pages.NvSetPage();
        public Pages.AboutPage About = new Pages.AboutPage();
        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var item = sender.SelectedItem;
            Page? page = null;

            if (item == NavigationViewItem_Home)
            {
                page = Page_Home;
            }
            else if (item == NvSet)
            {
                page = Page_Set;
            }
            else if (item == NavigationViewItem_Apps)
            {
                page = Page_Apps;
            }
            else if (item == Aboutb)
            {
                page = About;
            }
            if (page != null)
            {
                NavigationView_Root.Header = page.Title;
                Frame_Main.Navigate(page);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //判断是否存在
            if (!System.IO.File.Exists(setting_path))
            {
                StreamWriter sm1 = new StreamWriter(setting_path);
                sm1.Write(Height + "," + Width + "," + Left + "," + Top+"${@}dark");
                sm1.Close();
            }
            else if (System.IO.File.Exists(setting_path))
            {
                setting_homelist = File.ReadAllText(setting_path).Split("${@}").ToList();//设置列表
                setting_homelist_count = setting_homelist.Count;
                List<string> tall2 = File.ReadAllText(setting_path).Split(",").ToList();//读取和设置屏幕
                Double happ = Double.Parse(tall2[0]);
                Height = happ;
                Double wapp = Double.Parse(tall2[1]);
                Width = wapp;
                Double lapp = Double.Parse(tall2[2]);
                Left = lapp;
                Double tapp = Double.Parse(tall2[3]);
                Top = tapp;
                List<string> theme = File.ReadAllText(setting_path).Split("${@}").ToList();//设置主题
                if (setting_homelist[1] == "dark")
                {
                    ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
                }
                else if (setting_homelist[1] == "light")
                {
                    ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
                }
                else if (setting_homelist[1] == "null")
                {
                    ThemeManager.Current.ApplicationTheme = null;
                }
                NavigationView_Root.SelectedItem = NavigationViewItem_Home;

            }
        }
        private void _Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 弹窗提示是否确定要退出
            MessageBoxResult f1 = iNKORE.UI.WPF.Modern.Controls.MessageBox.Show("你确定退出?", "Double check", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (f1 == MessageBoxResult.OK)
            {
                //文本文件中的第一行内容获取，并且被设置为app的屏幕值
                //保存软件的屏幕位置和大小后退出
                string less = setting_homelist[0];
                string iftxt = Height + "," + Width + "," + Left + "," + Top;
                if (less == iftxt)
                {
                    Process.GetCurrentProcess().Kill();
                }
                else if (less!= iftxt)
                {
                    StreamWriter sm1 = new StreamWriter(setting_path);
                    string T1 = "";
                    for (int T0 = 0; 0 != setting_homelist_count; T0 += 1)
                    {
                        if (T0 != 0)
                        {
                            T1 = T1 + setting_homelist[T0];
                        }
                        else if (T0 == 0)
                        {
                            T1 = T1 + iftxt;
                        }
                    }
                    sm1.Write(T1);
                    sm1.Close();
                    Process.GetCurrentProcess().Kill();
                }
            }
            else
            {
                e.Cancel = true;
            }
        }
     
    }
}