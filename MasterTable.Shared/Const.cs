namespace MasterTable.Shared
{

    public class Const
    {
        public const string StructExtension = ".struct";
        public const string DataExtension = ".tabledata";

        public const string StructFolder = "Generated/StructData";
        public const string DataFolder = "Generated/SchemaData";


        public const string StructReadmeContent = $@"
# StructData

Struct Data is require for roslyn-based code generation.

";

        public const string SchemaReadmeContent = $@"
# SchemaData

Schema Data is require for data loading. 
Memory Pack will load schema data and generate data loader very fast.

";
    }
}