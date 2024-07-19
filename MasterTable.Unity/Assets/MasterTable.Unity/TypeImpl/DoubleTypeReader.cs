using MasterTable.Core;

namespace MasterTable.Unity.TypeImpl
{
    [DeclareType(typeof(double), "double")]
    public class DoubleTypeReader : IType<double>
    { 
        public double Read(string value)
        {
            return double.Parse(value);
        }
    }
}