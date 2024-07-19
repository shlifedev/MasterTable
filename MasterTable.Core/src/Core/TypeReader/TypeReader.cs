namespace MasterTable.Core
{ 
    /// <summary>
    /// Generator
    /// TypeReader.Reader = new IntTypeReader();
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class TypeReader<T>
    {
        public static IType<T>? Reader { get; set; }
    }
}