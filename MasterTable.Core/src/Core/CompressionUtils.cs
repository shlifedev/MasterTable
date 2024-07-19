// using System.IO.Compression;
// using System.Text;
// using Cysharp.Threading.Tasks;
//
// namespace MasterTable.Core
// {
//     public static class CompressionUtils
//     {
//         public static async UniTask<byte[]> CompressAsync(string source, CompressionLevel level)
//         {
//             var bytes = Encoding.UTF8.GetBytes(source);
//
//             await using var input = new MemoryStream(bytes);
//             await using var output = new MemoryStream();
//             await using var brotliStream = new BrotliStream(output, level);
//
//             await input.CopyToAsync(brotliStream);
//             await brotliStream.FlushAsync();
//
//             return output.ToArray();
//         }
//
//         public static async UniTask<byte[]> CompressBytesAsync(byte[] source, CompressionLevel level)
//         {
//             await using var input = new MemoryStream(source);
//             await using var output = new MemoryStream();
//             await using var brotliStream = new BrotliStream(output, level);
//
//             await input.CopyToAsync(brotliStream);
//             await brotliStream.FlushAsync();
//
//             return output.ToArray();
//         }
//
//         public static async UniTask<string> DecompressAsync(byte[] compressed)
//         {
//             await using var input = new MemoryStream(compressed);
//             await using var brotliStream = new BrotliStream(input, CompressionMode.Decompress);
//
//             await using var output = new MemoryStream();
//
//             await brotliStream.CopyToAsync(output);
//             await brotliStream.FlushAsync();
//
//             return Encoding.UTF8.GetString(output.ToArray());
//         }
//
//         public static async UniTask<byte[]> DecompressToBytesAsync(byte[] compressed)
//         {
//             await using var input = new MemoryStream(compressed);
//             await using var brotliStream = new BrotliStream(input, CompressionMode.Decompress);
//
//             await using var output = new MemoryStream();
//
//             await brotliStream.CopyToAsync(output);
//             await brotliStream.FlushAsync();
//
//             return output.ToArray();
//         }
//     }
// }