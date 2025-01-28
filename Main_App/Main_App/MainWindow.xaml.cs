using iNKORE.UI.WPF.Modern;
using iNKORE.UI.WPF.Modern.Controls;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
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
        //setting.txt
        //749,1266,258,123${@}dark${@}CN${@}firsttime,newtime${@}playtime,palyalist
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
            App.settings_ = File.ReadAllText(App.setting_path);
            List<string> setting_homelist = App.settings_.Split("${@}").ToList();
            setting_homelist_count = setting_homelist.Count;
            //读取和设置屏幕 
            List<string> tall2 = setting_homelist[0].Split(",").ToList();                                                            
            Double happ = Double.Parse(tall2[0]);
            Height = happ;
            Double wapp = Double.Parse(tall2[1]);
            Width = wapp;
            Double lapp = Double.Parse(tall2[2]);
            Left = lapp;
            Double tapp = Double.Parse(tall2[3]);
            Top = tapp;
            //设置主题          
            List<string> theme = App.settings_.Split("${@}").ToList();                                                                        
            if (setting_homelist[1] == "dark")
            {
                ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
            }
            else if (setting_homelist[1] == "light")
            {
                   ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
            }
            //设置语言
            if (setting_homelist[2] == "CN") //中
            {
                App.InterfaceLanguage_All = File.ReadAllText(App.Run_Time_Path + "\\Language_S\\CN.txt");
                App.IL_all_list = App.InterfaceLanguage_All.Split("${@}").ToList();
            }
            else if (setting_homelist[2] == "EN")//英文
            {
                App.InterfaceLanguage_All = File.ReadAllText(App.Run_Time_Path + "\\Language_S\\EN.txt");
                App.IL_all_list = App.InterfaceLanguage_All.Split("${@}").ToList();
            }
            if (setting_homelist[2] == "JP")//日语
            {
                App.InterfaceLanguage_All = File.ReadAllText(App.Run_Time_Path + "\\Language_S\\JP.txt");
                App.IL_all_list = App.InterfaceLanguage_All.Split("${@}").ToList();
            }
            if (setting_homelist[2] == "FR")//法语
            {
                App.InterfaceLanguage_All = File.ReadAllText(App.Run_Time_Path + "\\Language_S\\FR.txt");
                App.IL_all_list = App.InterfaceLanguage_All.Split("${@}").ToList();
            }
            Frame_Main_MainNv.Navigate(PageNv_Load);
            Frame_Main_MainControl.Navigate(PageControl_Load);
        }
        private async void Load_IL()
        {
            await Task.Run(() =>
            {

            });
        }
        private void _Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 弹窗提示是否确定要退出
            MessageBoxResult f1 = iNKORE.UI.WPF.Modern.Controls.MessageBox.Show(App.IL_all_list[0], App.IL_all_list[0], MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (f1 == MessageBoxResult.OK)
            {
                //文本文件中的第一行内容获取，并且被设置为app的屏幕值
                //保存软件的屏幕位置和大小后退出
                List<string> setting_homelist = File.ReadAllText(App.setting_path).Split("${@}").ToList();
                string less = setting_homelist[0];
                string iftxt = Height + "," + Width + "," + Left + "," + Top;
                if (less == iftxt)
                {
                    Process.GetCurrentProcess().Kill();
                }
                else if (less!= iftxt)
                {
                    int TrueCount = setting_homelist.Count;
                    string Data = iftxt;
                    for (int for_ = 1; for_ != TrueCount; for_ += 1)
                    {
                        Data = Data + "${@}" + setting_homelist[for_];
                    }
                    StreamWriter sm1 = new StreamWriter(App.setting_path);
                    sm1.Write(Data);
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