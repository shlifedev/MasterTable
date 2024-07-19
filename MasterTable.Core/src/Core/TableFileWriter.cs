using System.Text.Json;
using Cysharp.Threading.Tasks;
using MasterTable.Core;
using MasterTable.Shared; 
using MemoryPack;


namespace MasterTable.Core
{
    public static class TableFileWriter
    {
        public static async UniTask WriteAsync(VirtualSpreadSheet sheet, string dirPath)
        {
            string dataDirPath = Path.Combine(dirPath, Const.DataFolder);
            string structDirPath = Path.Combine(dirPath, Const.StructFolder);


            byte[] serializedVirtualSpreadSheet;
            string serializedStructureData;

            using (var ms = new MemoryStream())
            {
                await MemoryPackSerializer.SerializeAsync(ms, sheet);
                serializedVirtualSpreadSheet = ms.ToArray();
            }

            {
                var structure = StructureCreator.CreateSchemaStructure(ref sheet);
                serializedStructureData = JsonSerializer.Serialize(structure);
            }

            if (!Directory.Exists(dataDirPath))
                Directory.CreateDirectory(dataDirPath);
            if (!Directory.Exists(structDirPath))
                Directory.CreateDirectory(structDirPath);


            await System.IO.File.WriteAllBytesAsync(Path.Combine(dataDirPath,
                    sheet.Metadata.SpreadSheetName + Const.DataExtension),
                serializedVirtualSpreadSheet);


            await System.IO.File.WriteAllTextAsync(Path.Combine(structDirPath,
                    sheet.Metadata.SpreadSheetName + Const.StructExtension),
                serializedStructureData);
        }
    }
}