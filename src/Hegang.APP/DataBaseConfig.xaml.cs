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
using System.Windows.Shapes;
using System.Xml;

namespace Hegang.APP
{
    /// <summary>
    /// DataBaseConfig.xaml 的交互逻辑
    /// </summary>
    public partial class DataBaseConfig : Window
    {
        public DataBaseConfig()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void minimize_btn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void close_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private string get_db_config_path()
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            Directory.SetCurrentDirectory(Directory.GetParent(path).FullName);
            path = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(Directory.GetParent(path).FullName);
            path = Directory.GetCurrentDirectory();

            return path + @"/conf/db_config.xml";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(this.get_db_config_path());

            XmlElement element = (XmlElement)doc.SelectSingleNode("property/server");
            this.server.Text = element.GetAttribute("value");

            element = (XmlElement)doc.SelectSingleNode("property/port");
            this.port.Text = element.GetAttribute("value");

            element = (XmlElement)doc.SelectSingleNode("property/user");
            this.user.Text = element.GetAttribute("value");

            element = (XmlElement)doc.SelectSingleNode("property/password");
            this.password.Text = element.GetAttribute("value");

            element = (XmlElement)doc.SelectSingleNode("property/database");
            this.database.Text = element.GetAttribute("value");
        }

        private void btn_apply_Click(object sender, RoutedEventArgs e)
        {
            string path = this.get_db_config_path();
            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            XmlElement element = (XmlElement)doc.SelectSingleNode("property/server");
            element.SetAttribute("value", this.server.Text);

            element = (XmlElement)doc.SelectSingleNode("property/port");
            element.SetAttribute("value", this.port.Text);

            element = (XmlElement)doc.SelectSingleNode("property/user");
            element.SetAttribute("value", this.user.Text);

            element = (XmlElement)doc.SelectSingleNode("property/password");
            element.SetAttribute("value", this.password.Text);

            element = (XmlElement)doc.SelectSingleNode("property/database");
            element.SetAttribute("value", this.database.Text);

            doc.Save(path);
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
