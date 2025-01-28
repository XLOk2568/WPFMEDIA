using System.Configuration;
using System.Data;
using System.Windows;
using static NavigationViewExample.Pages.MainControlPage;

namespace NavigationViewExample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string Run_Time_Path = $"{Environment.CurrentDirectory}" + "\\AppD";
        public static string setting_path = $"{Environment.CurrentDirectory}" + "\\AppD\\setting.txt";//其他窗口也用这个
        public static string InterfaceLanguage_All = "";//其实是选择的语言文本的所有
        public static string settings_ = "";
        public static List<string> setting_all_list = App.settings_.Split("${@}").ToList();
        public static List<string> IL_all_list = App.settings_.Split("${@}").ToList();
        public static string Mediaworkfile = "";
        public static string Temp_Lrc_txt ="";//放到LRC。xaml获取歌词的
    }
}
    


