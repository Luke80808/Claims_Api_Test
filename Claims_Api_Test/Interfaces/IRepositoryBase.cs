namespace Claims_Api_Test.Interfaces;

public interface IRepositoryBase<T>
{
    void Add(T entity);
    T Get(string id);
}
