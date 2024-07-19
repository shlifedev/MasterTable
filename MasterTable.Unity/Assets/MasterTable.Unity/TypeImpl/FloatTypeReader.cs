using MasterTable.Core;

namespace MasterTable.Unity.TypeImpl
{
    [DeclareType(typeof(float), "float")]
    public class FloatTypeReader : IType<float>
    { 
        public float Read(string value)
        { 
            
            return float.Parse(value);
        }
    }
}