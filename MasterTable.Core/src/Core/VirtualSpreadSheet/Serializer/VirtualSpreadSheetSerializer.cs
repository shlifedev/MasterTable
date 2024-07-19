using Cysharp.Threading.Tasks;
using MemoryPack;

namespace MasterTable.Core
{ 
    public static class VirtualSpreadSheetSerializer
    {
        public static async UniTask<byte[]> SerializeAsync(VirtualSpreadSheet spreadSheet)
        { 
            
            using (MemoryStream ms = new MemoryStream())
            { 
                await MemoryPackSerializer.SerializeAsync(ms, spreadSheet);
                return ms.ToArray();
            }
        }

        public static async UniTask<byte[]> Serialize(VirtualSpreadSheet spreadSheet)
        {
            return MemoryPackSerializer.Serialize(spreadSheet);
        }
    }
}