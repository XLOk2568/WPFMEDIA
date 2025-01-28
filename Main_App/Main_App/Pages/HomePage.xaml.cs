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

namespace NavigationViewExample.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }
        //列表排序

        public struct Student
        {
            public int id;
            public int score;
            public string name;
            public Student(int id, int score, string name)
            {
                this.id = id;
                this.score = score;
                this.name = name;
            }
        }
        public static int CompareMethod(Student a, Student b)
        {
            int temp = b.score - a.score;
            if (temp == 0)
            {
                temp = a.id - b.id;
            }
            return temp;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Student[] nums = new Student[5];
            nums[0] = new Student(1001, 90, "张");
            nums[1] = new Student(1009, 83, "王");
            nums[2] = new Student(1004, 88, "李");
            nums[3] = new Student(1002, 88, "何");
            nums[4] = new Student(1005, 93, "赵");
            Array.Sort(nums, CompareMethod);
            for (int i = 0; i < nums.Length; i++)
            {
                Console.WriteLine("第" + (i + 1) + "名" + nums[i].name + " 学号:" + nums[i].id + " 分数:" + nums[i].score);
            }
            Console.ReadKey();
        }
    }
}
