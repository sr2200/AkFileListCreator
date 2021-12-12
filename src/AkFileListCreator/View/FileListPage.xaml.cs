using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
    /// FileListPage.xaml の相互作用ロジック
    /// </summary>
    public partial class FileListPage : AkPageBase
    {
        public DataTable Data { get; set; }

        public FileListPage()
        {
            InitializeComponent();

            this.IsVisibleChanged += (sender, e) => {
                if (this.IsVisible)
                {
                    Debug.WriteLine("表示");
                    dataGrid.DataContext = Data;
                }
                else
                {
                    Debug.WriteLine("非表示");
                }
            };
        }
    }
}
