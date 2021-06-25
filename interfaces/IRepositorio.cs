using System.Collections.Generic;

namespace SeriesCrud.interfaces
{
    public interface IRepositorio<T>
    {
        List<T> Lista();
        T ReturnForId(int id);
        void Insert(T entidade);
        void Delete(int id);
        void Update(int id, T entidade);
        int NextId();
    }
}