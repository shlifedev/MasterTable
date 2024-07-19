using MemoryPack;

namespace MasterTable.Core;

[MemoryPackable]
public partial record VirtualSheetMetadata : VirtualTableMetadata
{
    [MemoryPackConstructor]
    public VirtualSheetMetadata(string sheetName, string? sheetId, string originSpreadSheetName,
        string? originSpreadSheetId)
    {
        SheetName = sheetName;
        SheetId = sheetId;
        OriginSpreadSheetName = originSpreadSheetName;
        OriginSpreadSheetId = originSpreadSheetId;
    }

    public VirtualSheetMetadata(string sheetName, string originSpreadSheetName)
    {
        SheetName = sheetName;
        OriginSpreadSheetName = originSpreadSheetName;
    }

    public string SheetName { get; set; }
    public string? SheetId { get; set; }

    public string OriginSpreadSheetName { get; set; }
    public string? OriginSpreadSheetId { get; set; }

    public bool IsFromGoogleSpreadSheet =>
        string.IsNullOrEmpty(SheetId) == false && string.IsNullOrEmpty(OriginSpreadSheetId) == false;
}