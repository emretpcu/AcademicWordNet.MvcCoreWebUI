using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IGenericService<T>
    {
        void TInsert(T t);
        void TUpdate(T t);
        void TDelete(T t);
        List<T> TGetAll();
        List<T> TGetAll(Expression<Func<T, bool>> where);
        List<T> TGetAll(Expression<Func<T, bool>>? where, params Expression<Func<T, object>>[] includes);
        T TGetById(int id);
        T TFind(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);
    }
}
