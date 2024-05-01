namespace Claims_Api.Interfaces;

public interface IRepositoryBase<T>
{
    void Add(T entity);
    T? Get(string id);
}
