using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace AkFileListCreator.Logic
{
    internal class JsonUtil
    {
        internal void WriteJson(string path, DataTable tbl)
        {
            var jsonStr = JsonConvert.SerializeObject(tbl, Formatting.Indented);
            File.WriteAllText(path, jsonStr, Encoding.UTF8);
        }
    }
}
