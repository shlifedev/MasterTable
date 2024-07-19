using System.Collections.Generic;
using MemoryPack;

namespace MasterTable.Core
{

    [MemoryPackable]
    public partial class VirtualSheetCellTableGroup
    {
        public VirtualSheetMetadata Metadata;
        public List<List<VirtualCell>> Table = new();

        public VirtualSheetCellTableGroup(VirtualSheetMetadata metadata)
        {
            Metadata = metadata;
            Table = new();
        }

        public int RowCount => Table.Count;
        public int ColumnCount => Table[0].Count;

        public IEnumerable<VirtualCell> GetRow(int rowIndex)
        {
            var row = Table[rowIndex];
            for (int column = 0; column < ColumnCount; column++)
                yield return row[column];
        }

        public IEnumerable<VirtualCell> GetAll()
        {
            for (int row = 0; row < Table.Count; row++)
            {
                for (int col = 0; col < Table[row].Count; col++)
                {
                    yield return Table[row][col];
                }
            }
        }
    }
}