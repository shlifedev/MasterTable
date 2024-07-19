namespace MasterTable;

public interface IType<T>
{
    public T Read(string value); 
}