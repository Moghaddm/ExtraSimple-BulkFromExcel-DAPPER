using OfficeOpenXml;
using System.Data;

namespace Extensions;

public static class ExcelHandler
{
    public static DataTable Read(string path)
    {
        DataTable dataTable = new DataTable();
        using (var workSheet = new ExcelPackage(path).Workbook.Worksheets[0])
        {
            try
            {
                var totalColumns = workSheet.Dimension.End.Column;
                var totalRows = workSheet.Dimension.End.Row;
                for (int columnIndex = 1; columnIndex < totalColumns; columnIndex++)
                {
                    dataTable.Columns.Add(workSheet.Cells[1, columnIndex].Value.ToString());
                }
                for (int rowId = 1; rowId < totalRows; rowId++)
                {
                    var newRow = dataTable.NewRow();
                    for (int columnId = 1; columnId < totalColumns; columnId++)
                    {
                        newRow[columnId - 1] = workSheet.Cells[rowId, columnId].Value;
                    }
                    dataTable.Rows.Add(newRow);
                }
                return dataTable;
            }
            catch (System.Exception exception)
            {
                throw new ArgumentException(exception.Message);
            }
        }
    }
}
