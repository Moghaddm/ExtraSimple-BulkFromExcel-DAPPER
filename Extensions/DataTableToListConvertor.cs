using System.Data;
using System.Reflection;

namespace Extensions;

public static class DataTableToListConvertor<T>
    where T : new()
{
    public static List<T> Convert(DataTable dataTable)
    {
        var datas = new List<T>();
        var properties = typeof(T).GetProperties();

        foreach (DataRow row in dataTable.Rows)
        {
            var item = new T();
            foreach (PropertyInfo property in properties)
            {
                if (dataTable.Columns.Contains(property.Name))
                {
                    object value = row[property.Name];
                    if (value != DBNull.Value)
                    {
                        property.SetValue(item, value);
                    }
                }
            }
            datas.Add(item);
        }
        return datas;
    }
}
