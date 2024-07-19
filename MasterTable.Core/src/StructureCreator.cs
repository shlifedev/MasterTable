using MasterTable.Core;

namespace MasterTable.Core;

public class StructureCreator
{
    public static global::MasterTable.Shared.SchemaStructure CreateSchemaStructure(ref VirtualSpreadSheet from)
    {
        global::MasterTable.Shared.SchemaStructure schemaStructure = new();
        schemaStructure.SchemaName = from.Metadata.SpreadSheetName;
        foreach (var table in from.Sheets)
        {
            var tableStructure = new global::MasterTable.Shared.TableStructure();
            tableStructure.Fields = table.Fields.Values.Select(x => x.Value).ToList();
            tableStructure.Types = table.Types.Values.Select(x => x.Value).ToList();
            tableStructure.TableName = table.Metadata.SheetName; 
            schemaStructure.Tables.Add(tableStructure);
        } 
        return schemaStructure;
    } 
}