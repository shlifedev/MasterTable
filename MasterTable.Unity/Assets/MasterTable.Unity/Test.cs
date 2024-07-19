using System;
using System.Collections.Generic;
using MasterTable.Core;
using UnityEngine;

namespace MasterTable.Unity
{
    public class Test : MonoBehaviour
    {
        private void Awake()
        {
           // TypeInitializer.Initialize();
        }

        void Update()
        {

            //Debug.Log(string.Join(",", TypeReader<List<int>>.Reader.Read("1,2,3,4")));
            
        }
    }
}