using System.Collections; 
using MemoryPack;

namespace MasterTable.Core
{
    [MemoryPackable] 
    [Serializable]
    public partial class VirtualSheet
    {
        
        public VirtualSheetMetadata Metadata;
        public VirtualSheetCellGroup Fields;
        public VirtualSheetCellGroup Types;
        public VirtualSheetCellGroup Comments;
        public VirtualSheetCellTableGroup DataRanges;

        public VirtualSheet(VirtualSheetMetadata metadata)
        {
            Metadata = metadata;
            Fields = new VirtualSheetCellGroup(Metadata);
            Types = new VirtualSheetCellGroup(Metadata);
            Comments = new VirtualSheetCellGroup(Metadata);
            DataRanges = new VirtualSheetCellTableGroup(Metadata);
        }



        public void AppendCell(VirtualCell cell)
        {
            if (cell.IsCommantRow)
            {
                Comments.Values.Add(cell);
            }

            if (cell.IsFieldRow)
            {
                Fields.Values.Add(cell);
            }

            if (cell.IsTypeRow)
            {
                Types.Values.Add(cell);
            }


            if (cell.IsDataRow)
            {
                try
                {
                    if (DataRanges.Table.Count <= cell.CurrentDataRowIndex)
                        DataRanges.Table.Add(new List<VirtualCell>());
                    DataRanges.Table[cell.CurrentDataRowIndex].Add(cell);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine(cell.RowIndex);

                    Console.WriteLine(cell.DataRowIndex);
                    Console.WriteLine(cell.RowIndex - cell.DataRowIndex);
                    throw;
                }
            }


        }


        public IEnumerator<string> GetRow(int row)
        {
            foreach (var column in DataRanges.Table[row])
            {
                yield return column.Value;
            }
        }



        public void AppendToSpreadSheet(VirtualSpreadSheet sheet)
        {
            sheet.AppendSheet(this);
        }
    }
}