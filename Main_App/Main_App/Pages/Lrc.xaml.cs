using iNKORE.UI.WPF.Modern.Controls;
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
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
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
using System.Windows.Threading;
using UpdateD;
using Windows.Media.Protection.PlayReady;
using MessageBox = iNKORE.UI.WPF.Modern.Controls.MessageBox;
using Page = iNKORE.UI.WPF.Modern.Controls.Page;

namespace NavigationViewExample.Pages
{
    public partial class LrcPage : Page
    {
        public LrcPage()
        {
            InitializeComponent();
            MainControlPage.LRC_F += Read_N_F;//绑定MainControl
        }
        //时钟
        private System.Threading.Timer _timer;
        private int _counter;
        private bool _isPaused;
        //激活时钟
        private void Start__timer(object sender, RoutedEventArgs e)
        {
            _counter = 0;
            _timer = new System.Threading.Timer(TimerCallback, null, 0, 1000);
        }
        private int Move_ui_text_int = 0;//移动到第X个控件
        private void TimerCallback(object state)
        {
            _counter += 1;
            Dispatcher.Invoke(() =>
            {
                int x2 = (Move_ui_text_int * 80) + 60;
                scrollViewer.ScrollToVerticalOffset(x2); // 滚动到顶部
                if (Move_ui_text_int < contentGrid.Children.Count)
                {
                    var targetButton = contentGrid.Children[Move_ui_text_int] as HyperlinkButton;
                    if (targetButton != null)
                    {
                        targetButton.BringIntoView();
                        targetButton.FontSize = 40; // 修改字体大小
                        targetButton.Foreground = new SolidColorBrush(Colors.Red); // 修改字体颜色
                    }
                }
            });
        }
        //暂停
        private void BtnPauseTimer_Click(object sender, RoutedEventArgs e)
        {
            _isPaused = true;
        }
        //暂停后的启动
        private void BtnResumeTimer_Click(object sender, RoutedEventArgs e)
        {
            _isPaused = false;
        }
        //关闭计时器
        private void timer_Dispose(object sender, RoutedEventArgs e)
        {
            _timer?.Dispose();
        }
        private void Read_N_F()
        {

        }
        //获取列表长度并且更新控件
        public List<int> list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10,11,12,13,14,15,16 }; // 示例列表
        private List<int> listA = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }; // 示例列表A
        private List<string> listB = new List<string> { "你好", "我是", "小猪","6","efew","trewt","对俄","结尾","你好", "我是", "小猪", "6", "efew", "trewt", "对俄", "结尾" }; // 示例列表B
        private string d;
        private void Start_read_LRC(object? sender, RoutedEventArgs? e)
        {
            contentGrid.Children.Clear();
            contentGrid.RowDefinitions.Clear();

            for (int i = 0; i < listA.Count; i++)
            {
                var hyperlinkButton = new HyperlinkButton
                {
                    Content = i < listB.Count ? listB[i] : $"Item {i + 1}",
                    Tag = i,
                    HorizontalAlignment = HorizontalAlignment.Stretch, // 水平拉伸
                    VerticalAlignment = VerticalAlignment.Center // 垂直居中
                };
                hyperlinkButton.Click += HyperlinkButton_Click;
                hyperlinkButton.Height = 80;
                hyperlinkButton.FontSize = 16;
                hyperlinkButton.HorizontalAlignment = HorizontalAlignment.Stretch;
                contentGrid.RowDefinitions.Add(new RowDefinition());
                Grid.SetRow(hyperlinkButton, i);
                contentGrid.Children.Add(hyperlinkButton);
                int t90_ = (int)(WindowHeight - 200);
                scrollViewer.Height = t90_;
            }
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is HyperlinkButton hyperlinkButton)
            {
                d = hyperlinkButton.Content.ToString();
                apprun_();
            }
        }
        private void apprun_()
        {
            iNKORE.UI.WPF.Modern.Controls.MessageBox.Show($"apprun_事件触发，内容：{d}\n");
        }
        List<int> listA_3 = new List<int>();//将其换成毫秒
        //获取歌词文本加载
        private void X90_Loaded(object sender, RoutedEventArgs e)
        {
            //ButtonB_Click(null,null);
            List<string> listA = new List<string>(); // 用于存储 [] 内的内容
            List<string> listV = new List<string>(); // 用于存储 ] 右边的文本
            // 读取文件的所有行
            string[] lines = File.ReadAllLines(App.Temp_Lrc_txt);
            // 定义正则表达式来匹配 [] 内的内容和 ] 右边的文本
            Regex regex = new Regex(@"\[(.*?)\](.*)");
            foreach (var line in lines)
            {
                Match match = regex.Match(line);
                if (match.Success)
                {
                    // 将匹配到的 [] 内的内容和 ] 右边的文本分别添加到 listA 和 listV 中
                    listA.Add(match.Groups[1].Value);
                    listV.Add(match.Groups[2].Value);
                }
            }
            // 将结果显示在文本框中
            //txtListA.Text = string.Join(Environment.NewLine, listA);
            //txtListV.Text = string.Join(Environment.NewLine, listV);
            int C_1 = listA.Count-1;
            int Math_ing = 0;
            int MM_ = 0;
            int SS_ = 0;
            int MS_ = 0;
            for (int T_121 = 0; T_121 < C_1; T_121 += 1)
            {
                if (listA[T_121].Length == 8)//03:01:08
                {
                    var part_ = listA[T_121].Split(new char[] { ':', '.' });
                    MM_ = int.Parse(part_[0]);
                    SS_ = int.Parse(part_[1]);
                    MS_ = int.Parse(part_[2]);
                    Math_ing = MM_ * 60000 + SS_ * 1000 + MS_ * 10;
                }
                else if(listA[T_121].Length==9)
                {
                    var part_ = listA[T_121].Split(new char[] { ':', '.' });
                    MM_ = int.Parse(part_[0]);
                    SS_ = int.Parse(part_[1]);
                    MS_ = int.Parse(part_[2]);
                    Math_ing = MM_ * 60000 + SS_ * 1000 + MS_ ;
                }
                else if (listA[T_121].Length == 7)
                {
                    var part_ = listA[T_121].Split(new char[] { ':', '.' });
                    MM_ = int.Parse(part_[0]);
                    SS_ = int.Parse(part_[1]);
                    MS_ = int.Parse(part_[2]);
                    Math_ing = MM_ * 60000 + SS_ * 1000 + MS_ * 100;
                }
                listA_3.Add(Math_ing);
            }
         }
     }
}
