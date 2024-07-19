using System.Collections.Generic;
using MemoryPack;

namespace MasterTable.Core;

[MemoryPackable]
public partial class VirtualSheetCellGroup
{
    public VirtualSheetMetadata Metadata;
    public List<VirtualCell> Values = new();

    public VirtualSheetCellGroup(VirtualSheetMetadata metadata)
    {
        Metadata = metadata;
        Values = new();
    }
}