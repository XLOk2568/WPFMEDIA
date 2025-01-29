using ILGPU.Runtime;
using ILGPU;
using iNKORE.UI.WPF.Modern;
using System;
using System.Collections.Generic;
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
using static System.Net.Mime.MediaTypeNames;
using Page = iNKORE.UI.WPF.Modern.Controls.Page;
using ILGPU.Runtime.Cuda;
using ILGPU.Runtime.OpenCL;

namespace NavigationViewExample.Pages
{
    /// <summary>
    /// Interaction logic for AppsPage.xaml
    /// </summary>
    public partial class MainControlPage : Page
    {
        public MainControlPage()
        {
            InitializeComponent();
            NvSetPage.Theme_MainControl_New += Theme_png_darkOrlight;
        }
        private void Theme_png_darkOrlight()
        {
            Page_Loaded(null, null);
        }
        //
        public delegate void Media_();
        public static event Media_ Media_Do;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Media_Do?.Invoke();
        }
        //让Lrc.xaml...绑定
        public delegate void LRC_();
        public static event LRC_ LRC_F;
        private void Button_Click_old(object sender, RoutedEventArgs e)
        {
            RunKernel();
        }
        private void Button_Click_next(object sender, RoutedEventArgs e)
        {

        }

        private void Page_Loaded(object? sender, RoutedEventArgs? e)
        {
            string path=$"{Environment.CurrentDirectory}" + "\\AppD\\setting.txt";
            List<string> setting_homelist = File.ReadAllText(path).Split("${@}").ToList();
            if (setting_homelist[1] == "dark")
            {
                ControlPS.Source = new BitmapImage(new Uri("/photo/light/play.png", UriKind.Relative));
                ControlOld.Source = new BitmapImage(new Uri("/photo/light/old.png", UriKind.Relative));
                ControlNext.Source = new BitmapImage(new Uri("/photo/light/next.png", UriKind.Relative));
            }
            else if (setting_homelist[1] == "light")
            {
                ControlPS.Source = new BitmapImage(new Uri("/photo/dark/play.png", UriKind.Relative));
                ControlOld.Source = new BitmapImage(new Uri("/photo/dark/old.png", UriKind.Relative));
                ControlNext.Source = new BitmapImage(new Uri("/photo/dark/next.png", UriKind.Relative));
            }
        }

        private void Button_Click_list_ing(object sender, RoutedEventArgs e)
        {

        }
        private int T4 = 0;
        private void SD_1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int a = (int)SD_1.Value;
            string a2 = a.ToString();
            T6.Text = a2;
        }
        //
        private void RunKernel()
        {
            // 初始化数据
            int[] data = new int[] { 4, 3, 2, 0, 1,9,11,10 };
            string[] words = new string[] { "啊", "乐", "快", "新", "年" ,"祝","位","各"};
            //选择使用CUDA的显卡以进行加速
            using Context context = Context.Create(builder => builder.OpenCL());
            Accelerator accelerator = context.GetPreferredDevice(preferCPU: false).CreateAccelerator(context);
            // GPU内存加载
            MemoryBuffer1D<int, Stride1D.Dense> deviceData = accelerator.Allocate1D(data);
            MemoryBuffer1D<int, Stride1D.Dense> deviceOutput = accelerator.Allocate1D<int>(data.Length);
            // 加载和预编译内核
            Action<Index1D, ArrayView<int>, ArrayView<int>> loadedKernel = accelerator.LoadAutoGroupedStreamKernel<Index1D, ArrayView<int>, ArrayView<int>>(SortKernel);
            // 执行排序内核
            loadedKernel(deviceData.Extent.ToIntIndex(), deviceData.View, deviceOutput.View);
            // 等待计算完成
            accelerator.Synchronize();
            // 运行后，将数据转移至CPU
            int[] sortedData = deviceOutput.GetAsArray1D();
            // 根据排序后的data对words进行排序
            List<string> sortedWords = sortedData.Select((value, index) => new { Value = value, Word = words[Array.IndexOf(data, value)] }).OrderBy(pair => pair.Value).Select(pair => pair.Word).ToList();
            // 显示结果
            iNKORE.UI.WPF.Modern.Controls.MessageBox.Show("", string.Join(", ", sortedData)+"/n"+ string.Join(", ", sortedWords), MessageBoxButton.OKCancel, MessageBoxImage.Question);
            // 释放GPU资源
            accelerator.Dispose();
            context.Dispose();
        }
        static void SortKernel(Index1D index, ArrayView<int> data, ArrayView<int> output)
        {
            int n = (int)data.Length;
            // 冒泡排序算法
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (data[j] > data[j + 1])
                    {
                        // 交换 data[j] 和 data[j + 1]
                        int temp = data[j];
                        data[j] = data[j + 1];
                        data[j + 1] = temp;
                    }
                }
            }
            // 将排序后的数据复制到输出数组
            if (index < n)
            {
                output[index] = data[index];
            }

        }
    }
}
