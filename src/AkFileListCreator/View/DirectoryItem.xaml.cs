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
    }
}
