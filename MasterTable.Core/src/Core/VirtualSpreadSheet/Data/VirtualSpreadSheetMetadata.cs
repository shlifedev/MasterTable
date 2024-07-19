using MemoryPack;

namespace MasterTable.Core
{
    [MemoryPackable]
    public partial record VirtualSpreadSheetMetadata : VirtualTableMetadata
    {
    [MemoryPackConstructor]
    public VirtualSpreadSheetMetadata(string spreadSheetName, string? spreadSheetId)
    {
        SpreadSheetName = spreadSheetName;
        SpreadSheetId = spreadSheetId;
    }

    public VirtualSpreadSheetMetadata(string spreadSheetName)
    {
        SpreadSheetName = spreadSheetName;
    }

    public string SpreadSheetName { get; set; }
    public string? SpreadSheetId { get; set; }


    public bool IsFromGoogleSpreadSheet => string.IsNullOrEmpty(SpreadSheetId) == false;
    }
}