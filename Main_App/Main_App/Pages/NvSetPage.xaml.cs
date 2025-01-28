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
            List<string> setting_homelist = File.ReadAllText(App.setting_path).Split("${@}").ToList();
            string fliepath = $"{Environment.CurrentDirectory}" + "\\AppD\\setting.txt";
            int TrueCount = setting_homelist.Count;
            string Data = setting_homelist[0] + "${@}" + "dark";
            for (int for_ = 2; for_ != TrueCount; for_ += 1)
            {
                Data = Data + "${@}" + setting_homelist[for_];
            }            
            StreamWriter sm1 = new StreamWriter(App.setting_path);
            sm1.Write(Data);
            sm1.Close();
            ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
            Theme_MainControl_New?.Invoke();
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            //重构
            List<string> setting_homelist = File.ReadAllText(App.setting_path).Split("${@}").ToList();
            string fliepath = $"{Environment.CurrentDirectory}" + "\\AppD\\setting.txt";
            int TrueCount = setting_homelist.Count;
            string Data = setting_homelist[0]+"${@}"+"light";
            for (int for_=2; for_ != TrueCount; for_ += 1)
            {
                Data = Data + "${@}" + setting_homelist[for_];
            }
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
    }
}
