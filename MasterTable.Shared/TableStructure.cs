using System;
using System.Collections.Generic; 

namespace MasterTable.Shared;

[Serializable]
public struct TableStructure
{
    public TableStructure()
    {
    }

    public string TableName { get; set; }
    public List<string> Types { get; set; }
    public List<string> Fields { get; set; }
}