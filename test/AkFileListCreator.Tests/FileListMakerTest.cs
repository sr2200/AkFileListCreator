using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;

using AkFileListCreator.Logic;

using Xunit;
using Xunit.Abstractions;

namespace AkFileListCreator.Test;

public class FileListMakerTest
{

    private readonly ITestOutputHelper output;

    public FileListMakerTest(ITestOutputHelper output)
    {
        this.output = output;
    }

    [Fact]
    public void MakeListTest01()
    {
        var dir = Environment.CurrentDirectory;

        output.WriteLine("カレントディレクトリ：");
        output.WriteLine($"{dir}");

        var lst = new List<string>();
        lst.Add(Path.Combine(dir, "TestData"));

        var prg = new FileListMaker();
        var result = prg.MakeList(lst);

        foreach (DataRow row in result.Rows)
        {
            foreach (DataColumn col in result.Columns)
            {
                output.WriteLine($"{col.ColumnName}：{row[col].ToString()}");
            }
        }
    }

    [Fact]
    public void GetMd5Test01()
    {
        var dir = Environment.CurrentDirectory;
        var path = Path.Combine(dir, "TestData", "File.txt");
        var md5 = "142F8970DA344DCEC57E9FA33A6FA509";

        var prg = new FileListMaker();
        var result = prg.GetMd5(path);

        Assert.Equal(md5, result);
    }

    [Fact]
    public void GetSha256Test01()
    {
        var dir = Environment.CurrentDirectory;
        var path = Path.Combine(dir, "TestData", "File.txt");
        var sha256 = "6051D9B1C47B52CEF5475D25F759D2B71CCC9CCDFED3EF9FE3A22333EF6786AC";

        var prg = new FileListMaker();
        var result = prg.GetSha256(path);

        Assert.Equal(sha256, result);
    }
}