using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrate
{
	public class HeadingService : IHeadingService
	{
		IHeadingDal _headingDal;
		public HeadingService(IHeadingDal headingDal)
		{
			_headingDal = headingDal;
		}

		public void TDelete(Heading t)
		{
			 _headingDal.Delete(t);
		}

		public Heading TFind(Expression<Func<Heading, bool>> where)
		{
			return _headingDal.Find(where);
		}

		public Heading TFind(Expression<Func<Heading, bool>> where, params Expression<Func<Heading, object>>[] includes)
		{
			return _headingDal.Find(where, includes);
		}

		public List<Heading> TGetAll()
		{
			return _headingDal.List();
		}

		public List<Heading> TGetAll(Expression<Func<Heading, bool>> where)
		{
			return _headingDal.List(where);
		}

		public List<Heading> TGetAll(Expression<Func<Heading, bool>>? where, params Expression<Func<Heading, object>>[] includes)
		{
			return _headingDal.List(where, includes);
		}

		public Heading TGetById(int id)
		{
			return _headingDal.GetById(id);
		}

		public void TInsert(Heading t)
		{
			_headingDal.Insert(t);
		}

		public void TUpdate(Heading t)
		{
		   _headingDal.Update(t);
		}
	}
}
