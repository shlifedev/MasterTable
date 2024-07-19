using System.Collections.Generic;
using System.Linq;
using MasterTable.Core;

namespace MasterTable.Unity.TypeImpl
{
    [DeclareType(typeof(List<int>), "list<int>")]
    public class IntListTypeReader : IType<List<int>>
    { 
        public List <int> Read(string value)
        { 
            
            return value.Split(',').Select(int.Parse).ToList(); 
        }
    }
}