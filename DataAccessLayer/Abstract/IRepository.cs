using EntityLayer.Concreate;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
	public interface IRepository<T>
	{
		List<T> List();
		void Insert(T p);
		void Update(T p);
		void Delete(T p);
        List<T> List(Expression<Func<T, bool>> where);
		List<T> List(Expression<Func<T, bool>>? where, params Expression<Func<T, object>>[] includes);
        T GetById(int id);
		T Find(Expression<Func<T, bool>> where);
		T Find(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);

	}
}
