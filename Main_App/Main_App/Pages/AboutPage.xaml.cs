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
using UpdateD;
using Windows.Media.Protection.PlayReady;
using Page = iNKORE.UI.WPF.Modern.Controls.Page;

namespace NavigationViewExample.Pages
{
    /// <summary>
    /// Interaction logic for AppsPage.xaml
    /// </summary>
    public partial class AboutPage : Page
    {
        UpdateD.Update up = new UpdateD.Update();
        private int getver = 1;
        private string nt="";
        private int downing = 0;
        public AboutPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            meag.Text = "本软件项目开源:\nhttps://github.com/XLOk2568/WPFMEDIA\n当前软件版本:\n0.0.1.2\nCopyright  ©2024-2025  @XiaLiang\n当前版本支持的语言:中，英，日，法\n堆栈追踪：\n" + Environment.StackTrace;
            ring.Visibility = Visibility.Collapsed;
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (downing == 0)
            {
                downing = 1;
                ring.Visibility = Visibility.Visible;//显示等待动画
                                                     //开启线程获取网络版本
                await Task.Run(() =>
                {
                    if (up.GetUpdate("C3ED9ED08FA94089B0D3D8A5ED27B720", "1.0.0.3") == true)
                    {
                        getver = 2;
                    }
                    else
                    {
                        getver = 0;
                    }
                });
                ring.Visibility = Visibility.Collapsed;
                //等待结果后询问
                if (getver == 2)
                {
                    ring.Visibility = Visibility.Visible;
                    //开线程获取公告信息
                    await Task.Run(() =>
                    {
                        nt = up.GetUpdateNotice("C3ED9ED08FA94089B0D3D8A5ED27B720");
                    });
                    ring.Visibility = Visibility.Collapsed;
                    MessageBoxResult u = iNKORE.UI.WPF.Modern.Controls.MessageBox.Show("是否需要更新?\n" + nt, "Notice", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                    if (u == MessageBoxResult.OK)
                    {
                        Download();
                    }
                    else 
                    {
                        downing = 0;
                    }
                }
                else if (getver == 0)
                {
                    ring.Visibility = Visibility.Collapsed;
                    iNKORE.UI.WPF.Modern.Controls.MessageBox.Show("没有任何更新", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                    downing = 0;
                }
                else if (getver == 1)
                {
                    iNKORE.UI.WPF.Modern.Controls.MessageBox.Show("遇到错误，\n无法获取服务器信息", "No", MessageBoxButton.OK, MessageBoxImage.Warning);
                    downing = 0;
                }
            }
        }

        private async void Download()
        {
            downing = 1;
            ring.Visibility = Visibility.Visible;
            await Task.Run(() =>
            {
                string url = up.GetUpdateFile("C3ED9ED08FA94089B0D3D8A5ED27B720");
                string savepath = $"{System.Environment.CurrentDirectory}" + "\\Newapp.exe";
                //旧版本方案
                //WebClient myWebClient = new();
                //myWebClient.DownloadFile(url, savepath);
                //
                string content;
                using (HttpClient client =new())
                {
                    content = client.GetStringAsync(url).Result;
                }
                File.WriteAllText(savepath, content);
                //Process b = System.Diagnostics.Process.Start($"{System.Environment.CurrentDirectory}" + "\\Newapp.exe");
                //b.WaitForExit();
                System.Diagnostics.Process.Start(savepath);
            });
            ring.Visibility = Visibility.Collapsed;
            Process.GetCurrentProcess().Kill();
        }
    }
}
