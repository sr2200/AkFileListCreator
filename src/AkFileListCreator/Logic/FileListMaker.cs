using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("AkFileListCreator.Tests")]

namespace AkFileListCreator.Logic
{
    internal class FileListMaker
    {
        internal DataColumn[] Columns { get; set; }

        public FileListMaker()
        {
            Columns = new DataColumn[]{
                new DataColumn("ファイル名",typeof(string)),
                new DataColumn("フルパス",typeof(string)),
                new DataColumn("フォルダパス",typeof(string)),
                new DataColumn("親フォルダ",typeof(string)),
                new DataColumn("拡張子",typeof(string)),
                new DataColumn("サイズ",typeof(long)),
                new DataColumn("更新日付",typeof(DateTime)),
                new DataColumn("MD5",typeof(string)),
                new DataColumn("SHA256",typeof(string))
            };
        }

        internal DataTable MakeList(List<string> lst)
        {
            var tbl = new DataTable("FileList");
            tbl.Columns.AddRange(Columns);

            foreach (var path in lst)
            {
                var dInfo = new DirectoryInfo(path);
                MakeList(dInfo, tbl);
            }

            return tbl;
        }

        internal void MakeList(DirectoryInfo dInfo, DataTable tbl)
        {
            foreach (var dir in dInfo.GetDirectories())
            {
                MakeList(dir, tbl);
            }

            foreach (var file in dInfo.GetFiles())
            {
                var row = tbl.NewRow();
                tbl.Rows.Add(row);
                foreach (var col in Columns)
                {
                    switch (col.ColumnName)
                    {
                        case "ファイル名":
                            row[col] = file.Name;
                            break;
                        case "フルパス":
                            row[col] = file.FullName;
                            break;
                        case "フォルダパス":
                            row[col] = file.DirectoryName;
                            break;
                        case "親フォルダ":
                            row[col] = file.Directory.Name;
                            break;
                        case "拡張子":
                            row[col] = file.Extension;
                            break;
                        case "サイズ":
                            row[col] = file.Length;
                            break;
                        case "更新日付":
                            row[col] = file.LastWriteTime;
                            break;
                        case "MD5":
                            row[col] = GetMd5(file.FullName);
                            break;
                        case "SHA256":
                            row[col] = GetSha256(file.FullName);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        internal string GetMd5(string filePath)
        {
            string ret = string.Empty;
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var hashProvider = MD5.Create();
                var bs = hashProvider.ComputeHash(fs);
                ret = BitConverter.ToString(bs).ToUpper().Replace("-", String.Empty);
            }
            return ret;
        }

        internal string GetSha256(string filePath)
        {
            string ret = string.Empty;
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var hashProvider = SHA256.Create();
                var bs = hashProvider.ComputeHash(fs);
                ret = BitConverter.ToString(bs).ToUpper().Replace("-", String.Empty);
            }
            return ret;
        }
    }
}
