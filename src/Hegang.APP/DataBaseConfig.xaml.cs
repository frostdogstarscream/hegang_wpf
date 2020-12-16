﻿using System.IO;
using System.Windows;
using System.Windows.Input;
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

        /// <summary>
        /// 为窗体中的每一个TextBlock赋值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Utils.get_db_config_path());

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

        /// <summary>
        /// 将改动保存到db_config.xml
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_apply_Click(object sender, RoutedEventArgs e)
        {
            string path = Utils.get_db_config_path();
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

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
