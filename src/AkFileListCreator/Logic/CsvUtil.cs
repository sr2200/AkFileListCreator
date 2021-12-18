using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkFileListCreator.Logic
{
    internal class CsvUtil
    {
        internal void WriteCsv(string path, DataTable tbl)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (var writer = new StreamWriter(path, true, Encoding.UTF8))
            {
                StringBuilder line = new StringBuilder();

                foreach (DataColumn column in tbl.Columns)
                {
                    line.Append($"\"{column.ColumnName}\",");
                }
                line = line.Remove(line.Length - 1, 1);
                writer.WriteLine(line);

                line.Clear();
                foreach (DataRow row in tbl.Rows)
                {
                    foreach (DataColumn column in tbl.Columns)
                    {
                        line.Append($"\"{row[column].ToString()}\",");
                    }

                    line = line.Remove(line.Length - 1, 1);
                    writer.WriteLine(line);
                    line.Clear();
                }
            }
        }
    }
}
