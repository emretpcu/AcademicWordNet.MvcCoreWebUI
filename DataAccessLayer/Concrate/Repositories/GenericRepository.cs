using DataAccessLayer.Abstract;
using EntityLayer.Concreate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrate.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        public void Delete(T p)
        {
            using var context = new Context();
            var _object = context.Set<T>();
            _object.Remove(p);
            context.SaveChanges();
        }
            
        public T Find(Expression<Func<T, bool>> where)
        {
            using var context = new Context();
            var _object = context.Set<T>();
            return _object.FirstOrDefault(where);
        }

		public T Find(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes)
		{
			using var context = new Context();

			IQueryable<T> query = context.Set<T>();

			// include ifadeleri varsa hepsini birer birer Include metoduna ekliyoruz
			if (includes != null && includes.Length > 0)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}

			// filtrelemeyi gerçekleştiriyoruz
			return query.FirstOrDefault(where);
		}

		public T GetById(int id)
        {
            using var context = new Context();
            var _object = context.Set<T>();
            return _object.Find(id);
        }

        public void Insert(T p)
        {
            using var context = new Context();
            var _object = context.Set<T>();
            _object.Add(p);
            context.SaveChanges();
        }

        public List<T> List()
        {
            using var context = new Context();
            var _object = context.Set<T>();
            return _object.ToList();
        }

        public List<T> List(Expression<Func<T, bool>> where)
        {
            using var context = new Context();
            var query = context.Set<T>();


            return query.Where(where).ToList();
        }

		public List<T> List(Expression<Func<T, bool>>? where, params Expression<Func<T, object>>[] includes)
		{
			using var context = new Context();

			IQueryable<T> query = context.Set<T>();

			// include ifadeleri varsa hepsini birer birer Include metoduna ekliyoruz
			if (includes != null && includes.Length > 0)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}

			if (where!=null)
            {
				// filtrelemeyi gerçekleştiriyoruz
				return query.Where(where).ToList();
				
			}
            else
            {
				return query.ToList();
			}
		}

		public void Update(T p)
        {
            using var context = new Context();
            var _object = context.Set<T>();
            _object.Update(p);
            context.SaveChanges();
        }

    }
}
