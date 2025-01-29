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
using ILGPU.Runtime.OpenCL;
using Microsoft.Win32;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Windows.Controls.Image;

namespace NavigationViewExample.Pages
{
    /// <summary>
    /// Interaction logic for AppsPage.xaml
    /// </summary>
    public partial class File_ListPage : Page
    {
        private ListBoxItem draggingItem = null;
        private Point startPoint;
        private List<string> B_112 = new List<string> { "3fe我是", "新000", "2025Hi", "hyppynewyear", "hyppynewyear", "hyppynewyear" , "hyppynewyear" , "hyppynewyear" };
        private string A = "";
        private string CC = "";

        public File_ListPage()
        {
            InitializeComponent();
            InitializeListBoxItems();
        }

        private void InitializeListBoxItems()
        {
            for (int i = 0; i < B_112.Count; i++)
            {
                var item = new ListBoxItem();
                var panel = new StackPanel { Orientation = Orientation.Horizontal };

                var image = new Image { Width = 20, Height = 20, Margin = new Thickness(5) };
                var centerLabel = new Label { Content = B_112[i], Width = 100, VerticalAlignment = VerticalAlignment.Center };
                var rightLabel = new Label { Content = (i + 1).ToString(), HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Center };

                panel.Children.Add(image);
                panel.Children.Add(centerLabel);
                panel.Children.Add(rightLabel);

                item.Content = panel;
                item.Tag = B_112[i];
                item.MouseRightButtonUp += Item_RightClick;
                item.MouseLeftButtonUp += Item_LeftClick;

                listBox1.Items.Add(item);
            }
        }

        private void ListBox1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
            draggingItem = GetListBoxItemUnderMouse(listBox1, e.GetPosition(listBox1));
        }

        private void ListBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && draggingItem != null)
            {
                Point currentPoint = e.GetPosition(null);
                if (Math.Abs(currentPoint.X - startPoint.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(currentPoint.Y - startPoint.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    DataObject data = new DataObject("myFormat", draggingItem);
                    DragDrop.DoDragDrop(listBox1, data, DragDropEffects.Move);
                }
            }
        }

        private void ListBox1_Drop(object sender, DragEventArgs e)
        {
            if (draggingItem != null)
            {
                ListBoxItem targetItem = GetListBoxItemUnderMouse(listBox1, e.GetPosition(listBox1));
                if (targetItem != null)
                {
                    int targetIndex = listBox1.Items.IndexOf(targetItem);
                    listBox1.Items.Remove(draggingItem);
                    listBox1.Items.Insert(targetIndex, draggingItem);
                    UpdateRightLabels();
                }
                draggingItem = null;
            }
        }

        private ListBoxItem GetListBoxItemUnderMouse(ListBox listBox, Point position)
        {
            HitTestResult hitTestResult = VisualTreeHelper.HitTest(listBox, position);
            if (hitTestResult != null)
            {
                ListBoxItem listBoxItem = FindAncestor<ListBoxItem>((DependencyObject)hitTestResult.VisualHit);
                return listBoxItem;
            }
            return null;
        }

        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            while (current != null)
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            return null;
        }

        private void UpdateRightLabels()
        {
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                var listBoxItem = listBox1.Items[i] as ListBoxItem;
                if (listBoxItem != null)
                {
                    var panel = listBoxItem.Content as StackPanel;
                    if (panel != null && panel.Children.Count == 3)
                    {
                        var rightLabel = panel.Children[2] as Label;
                        if (rightLabel != null)
                        {
                            rightLabel.Content = (i + 1).ToString();
                        }
                    }
                }
            }
        }

        private void Item_RightClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBoxItem item)
            {
                CC = item.Tag.ToString();
                C23();
                ShowContextMenu(item);
            }
        }

        private void Item_LeftClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBoxItem item)
            {
                B_113(item.Tag.ToString());
                F_12(item.Tag.ToString());
            }
        }

        private void ShowContextMenu(ListBoxItem item)
        {
            var contextMenu = new ContextMenu();
            var deleteMenuItem1 = new MenuItem { Header = "删除1" };
            var deleteMenuItem2 = new MenuItem { Header = "删除2" };
            var deleteMenuItem3 = new MenuItem { Header = "删除3" };
            deleteMenuItem1.Click += (s, e) => DeleteItem(item);
            deleteMenuItem2.Click += (s, e) => DeleteItem(item);
            deleteMenuItem3.Click += (s, e) => DeleteItem(item);
            contextMenu.Items.Add(deleteMenuItem1);
            contextMenu.Items.Add(deleteMenuItem2);
            contextMenu.Items.Add(deleteMenuItem3);
            item.ContextMenu = contextMenu;
            contextMenu.IsOpen = true;
        }

        private void DeleteItem(ListBoxItem item)
        {
            listBox1.Items.Remove(item);
            ButtonB_Click(null, null);
        }

        private void ButtonB_Click(object sender, RoutedEventArgs e)
        {
            List<string> itemsContent = new List<string>();
            foreach (ListBoxItem item in listBox1.Items)
            {
                itemsContent.Add(item.Tag.ToString());
            }
            string content = string.Join("${@}", itemsContent);
            File.WriteAllText("xx.txt", content);
        }

        private void ButtonC_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var item = new ListBoxItem();
                var panel = new StackPanel { Orientation = Orientation.Horizontal };

                var image = new Image { Width = 20, Height = 20, Margin = new Thickness(5) };
                var centerLabel = new Label { Content = "C", Width = 100, VerticalAlignment = VerticalAlignment.Center };
                var rightLabel = new Label { Content = listBox1.Items.Count.ToString(), HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Center };

                panel.Children.Add(image);
                panel.Children.Add(centerLabel);
                panel.Children.Add(rightLabel);

                item.Content = panel;
                item.Tag = "C";
                item.MouseRightButtonUp += Item_RightClick;
                item.MouseLeftButtonUp += Item_LeftClick;

                listBox1.Items.Add(item);
                UpdateRightLabels();
            }
        }

        private void B_113(string content)
        {
            A = content;
            MessageBox.Show($"Item clicked: {content}");
        }

        private void F_12(string content)
        {
            MessageBox.Show($"Running event F_12 for item: {content}");
        }

        private void C23()
        {
            MessageBox.Show($"Right-clicked item with content: {CC}");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            string imagePath = "path_image.png";
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
            bitmap.DecodePixelWidth = 256;
            bitmap.DecodePixelHeight = 256;
            bitmap.EndInit();
            //myImage.Source = bitmap;
        }
    }
}
