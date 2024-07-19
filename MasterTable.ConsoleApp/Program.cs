using Cysharp.Threading.Tasks;
using GameDB;
using GameDB.Core;

public class Program
{
    static void Main()
    {
        GameDB.Unit a = null;
        
    }
}


public static class Schemas<T> where T : ISchema
{
        public static T Schema { get; set; }
        public static bool isLoaded = false;
        public static async UniTask Load(){
                    
        }
}


