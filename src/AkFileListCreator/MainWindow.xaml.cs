using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Ak.Fw.Wpf;

using AkFileListCreator.Logic;
using AkFileListCreator.Share;
using AkFileListCreator.View;

using Microsoft.Win32;

using Path = System.IO.Path;

namespace AkFileListCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : AkWindowBase
    {
        private readonly Dictionary<PageEnum, AkPageBase> pageDic = new Dictionary<PageEnum, AkPageBase>();

        public MainWindow()
        {
            InitializeComponent();

            OutputFileButtion.IsEnabled = false;

            pageDic[PageEnum.DirectoryList] = new DirectoryListPage();
            pageDic[PageEnum.FileList] = new FileListPage();

            //pageDic[PageEnum.DirectoryList].RaiseCustomEvent += (sender, e) =>
            //{

            //};

            //pageDic[PageEnum.FileList].RaiseCustomEvent += (sender, e) =>
            //{

            //};

            var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            path = Path.Combine(path, "FileList.xlsx");
            this.OutputFilePath.Text = path;

            frame.Navigate(pageDic[PageEnum.DirectoryList]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OutputFileButtion_Click(object sender, RoutedEventArgs e)
        {
            string dirPath = string.Empty;
            if (!string.IsNullOrWhiteSpace(this.OutputFilePath.Text))
            {
                var filePath = this.OutputFilePath.Text;
                var fInfo = new FileInfo(filePath);
                dirPath = fInfo.DirectoryName;
            }
            else
            {
                dirPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }

            var dialog = new SaveFileDialog
            {
                FileName = this.OutputFilePath.Text,
                InitialDirectory = dirPath,
                Filter = "Excelファイル(*.xlsx)|*.xlsx|CSVファイル(*.csv)|*.csv|全てのファイル(*.*)|*.*"
            };
            var result = dialog.ShowDialog();

            if (result == true)
            {
                this.OutputFilePath.Text = dialog.FileName;
            }
        }

        /// <summary>
        /// リスト作成ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MakeFileListButton_Click(object sender, RoutedEventArgs e)
        {
            var prg = new FileListMaker();
            var lst = new List<string>();

            var panel = pageDic[PageEnum.DirectoryList].FindName("panel") as Grid;
            foreach (DirectoryItem item in panel.Children)
            {
                var path = item.DirectoryPathTextBox.Text;
                if (string.IsNullOrWhiteSpace(path))
                {
                    continue;
                }

                if (!Directory.Exists(path))
                {
                    continue;
                }

                lst.Add(path);
            }

            if (0 == lst.Count)
            {
                MessageBox.Show("フォルダが指定されていません。",
                                "警告",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            var tbl = prg.MakeList(lst);

            ((FileListPage)pageDic[PageEnum.FileList]).Data = tbl;
            frame.Navigate(pageDic[PageEnum.FileList]);

            OutputFileButtion.IsEnabled = true;

            MessageBox.Show("リスト作成完了", 
                            "情報", 
                            MessageBoxButton.OK, 
                            MessageBoxImage.Information);
        }

        /// <summary>
        /// 書き込みボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OutputFileButton_Click(object sender, RoutedEventArgs e)
        {
            var path = OutputFilePath.Text;
            if (string.IsNullOrWhiteSpace(path))
            {
                MessageBox.Show("出力ファイルが指定されていません。",
                                "警告",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            var fInfo = new FileInfo(path);
            if (".xlsx" != fInfo.Extension)
            {
                MessageBox.Show("Excelファイル(*.xlsx)以外は現時点ではサポート外です。",
                                "警告",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            DataTable tbl = ((FileListPage)pageDic[PageEnum.FileList]).Data;

            if (null == tbl)
            {
                MessageBox.Show("ファイル一覧が作成されていません。",
                                "警告",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            var excel = new ExcelUtil();
            excel.WriteExcel(path, tbl);

            MessageBox.Show("ファイル出力完了",
                "情報",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        /// <summary>
        /// ファイルリスト作成＆書き込みボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MakeListAndOutputButton_Click(object sender, RoutedEventArgs e)
        {
            var prg = new FileListMaker();
            var lst = new List<string>();

            var panel = pageDic[PageEnum.DirectoryList].FindName("panel") as Grid;
            foreach (DirectoryItem item in panel.Children)
            {
                var path = item.DirectoryPathTextBox.Text;
                if (string.IsNullOrWhiteSpace(path))
                {
                    continue;
                }

                if (!Directory.Exists(path))
                {
                    continue;
                }

                lst.Add(path);
            }

            if (0 == lst.Count)
            {
                MessageBox.Show("フォルダが指定されていません。",
                                "警告",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            var outputPath = OutputFilePath.Text;
            if (string.IsNullOrWhiteSpace(outputPath))
            {
                MessageBox.Show("出力ファイルが指定されていません。",
                                "警告",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            var fInfo = new FileInfo(outputPath);
            if (".xlsx" != fInfo.Extension)
            {
                MessageBox.Show("Excelファイル(*.xlsx)以外は現時点ではサポート外です。",
                                "警告",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            var tbl = prg.MakeList(lst);

            ((FileListPage)pageDic[PageEnum.FileList]).Data = tbl;
            frame.Navigate(pageDic[PageEnum.FileList]);

            if (null == tbl)
            {
                MessageBox.Show("ファイル一覧が作成されていません。",
                                "警告",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            var excel = new ExcelUtil();
            excel.WriteExcel(outputPath, tbl);

            MessageBox.Show("ファイルリスト作成完了＆ファイル出力完了",
                "情報",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}
