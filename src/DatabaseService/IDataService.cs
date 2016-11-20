using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel;

namespace DatabaseService
{
    public interface IDataService<T>
    {
        IList<T> GetList(int page, int pagesize);
        T Get(int id);
        void Add(T someDbObject);
        bool Update(T someDbObject);
        bool Delete(int id);
        int Count();
    }
}
