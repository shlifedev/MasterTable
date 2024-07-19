using System;
using System.Collections.Generic;

namespace MasterTable.Generator;

public class DeclaredInfoContainer
{
    public List<DeclareInfo> InfoList = new();
    public Dictionary<string, DeclareInfo> DeclaredInfoMap = new();

    public void Add(DeclareInfo declareInfo)
    { 
        if (!DeclaredInfoMap.ContainsKey(declareInfo.GetTypeFullName()))
        {
            DeclaredInfoMap.Add(declareInfo.GetTypeFullName(), declareInfo);
            InfoList.Add(declareInfo);
        }

        foreach (var key in declareInfo.DeclaredAliases)
        {
            if (!DeclaredInfoMap.ContainsKey(key))
            {
                DeclaredInfoMap.Add(key, declareInfo);
            }
        }
    }
}