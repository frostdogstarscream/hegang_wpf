using HFD.KEPWare;
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

namespace Hegang.APP
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private KEPWareDataAdapter da;
        public MainWindow()
        {
            InitializeComponent();
            da = new KEPWareDataAdapter();

            this.chk_treeview.ItemsSource = Node.test();

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btn_connect_Click(object sender, RoutedEventArgs e)
        {
            this.btn_connect.IsEnabled = false;
            object serverList = da.GetLocalServer();
            List<string> serverListToString = new List<string>();
            foreach (string turn in (Array)serverList)
            {
                serverListToString.Add(turn);
            }
            this.cmb_server_list.ItemsSource = serverListToString;
        }

        
    }
}
