// // using UnityEditor.AssetImporters;
// // using UnityEngine;
// //
// // [ScriptedImporter(1, "struct")]
// // public class ResxImporter : ScriptedImporter
// // {
// //     public override void OnImportAsset(AssetImportContext ctx)
// //     {
// //         var subAsset = new TextAsset(System.IO.File.ReadAllText(ctx.assetPath));
// //         ctx.AddObjectToAsset("t", subAsset);
// //         ctx.SetMainObject(subAsset);
// //     }
// // }
//
// using UnityEditor;
// using UnityEditor.Compilation;
// using UnityEngine;
//
// public class A
// {
//     [InitializeOnLoadMethod]
//     public static void E()
//     {
//         Debug.Log(ScriptCompilerOptions.RoslynAdditionalFilePaths);   
//     }
// }