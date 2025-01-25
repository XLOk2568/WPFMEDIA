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

        public int setting_homelist_count = 0;
        public MainWindow()
        {
            InitializeComponent();
        }
        //这里说下方案，在主页弄两个frame，一个MainNv是用来作为用户主要的交互导航，另一个MainMini则是用来作为用户的进度条等等播放控制
        public Pages.MainNvPage PageNv_Load= new Pages.MainNvPage();
        public Pages.MainControlPage PageControl_Load = new Pages.MainControlPage();

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
                List<string> setting_homelist = File.ReadAllText(setting_path).Split("${@}").ToList();
                setting_homelist_count = setting_homelist.Count;
                List<string> tall2 = setting_homelist[0].Split(",").ToList();//读取和设置屏幕
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
            }
            Frame_Main_MainNv.Navigate(PageNv_Load);
            Frame_Main_MainControl.Navigate(PageControl_Load);
        }
        private void _Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 弹窗提示是否确定要退出
            MessageBoxResult f1 = iNKORE.UI.WPF.Modern.Controls.MessageBox.Show("你确定退出?", "Double check", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (f1 == MessageBoxResult.OK)
            {
                //文本文件中的第一行内容获取，并且被设置为app的屏幕值
                //保存软件的屏幕位置和大小后退出
                List<string> setting_homelist = File.ReadAllText(setting_path).Split("${@}").ToList();
                string less = setting_homelist[0];
                string iftxt = Height + "," + Width + "," + Left + "," + Top;
                if (less == iftxt)
                {
                    Process.GetCurrentProcess().Kill();
                }
                else if (less!= iftxt)
                {
                    StreamWriter sm1 = new StreamWriter(setting_path);
                    int Count = setting_homelist_count - 2;
                    for (int T0 = 1; T0 !=Count; T0 += 1)
                    {
                        iftxt = iftxt + "${@}" + setting_homelist[T0];
                    }
                    iftxt = iftxt + "${@}" + setting_homelist[setting_homelist_count-1];
                    sm1.Write(iftxt);
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
