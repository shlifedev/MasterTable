using System.Diagnostics;
using Cysharp.Threading.Tasks;
using GameDB;
using MasterTable.Core;

public class Program
{

    static async UniTask PP()
    {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        TypeInitializer.Initialize();
        ExcelTableReader reader = new ExcelTableReader();
        var readed =await  reader.ReadAsync("/Users/shlifedev/git.shlifedev/ld.table/MasterTable2323/MasterTable.Core/Excels/Unit.xlsx");
        
 
         
        Unit a = new Unit();
        Stopwatch sw = new Stopwatch();
          
        sw.Start();
        await a.DataTable.LoadAsync(readed.Sheets[0]);
        sw.Stop();
        Console.Write(sw.ElapsedMilliseconds);
    }
    static async Task Main()
    {
        await PP();


    }
}
//
//  
// public static class Schemas<T> where T : ISchema
// {
//         public static T Schema { get; set; }
//         public static bool isLoaded = false;
//         public static async UniTask Load(){
//                     
//         }
// }
//

