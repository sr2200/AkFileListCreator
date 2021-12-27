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

        public DirectoryItem FocusCtrl { get; set; }

        private void CreateRow()
        {
            var row = new RowDefinition();
            row.Height = new GridLength(30);
            panel.RowDefinitions.Add(row);

            var item = new DirectoryItem();
            item.FocusEvent += (sender, e) => {
                switch (e.eventType)
                {
                    case DirectoryItem.DirectoryItemEventArgs.EventType.Got:
                        FocusCtrl = (DirectoryItem)sender;
                        break;
                    default:
                        break;
                }
            };

            var rowIndex = panel.Children.Count;
            Grid.SetRow(item, rowIndex);
            panel.Children.Add(item);
        }

        private void AddRow(DirectoryItem item)
        {
            var row = new RowDefinition();
            row.Height = new GridLength(30);
            panel.RowDefinitions.Add(row);

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
            if(null != FocusCtrl)
            {
                panel.Children.Remove(FocusCtrl);

                List<DirectoryItem> lst = new List<DirectoryItem>();
                foreach (DirectoryItem item in panel.Children)
                {
                    lst.Add(item);
                }

                panel.Children.Clear();
                panel.RowDefinitions.Clear();

                foreach (DirectoryItem item in lst)
                {
                    AddRow(item);
                }

                FocusCtrl = null;
            }
        }
    }
}
