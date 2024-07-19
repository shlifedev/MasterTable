using System;
using System.Collections.Generic;
using System.Xml.Serialization; 

namespace MasterTable.Shared
{
    [Serializable] 
    public struct SchemaStructure
    {
        public SchemaStructure()
        {
        }

        public string SchemaName { get; set; }
        public List<TableStructure> Tables { get; set; } = new();
    }
} 