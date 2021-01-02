using Hegang.APP.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// ModifyTestPoint.xaml 的交互逻辑
    /// </summary>
    public partial class ModifyTestPoint : Window
    {
        private ObservableCollection<TestPoint> testPoints;
        private int id;
        private string fileName;
        private string rtn_msg;

        public string Rtn_msg
        {
            get
            {
                return rtn_msg;
            }
        }

        /**
         * flag为 true 代表修改用户信息
         * flag为 false 代表添加用户
         */
        private bool flag;

        public ModifyTestPoint()
        {
            InitializeComponent();
        }

        public ModifyTestPoint(ObservableCollection<TestPoint> _testPoints, int _id, string _fileName)
        {
            InitializeComponent();
            this.testPoints = _testPoints;
            this.id = _id;
            this.fileName = _fileName;

            this.Name.Text = testPoints[id-1].Name;
            this.Address.Text = testPoints[id - 1].Address;
            this.DataType.Text = testPoints[id - 1].DataType;
            this.ClientAccess.Text = testPoints[id - 1].ClientAccess;
            this.ScanRate.Text = testPoints[id - 1].ScanRate;

            this.flag = true;
        }

        public ModifyTestPoint(ObservableCollection<TestPoint> _testPoints, string _fileName)
        {
            InitializeComponent();
            this.testPoints = _testPoints;
            this.fileName = _fileName;

            this.flag = false;
        }

        /// <summary>
        /// 鼠标左键按住可以移动窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void close_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_modify_Click(object sender, RoutedEventArgs e)
        {
            if (flag)
            {
                testPoints[id - 1].Name = this.Name.Text;
                testPoints[id - 1].Address = this.Address.Text;
                testPoints[id - 1].DataType = this.DataType.Text;
                testPoints[id - 1].ClientAccess = this.ClientAccess.Text;
                testPoints[id - 1].ScanRate = this.ScanRate.Text;

                CSVUtils.saveTestPointsToCSV(testPoints, fileName);

                this.rtn_msg = "测点修改成功！";
            }
            else
            {
                TestPoint t = new TestPoint();

                t.Id = (testPoints.Count + 1).ToString();
                t.Name = this.Name.Text;
                t.Address = this.Address.Text;
                t.DataType = this.DataType.Text;
                t.ClientAccess = this.ClientAccess.Text;
                t.ScanRate = this.ScanRate.Text;

                testPoints.Add(t);

                CSVUtils.saveTestPointsToCSV(testPoints, fileName);

                this.rtn_msg = "测点添加成功！";
            }
            
            this.Close();
        }
    }
}
