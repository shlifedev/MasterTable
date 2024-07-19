 

using System.Data; 
using Cysharp.Threading.Tasks;
using ExcelDataReader;
using MasterTable.Core;

namespace MasterTable.Core
{ 
    public class ExcelTableReader : ITableReader, IDisposable
    {
#pragma warning disable CS1998 // 이 비동기 메서드에는 'await' 연산자가 없으며 메서드가 동시에 실행됩니다.
        public async UniTask<VirtualSpreadSheet> ReadAsync(string filePath)
#pragma warning restore CS1998 // 이 비동기 메서드에는 'await' 연산자가 없으며 메서드가 동시에 실행됩니다.
        {  
            var streamer = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            List<DataTable> dataTables = new List<DataTable>();
            using (var reader = ExcelReaderFactory.CreateReader(streamer))
            {
                var excelFileName = Path.GetFileNameWithoutExtension(filePath);
                
                VirtualSpreadSheet virtualSpreadSheet =
                    new VirtualSpreadSheet(new VirtualSpreadSheetMetadata(excelFileName));

                // Load Sheets
                foreach (var table in reader.AsDataSet().Tables)
                    dataTables.Add((DataTable)table);


                foreach (var sheet in dataTables)
                {
                    var virtualSheet = new VirtualSheet(new VirtualSheetMetadata(sheet.TableName, reader.Name));
                    virtualSheet.AppendToSpreadSheet(virtualSpreadSheet);


                    for (int i = 0; i < sheet.Rows.Count; i++)
                    {
                        for (int j = 0; j < sheet.Columns.Count; j++)
                        {
                            var data = new VirtualCell(i, j, sheet.Rows[i][j].ToString());
                            virtualSheet.AppendCell(data);
                        }
                    }
                }

                return virtualSpreadSheet;
            }
        }
 
 
        public void Dispose()
        {

        }
    }
}