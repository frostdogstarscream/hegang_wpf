using Hegang.APP.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace Hegang.APP.Views
{
    /// <summary>
    /// PLCItemConfig.xaml 的交互逻辑
    /// </summary>
    public partial class PLCItemConfig : Window
    {
        private ObservableCollection<string> csvFiles;
        private ObservableCollection<TestPoint> testPoints;

        public PLCItemConfig()
        {
            InitializeComponent();
            csvFilesInit();
        }

        /// <summary>
        /// 初始化csvFiles
        /// 参考：https://blog.csdn.net/chunleixiahe/article/details/86532448
        /// </summary>
        public void csvFilesInit()
        {
            //初始化相关变量
            this.csvFiles = new ObservableCollection<string>();
            string path = CSVUtils.get_csv_path();
            string extName = ".csv";
                
            DirectoryInfo fdir = new DirectoryInfo(path);
            FileInfo[] file = fdir.GetFiles();
            //FileInfo[] file = Directory.GetFiles(path); //文件列表   

            if (file.Length != 0) //当前目录文件或文件夹不为空                   
            {
                foreach (FileInfo f in file) //显示当前目录所有文件   
                {
                    if (extName.ToLower().IndexOf(f.Extension.ToLower()) >= 0)
                    {
                        this.csvFiles.Add(f.Name);
                    }
                }
            }
            this.tp_comboBox.ItemsSource = csvFiles;
        }

        private void tp_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(null!= this.tp_comboBox.SelectedItem)
            {
                string fileName = this.tp_comboBox.SelectedItem.ToString();
                this.testPoints = CSVUtils.getTestPoints(fileName);
                this.tp_listView.ItemsSource = this.testPoints;
            }
        }

        private void modify_btn_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var testPoint = btn.DataContext as TestPoint;
            string fileName = this.tp_comboBox.SelectedItem.ToString();
            int id = Convert.ToInt32(testPoint.Id);

            ModifyTestPoint modifyTestPoint = new ModifyTestPoint(testPoints, id, fileName);
            modifyTestPoint.Owner=this;
            modifyTestPoint.ShowDialog();

            if (!string.IsNullOrEmpty(modifyTestPoint.Rtn_msg))
                this.color_bar_text.Text = modifyTestPoint.Rtn_msg;
        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var testPoint = btn.DataContext as TestPoint;
            string fileName = this.tp_comboBox.SelectedItem.ToString();
            this.testPoints.Remove(testPoint);
            CSVUtils.saveTestPointsToCSV(testPoints,fileName);
            this.color_bar_text.Text = "测点删除成功！";
        }

        private void add_pt_btn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "逗号分隔值文件(*.csv)|*.csv";
            bool? b = ofd.ShowDialog();
            if (b==true)
            {
                string filePath = ofd.FileName;
                string fileName=System.IO.Path.GetFileName(filePath);
                string savePath = CSVUtils.get_csv_path() + fileName;
                if (File.Exists(savePath))
                {
                    Tip tip = new Tip("点表已存在！");
                    tip.ShowDialog();
                }
                else
                {
                    File.Copy(filePath, savePath);
                    this.color_bar_text.Text = "点表添加成功！";
                    csvFilesInit();
                }

                /*FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                File.Copy(filePath)*/
            }
        }

        private void add_tp_btn_Click(object sender, RoutedEventArgs e)
        {
            if (this.tp_comboBox.SelectedItem != null)
            {
                string fileName = this.tp_comboBox.SelectedItem.ToString();
                ModifyTestPoint modifyTestPoint = new ModifyTestPoint(testPoints, fileName);
                modifyTestPoint.ShowDialog();

                if (!string.IsNullOrEmpty(modifyTestPoint.Rtn_msg))
                    this.color_bar_text.Text = modifyTestPoint.Rtn_msg;
            }
            else
            {
                Tip tip = new Tip("请先选择点表！");
                tip.ShowDialog();
            }
        }

        private void del_pt_btn_Click(object sender, RoutedEventArgs e)
        {
            if (this.tp_comboBox.SelectedItem != null)
            {
                string fileName = this.tp_comboBox.SelectedItem.ToString();
                string filePath = CSVUtils.get_csv_path() + fileName;
                this.tp_comboBox.SelectedItem = null;
                File.Delete(filePath);
                csvFilesInit();
                this.tp_listView.ItemsSource = null;
                this.color_bar_text.Text = "点表删除成功！";
            }
            else
            {
                Tip tip = new Tip("请先选择点表！");
                tip.ShowDialog();
            }
        }
    }
}
