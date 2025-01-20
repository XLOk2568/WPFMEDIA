using iNKORE.UI.WPF.Modern;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public string setting_path = $"{Environment.CurrentDirectory}" + "\\AppD\\setting.txt";//其他窗口也用这个
        private void _Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 弹窗提示是否确定要退出
            MessageBoxResult f1 = iNKORE.UI.WPF.Modern.Controls.MessageBox.Show("你确定退出?", "Double check", MessageBoxButton.OKCancel, MessageBoxImage.Question);
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)//主题设置
        {
            //重构
            List<string> setting_homelist = File.ReadAllText(setting_path).Split("${@}").ToList();
            string fliepath = $"{Environment.CurrentDirectory}" + "\\AppD\\setting.txt";
            int TrueCount = setting_homelist.Count;
            string Data = setting_homelist[0] + "${@}" + "dark";
            for (int for_ = 2; for_ != TrueCount; for_ += 1)
            {
                Data = Data + "${@}" + setting_homelist[for_];
            }            
            StreamWriter sm1 = new StreamWriter(setting_path);
            sm1.Write(Data);
            sm1.Close();
            ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
  MainControlPage.ControlOld.Source= new BitmapImage(new Uri("/photo/light/old.png"));  //通过实例名直接访问Form1的控件和方法
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            //重构
            List<string> setting_homelist = File.ReadAllText(setting_path).Split("${@}").ToList();
            string fliepath = $"{Environment.CurrentDirectory}" + "\\AppD\\setting.txt";
            int TrueCount = setting_homelist.Count;
            string Data = setting_homelist[0]+"${@}"+"light";
            for (int for_=2; for_ != TrueCount; for_ += 1)
            {
                Data = Data + "${@}" + setting_homelist[for_];
            }
            StreamWriter sm1 = new StreamWriter(setting_path);
            sm1.Write(Data);
            sm1.Close();            
            ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
        }
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            //重构

            List<string> setting_homelist = File.ReadAllText(setting_path).Split("${@}").ToList();
            string fliepath = $"{Environment.CurrentDirectory}" + "\\AppD\\setting.txt";
            int TrueCount = setting_homelist.Count;
            string Data = setting_homelist[0] + "${@}" + "null";
            for (int for_ = 2; for_ != TrueCount; for_ += 1)
            {
                Data = Data + "${@}" + setting_homelist[for_];
            }            
            StreamWriter sm1 = new StreamWriter(setting_path);
            sm1.Write(Data);
            sm1.Close();
            ThemeManager.Current.ApplicationTheme = null;
        }
    }
}
