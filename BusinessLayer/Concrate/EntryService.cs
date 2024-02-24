using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrate;
using DataAccessLayer.Concrate.Repositories;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;



namespace BusinessLayer.Concrate
{
	public class EntryService : IEntryService
    {
        IEntryDal _contentDal;
        public EntryService(IEntryDal contentDal)
        {
            _contentDal=contentDal;
        }
		public void TDelete(Entry t)
        {
			_contentDal.Delete(t);
		}

        public Entry TFind(Expression<Func<Entry, bool>> where)
        {
            return _contentDal.Find(where);
        }

		public Entry TFind(Expression<Func<Entry, bool>> where, params Expression<Func<Entry, object>>[] includes)
		{
			return _contentDal.Find(where, includes);
		}

		public List<Entry> TGetAll()
        {
			return _contentDal.List();
		}

        public List<Entry> TGetAll(Expression<Func<Entry, bool>> where)
        {
            return _contentDal.List(where);
        }

		public List<Entry> TGetAll(Expression<Func<Entry, bool>>? where, params Expression<Func<Entry, object>>[] includes)
		{
			return _contentDal.List(where, includes);
		}

		public Entry TGetById(int id)
        {
            return _contentDal.GetById(id);
        }

        public void TInsert(Entry t)
        {
            _contentDal.Insert(t);
        }

        public void TUpdate(Entry t)
        {
            _contentDal.Update(t);
        }
    }
}
