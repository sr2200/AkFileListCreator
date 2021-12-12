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
    /// DirectoryListPage.xaml の相互作用ロジック
    /// </summary>
    public partial class DirectoryListPage : AkPageBase
    {
        public DirectoryListPage()
        {
            InitializeComponent();

            CreateRow();
        }

        private void CreateRow()
        {
            var row = new RowDefinition();
            row.Height = new GridLength(30);
            panel.RowDefinitions.Add(row);

            var item = new DirectoryItem();

            var rowIndex = panel.Children.Count;
            Grid.SetRow(item, rowIndex);
            panel.Children.Add(item);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            CreateRow();
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("未実装");
        }
    }
}
