using iNKORE.UI.WPF.Modern;
using iNKORE.UI.WPF.Modern.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TagLib.Ape;
using Page = iNKORE.UI.WPF.Modern.Controls.Page;

namespace NavigationViewExample.Pages
{
    /// <summary>
    /// Interaction logic for GamesPage.xaml
    /// </summary>
    public partial class NvSetPage : Page
    {
        public NvSetPage()
        {
            InitializeComponent();
        }
        private void _Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 弹窗提示是否确定要退出
            MessageBoxResult f1 = iNKORE.UI.WPF.Modern.Controls.MessageBox.Show("你确定退出?", "Double check", MessageBoxButton.OKCancel, MessageBoxImage.Question);
        }
        //控制MainControl的
        public delegate void Theme_Re();
        public static event Theme_Re Theme_MainControl_New;
        private void MenuItem_Click(object sender, RoutedEventArgs e)//主题设置
        {
            //重构
            List<string> setting_homelist = System.IO.File.ReadAllText(App.setting_path).Split("${@}").ToList();
            string fliepath = $"{Environment.CurrentDirectory}" + "\\AppD\\setting.txt";
            setting_homelist[1] = "dark";
            string Data = string.Join("${@}", setting_homelist);
            StreamWriter sm1 = new StreamWriter(App.setting_path);
            sm1.Write(Data);
            sm1.Close();
            ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
            Theme_MainControl_New?.Invoke();
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            //重构
            List<string> setting_homelist = System.IO.File.ReadAllText(App.setting_path).Split("${@}").ToList();
            string fliepath = $"{Environment.CurrentDirectory}" + "\\AppD\\setting.txt";
            setting_homelist[1] = "light";
            string Data = string.Join("${@}", setting_homelist);
            StreamWriter sm1 = new StreamWriter(App.setting_path);
            sm1.Write(Data);
            sm1.Close();            
            ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
            Theme_MainControl_New?.Invoke();
        }

        private void ComboBox_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void Changed_Theme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Page_Loaded(object? sender, RoutedEventArgs? e)
        {
            D3_setting_small.Header = App.IL_all_list[7];
            D3_setting_small.Description = App.IL_all_list[18];
            D3_er_zi_dark.Header = App.IL_all_list[8]; 
            D3_er_zi_light.Header = App.IL_all_list[9];
            List<string> setting_homelist = System.IO.File.ReadAllText(App.setting_path).Split("${@}").ToList();
            if (setting_homelist[5] == "GPU")
            {
                App.Acceleration_Mode = "GPU";
                Toggled_AM.IsOn = true;
            }
            else if (setting_homelist[5] == "CPU")
            {
                App.Acceleration_Mode = "CPU";
                Toggled_AM.IsOn = false;
            }
        }
        private void CN_C(object sender, RoutedEventArgs e)
        {
            //重构
            List<string> setting_homelist = System.IO.File.ReadAllText(App.setting_path).Split("${@}").ToList();
            string fliepath = App.setting_path;
            setting_homelist[2] = "CN";
            string Data = string.Join("${@}", setting_homelist);
            StreamWriter sm1 = new StreamWriter(App.setting_path);
            sm1.Write(Data);
            sm1.Close();
        }
        private void EN_C(object sender, RoutedEventArgs e)
        {
            //重构
            List<string> setting_homelist = System.IO.File.ReadAllText(App.setting_path).Split("${@}").ToList();
            string fliepath = App.setting_path;
            setting_homelist[2] = "EN";
            string Data = string.Join("${@}", setting_homelist);
            StreamWriter sm1 = new StreamWriter(App.setting_path);
            sm1.Write(Data);
            sm1.Close();
        }
        private void JP_C(object sender, RoutedEventArgs e)
        {
            //重构
            List<string> setting_homelist = System.IO.File.ReadAllText(App.setting_path).Split("${@}").ToList();
            string fliepath = App.setting_path;
            setting_homelist[2] = "JP";
            string Data = string.Join("${@}", setting_homelist);
            StreamWriter sm1 = new StreamWriter(App.setting_path);
            sm1.Write(Data);
            sm1.Close();
        }
        private void FR_C(object sender, RoutedEventArgs e)
        {
            //重构
            List<string> setting_homelist = System.IO.File.ReadAllText(App.setting_path).Split("${@}").ToList();
            string fliepath = App.setting_path;
            setting_homelist[2] = "FR";
            string Data = string.Join("${@}", setting_homelist);
            StreamWriter sm1 = new StreamWriter(App.setting_path);
            sm1.Write(Data);
            sm1.Close();
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            List<string> setting_homelist = System.IO.File.ReadAllText(App.setting_path).Split("${@}").ToList();
            string fliepath = App.setting_path;
            if (setting_homelist[5] == "GPU")
            {
                setting_homelist[5] = "CPU";
                string Data = string.Join("${@}", setting_homelist);
                StreamWriter sm1 = new StreamWriter(App.setting_path);
                sm1.Write(Data);
                sm1.Close();
            }
            else if (setting_homelist[5] == "CPU")
            {
                setting_homelist[5] = "GPU";
                string Data = string.Join("${@}", setting_homelist);
                StreamWriter sm1 = new StreamWriter(App.setting_path);
                sm1.Write(Data);
                sm1.Close();
            }
            Page_Loaded(null, null);
        }

        private void Toggled_AM_Unloaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
