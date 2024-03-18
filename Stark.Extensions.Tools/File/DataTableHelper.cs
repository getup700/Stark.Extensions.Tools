using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stark.Extensions.Tools.File;

public static class DataTableHelper
{
    /// <summary>
    /// 查询指定列的DataTable
    /// </summary>
    /// <param name="source"></param>
    /// <param name="resultTableName"></param>
    /// <param name="columns"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="Exception"></exception>
    public static DataTable GetTableColumn(DataTable source, string resultTableName = "default table", params string[] columns)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }
        var dataTable = new DataTable();
        try
        {
            dataTable = source.DefaultView.ToTable("defaultTable", false, columns);
        }
        catch (Exception)
        {
            throw new Exception("Search failure");
        }
        return dataTable;
    }

    /// <summary>
    /// 获取DataTable的重复值
    /// </summary>
    /// <param name="dataTable"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    public static List<string> FindDuplicateData(DataTable dataTable, DataColumn column)
    {
        var initialValue = new List<string>();
        foreach (DataRow item in dataTable.Rows)
        {
            initialValue.Add(item[column].ToString());
        }
        var result = initialValue.GroupBy(x => x)
            .Where(x => x.Count() > 1)
            .Select(x => x.Key).ToList();
        return result;
    }
}
