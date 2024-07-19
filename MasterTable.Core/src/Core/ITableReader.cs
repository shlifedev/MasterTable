 

using Cysharp.Threading.Tasks;
using MasterTable;
using MasterTable.Core;

namespace MasterTable.Core
{

     public interface ITableReader
     {
          UniTask<VirtualSpreadSheet> ReadAsync(string filePath);
     }

}