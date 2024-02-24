using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrate;
using DataAccessLayer.Concrate.Repositories;
using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrate
{
    public class CategoryService : ICategoryService
    {
        ICategoryDal _categoryDal;
        public CategoryService(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
		public void TDelete(Category t)
        {
            _categoryDal.Delete(t);
        }

        public Category TFind(Expression<Func<Category, bool>> where)
        {
            return _categoryDal.Find(where);
        }

		public Category TFind(Expression<Func<Category, bool>> where, params Expression<Func<Category, object>>[] includes)
		{
            return _categoryDal.Find(where, includes);
		}

		public List<Category> TGetAll()
        {
            return _categoryDal.List();

		}

        public List<Category> TGetAll(Expression<Func<Category, bool>> where)
        {
			return _categoryDal.List(where);
		}

		public List<Category> TGetAll(Expression<Func<Category, bool>>? where, params Expression<Func<Category, object>>[] includes)
		{
            return _categoryDal.List(where, includes);
		}

		public Category TGetById(int id)
        {
            return _categoryDal.GetById(id);
        }

        public void TInsert(Category t)
        {
            _categoryDal.Insert(t);
        }

        public void TUpdate(Category t)
        {
           _categoryDal.Update(t);
        }
    }
}
