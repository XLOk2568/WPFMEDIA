使用 iNKORE.UI.WPF.Modernusing iNKORE.UI.WPF.Modern;
using   使用 System;   使用系统;
using   使用 System.Collections.Generic;using   使用 系统命名空间集合
using   使用 System.Diagnostics;使用 System.Diagnostics
using   使用 System.IO;   使用先;
using   使用 System.Linq;   使用来;
using   使用 System.Text;   使用text;
using   使用 System.Threading.Tasks;using   使用 System.Threading.Tasks;  // 使用系统命名空间中的“线程任务”；
using   使用 System.Windows;   使用 System.Windows
using   使用 System.Windows.Controls;使用 System.Windows.Controls
using   使用 System.Windows.Data;使用 System.Windows.Data
using   使用 System.Windows.Documents;使用 System.Windows.Documents
using   使用 System.Windows.Input;使用 System.Windows.Input
using   使用 System.Windows.Media;使用 System.Windows.Media
using   使用 System.Windows.Media.Imaging;using   使用 System.Windows.Media.Imaging;  // 使用 System.Windows.Media.Imaging 命名空间；
using   使用 System.Windows.Navigation;使用 System.Windows.Navigation
using   使用 System.Windows.Shapes;使用 System.Windows.Shapes
using   使用 Page = iNKORE.UI.WPF.Modern.Controls.Page;使用 Page = iNKORE.UI.WPF.Modern.Controls.Page


namespace   名称空间 命名空间 NavigationViewExample   导航视图示例.PagesNavigationViewExample.Pages
{
       > / / / <总结/// <summary>   > / / / <总结
    GamesPage.xaml 的交互逻辑/// Interaction logic for GamesPage.xamlGamesPage.xaml 的交互逻辑
       > / / / < /总结/// </summary>   > / / / < /总结
    public   公共 partial   部分 class   类 NvSetPage : Page

部分类 NvSetPage 继承自 Pagepublic partial   部分 class   类 NvSetPage : Page
    {
        public   公共 NvSetPage()
        {
            InitializeComponent();
        }
        私有 void   无效 _Closing(object 发送者， System.ComponentModel.CancelEventArgs eprivate void _Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 弹窗提示是否确定要退出
            MessageBoxResult f1 = iNKORE.UI.WPF.Modern.Controls.MessageBox.Show("你确定退出?", "Double check", MessageBoxButton.OKCancel, MessageBoxImage.Question);
        }
        private   私人 void MenuItem_Click(object sender, RoutedEventArgs e)//主题设置
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
        private   私人 void MenuItem_Click_1(object sender, RoutedEventArgs e)
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
        private   私人 void MenuItem_Click_2(object sender, RoutedEventArgs e)
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
