using iNKORE.UI.WPF.Modern;
using iNKORE.UI.WPF.Modern.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;
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
using File = System.IO.File;
using Page = iNKORE.UI.WPF.Modern.Controls.Page;
using Path = System.IO.Path;

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
        //添加媒体文件
        private List<string> read_disk = new List<string> { "asf", "wma", "wmv", "wm", "avi", "mpg", "mpge", "m1v", "mp2", "mp3", "mpa", "mpe", "m3u", "m4a", "mp4", "m4v", "mp4v", "3g2", "3gp2", "3gp", "flac" }; // 示例后缀列表
        private List<string> listPath = new List<string>(); // ram中的路径
        private List<string> listName = new List<string>(); // ram中的名字
        private List<string> listNew = new List<string>(); // 新列表
        private string path = ""; // 选择文件夹路径
        private async void ButtonRD_Click(object sender, RoutedEventArgs e)
        {
            view_ing.Visibility = Visibility.Visible;
            var dialog = new CommonOpenFileDialog();
            dialog.Title = "请选择文件夹";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                path = dialog.FileName;
                await ProcessFilesAsync(path);
            }
            else
            {
                view_ing.Visibility = Visibility.Collapsed;
            }
        }
        private async Task ProcessFilesAsync(string folderPath)
        {
            List<string> listOld = new List<string>();
            List<string> listOldName = new List<string>();
            List<string> listOldPath = new List<string>();

            await Task.Run(() =>
            {
                foreach (string suffix in read_disk)
                {
                    string[] files = Directory.GetFiles(folderPath, $"*{suffix}", SearchOption.AllDirectories);
                    foreach (string file in files)
                    {
                        string name = Path.GetFileNameWithoutExtension(file);
                        string fullPath = file;

                        listName.Add(name);
                        listPath.Add(fullPath);
                        listNew.Add(name + "$@" + fullPath);
                    }
                }
                string userMediaDiskPath = Path.Combine(Directory.GetParent(App.Run_Time_Path).FullName, "User_Media", "UserMedia_Disk.txt");

                if (File.Exists(userMediaDiskPath))
                {
                    string oldContent = File.ReadAllText(userMediaDiskPath);
                    listOld = oldContent.Split(new string[] { "${@}" }, StringSplitOptions.None).ToList();

                    foreach (string item in listOld)
                    {
                        string[] parts = item.Split(new string[] { "$@" }, StringSplitOptions.None);
                        if (parts.Length == 2)
                        {
                            listOldName.Add(parts[0]);
                            listOldPath.Add(parts[1]);
                        }
                    }

                    foreach (string oldItem in listOld)
                    {
                        if (!listNew.Contains(oldItem))
                        {
                            listNew.Add(oldItem);
                        }
                    }
                }
                string newContent = string.Join("${@}", listNew);
                File.WriteAllText(Path.Combine(Directory.GetParent(App.Run_Time_Path).FullName, "User_Media", "UserMedia_Disk.txt"), newContent);
            });
            listPath.Clear();
            listName.Clear();
            listNew.Clear();
            view_ing.Visibility = Visibility.Collapsed;
        }
    }
}
