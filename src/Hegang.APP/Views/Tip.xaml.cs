using System.Windows;
using System.Windows.Input;

namespace Hegang.APP
{
    /// <summary>
    /// Tip.xaml 的交互逻辑
    /// </summary>
    public partial class Tip : Window
    {
        public Tip()
        {
            InitializeComponent();
        }

        public Tip(string text)
        {
            InitializeComponent();
            this.tip_textBlock.Text = text;
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
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
