using System.Collections.Generic; 
using MemoryPack;

namespace MasterTable.Core
{

    [MemoryPackable]
    public partial class VirtualSpreadSheet
    {
        public VirtualSpreadSheetMetadata Metadata;
        public List<VirtualSheet> Sheets = new();

        public VirtualSpreadSheet(VirtualSpreadSheetMetadata metadata)
        {
            Metadata = metadata;
        }



        public void AppendSheet(VirtualSheet sheet)
        {
            Sheets.Add(sheet);
        } 
    }
}