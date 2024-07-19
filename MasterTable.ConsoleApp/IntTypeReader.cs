using Cysharp.Threading.Tasks;
using MasterTable.Core;

namespace MasterTable.Unity.TypeImpl
{
    [DeclareType(typeof(int), "int, int32")]
    public class IntTypeReader : IType<int>
    { 
        public int Read(string value)
        {
            return int.Parse(value);
        }
    }
}