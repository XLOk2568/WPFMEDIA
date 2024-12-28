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
        private void MenuItem_Click(object sender, RoutedEventArgs e)//主题设置
        {
                StreamWriter sm1 = new StreamWriter(new MainWindow().setting_path);
                string T1 = "";
                for (int T0 = 0; T0!= new MainWindow().setting_homelist_count; T0 += 1)
                {
                if (T0 != 1)
                {
                    T1 = T1 + new MainWindow().setting_homelist[T0];
                }
                else if (T0 == 1)
                {
                    T1 = T1 + "dark";
                }
                }
                sm1.Write(T1);
                sm1.Close();
            ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            StreamWriter sm1 = new StreamWriter(new MainWindow().setting_path);
            string T1 = "";
            for (int T0 = 0; T0 != new MainWindow().setting_homelist_count; T0 += 1)
            {
                if (T0 != 1)
                {
                    T1 = T1 + new MainWindow().setting_homelist[T0];
                }
                else if (T0 == 1)
                {
                    T1 = T1 + "light";
                }
            }
            sm1.Write(T1);
            sm1.Close();
            ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
        }
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            StreamWriter sm1 = new StreamWriter(new MainWindow().setting_path);
            string T1 = "";
            for (int T0 = 0; T0 != new MainWindow().setting_homelist_count; T0 += 1)
            {
                if (T0 != 1)
                {
                    T1 = T1 + new MainWindow().setting_homelist[T0];
                }
                else if (T0 == 1)
                {
                    T1 = T1 + "null";
                }
            }
            sm1.Write(T1);
            sm1.Close();
            ThemeManager.Current.ApplicationTheme = null;
        }
    }
}
