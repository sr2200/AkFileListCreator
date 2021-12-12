using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using ClosedXML.Excel;

[assembly: InternalsVisibleTo("AkFileListCreator.Tests")]

namespace AkFileListCreator.Logic
{
    internal class ExcelUtil
    {

        internal void WriteExcel(string path, DataTable tbl)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(tbl);
                workbook.SaveAs(path);
            }
        }
    }
}