using ILGPU.Runtime;
using ILGPU;
using System;
using System.Collections.Generic;
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
using ILGPU.Runtime.Cuda;
using System.IO;

namespace NavigationViewExample.Pages
{
    /// <summary>
    /// Interaction logic for AppsPage.xaml
    /// </summary>
    public partial class AppsPage : Page
    {
        public AppsPage()
        {
            InitializeComponent();
        }
        //Gpu
        private async void BtnRunKernel_Click(object sender, RoutedEventArgs e)
        {
            // 异步调用 RunKernel 以避免阻塞 UI
            await RunKernelAsync();
        }
        private List<int> Temp_ShengCheng_Move = new List<int> { 3,4,7,1,9,4};
        private async Task RunKernelAsync()
        {
            HashSet<int> uniqueNumbers = new HashSet<int>();
            Random random = new Random();
            while (uniqueNumbers.Count < 20000)
            {
                int number = random.Next(int.MinValue, int.MaxValue);
                uniqueNumbers.Add(number);
            }
            // 将唯一的整数转换为列表
            List<int> intList = uniqueNumbers.ToList();
            // 使用 LINQ 将 int 列表转换为 string 列表
            List<string> stringList = intList.Select(x => x.ToString()).ToList();
            //List<string> dataList =Temp_ShengCheng_Move.Select(x => x.ToString()).ToList();
            // 异步_列表转换为整数数组
            int[] data = await Task.Run(() =>stringList.Select(int.Parse).ToArray());
            // 异步分割数组
            int cpuCount = 1200;
            int gpuCount = data.Length - cpuCount;

            Task<int[]> gpuDataTask = Task.Run(() => data.Take(gpuCount).ToArray());
            Task<int[]> cpuDataTask = Task.Run(() => data.Skip(gpuCount).ToArray());

            int[] gpuData = await gpuDataTask;
            int[] cpuData = await cpuDataTask;
            // 异步 CPU 排序部分
            Task cpuSortTask = Task.Run(() => Array.Sort(cpuData));
            // 使用 CUDA 
            using Context context = await Task.Run(() => Context.Create(builder => builder.Cuda()));
            Accelerator accelerator = await Task.Run(() => context.GetPreferredDevice(preferCPU: false).CreateAccelerator(context));
            // 异步 GPU 内存加载
            MemoryBuffer1D<int, Stride1D.Dense> deviceInput = await Task.Run(() => accelerator.Allocate1D(gpuData));
            MemoryBuffer1D<int, Stride1D.Dense> deviceOutput = await Task.Run(() => accelerator.Allocate1D<int>(gpuData.Length));
            // 异步加载_预编译内核
            Action<Index1D, ArrayView<int>, ArrayView<int>> loadedKernel = await Task.Run(() =>
                accelerator.LoadAutoGroupedStreamKernel<Index1D, ArrayView<int>, ArrayView<int>>(SortKernel));

            // 异步调用GPU 内核
            Task gpuSortTask = Task.Run(() =>
            {
                loadedKernel(deviceInput.Extent.ToIntIndex(), deviceInput.View, deviceOutput.View);
                accelerator.Synchronize();
            });
            // 等待 CPU 和 GPU 任务完成
            await Task.WhenAll(cpuSortTask, gpuSortTask);
            // 异步获取 GPU 排序结果
            int[] sortedGPUData = await Task.Run(() => deviceOutput.GetAsArray1D());

            // 异步合并排序结果 CPU 部分和 GPU 部分
            int[] sortedData = await Task.Run(() => MergeSortedArrays(cpuData, sortedGPUData).ToArray());
            // 显示结果（需要使用 Dispatcher 确保在 UI 线程上执行）
            Dispatcher.Invoke(() =>
            {
               // iNKORE.UI.WPF.Modern.Controls.MessageBox.Show("", string.Join(", ", sortedData), MessageBoxButton.OKCancel, MessageBoxImage.Question);
                CDSCsd.Text = string.Join("${@}", sortedData);
                Temp_ShengCheng_Move.Clear();
                //dataList.Clear();
            });

            // 释放 GPU 资源
            accelerator.Dispose();
            context.Dispose();
        }

        static IEnumerable<int> MergeSortedArrays(int[] arr1, int[] arr2)
        {
            int i = 0, j = 0;
            while (i < arr1.Length && j < arr2.Length)
            {
                if (arr1[i] < arr2[j])
                    yield return arr1[i++];
                else
                    yield return arr2[j++];
            }
            while (i < arr1.Length)
            {
                yield return arr1[i++];
            }
            while (j < arr2.Length)
            {
                yield return arr2[j++];
            }
        }

        static void SortKernel(Index1D index, ArrayView<int> input, ArrayView<int> output)
        {
            // 保持 bubble sort 简单实现
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input.Length - 1 - i; j++)
                {
                    if (input[j] > input[j + 1])
                    {
                        int temp = input[j];
                        input[j] = input[j + 1];
                        input[j + 1] = temp;
                    }
                }
            }

            if (index < input.Length)
            {
                output[index] = input[index];
            }
        }
        /////////////////////////////////////////////////////////////
        ///

    }
}
