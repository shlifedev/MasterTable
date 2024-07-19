using MemoryPack;


namespace MasterTable.Core
{


    [MemoryPackable]
    public partial class VirtualCell
    {
        public int RowIndex { get; }
        public int ColumnIndex { get; }
        public string Value { get; }

        [MemoryPackIgnore] public bool IsTypeRow => RowIndex == TypeRowIndex;
        [MemoryPackIgnore] public bool IsFieldRow => RowIndex == FieldRowIndex;
        [MemoryPackIgnore] public bool IsDataRow => RowIndex >= DataRowIndex;
        [MemoryPackIgnore] public bool IsCommantRow => RowIndex == CommentRowIndex;

        [MemoryPackIgnore] public int TypeRowIndex => 0;
        [MemoryPackIgnore] public int FieldRowIndex => 1;
        [MemoryPackIgnore] public int CommentRowIndex => 2;
        [MemoryPackIgnore] public int DataRowIndex => 3;
        [MemoryPackIgnore] public int CurrentDataRowIndex => RowIndex - DataRowIndex;


 


        public VirtualCell(int rowIndex, int columnIndex, string value)
        { 
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
            Value = value;
        }
    }
}