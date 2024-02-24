using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrate
{
	public class BadWordService : IBadWordService
	{
		IBadWordDal _badWordDal;
		public BadWordService(IBadWordDal badWordDal)
		{
			_badWordDal = badWordDal;
		}
		public void TDelete(BadWord t)
		{
			_badWordDal.Delete(t);
		}

		public BadWord TFind(Expression<Func<BadWord, bool>> where)
		{
			return _badWordDal.Find(where);
		}

		public BadWord TFind(Expression<Func<BadWord, bool>> where, params Expression<Func<BadWord, object>>[] includes)
		{
			return _badWordDal.Find(where, includes);
		}

		public List<BadWord> TGetAll()
		{
			return _badWordDal.List();
		}

		public List<BadWord> TGetAll(Expression<Func<BadWord, bool>> where)
		{
			return _badWordDal.List(where);
		}

		public List<BadWord> TGetAll(Expression<Func<BadWord, bool>>? where, params Expression<Func<BadWord, object>>[] includes)
		{
			return _badWordDal.List(where, includes);
		}

		public BadWord TGetById(int id)
		{
		   return _badWordDal.GetById(id);
		}

		public void TInsert(BadWord t)
		{
			_badWordDal.Insert(t);
		}

		public void TUpdate(BadWord t)
		{
			_badWordDal.Update(t);
		}

	}
}
