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

using Ak.Fw.Wpf;

namespace AkFileListCreator.View
{
    /// <summary>
    /// DirectoryItem.xaml の相互作用ロジック
    /// </summary>
    public partial class DirectoryItem : AkUserControlBase
    {
        internal delegate void FocusEventHandler(object sender, DirectoryItemEventArgs e);
        internal event FocusEventHandler FocusEvent;

        public DirectoryItem()
        {
            InitializeComponent();
        }


        private void DirectoryPathButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("未実装", "情報", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public string GetFolderPath()
        {
            return this.DirectoryPathTextBox.Text;
        }

        private void DirectoryPathTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            FocusEvent.Invoke(this, new DirectoryItemEventArgs { eventType = DirectoryItemEventArgs.EventType.Got });
        }

        private void DirectoryPathTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            FocusEvent.Invoke(this, new DirectoryItemEventArgs { eventType = DirectoryItemEventArgs.EventType.Lost });
        }

        internal class DirectoryItemEventArgs : EventArgs
        {
            public enum EventType
            {
                Got,
                Lost
            }

            public EventType eventType { get; set; }
        }
    }
}
