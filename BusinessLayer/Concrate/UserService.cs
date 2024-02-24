using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concreate;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrate
{
	public class UserService : IUserService
	{
		IUserDal _userDal;
		public UserService(IUserDal userDal)
		{
			_userDal = userDal;
		}

		public void TDelete(User t)
		{
			_userDal.Delete(t);
		}

		public User TFind(Expression<Func<User, bool>> where, params Expression<Func<User, object>>[] includes)
		{
			return _userDal.Find(where, includes);	
		}

		public List<User> TGetAll()
		{
			return _userDal.List();
		}

		public List<User> TGetAll(Expression<Func<User, bool>> where)
		{
			return _userDal.List(where);
		}

		public List<User> TGetAll(Expression<Func<User, bool>>? where, params Expression<Func<User, object>>[] includes)
		{
			return _userDal.List(where, includes);
		}

		public User TGetById(int id)
		{
			return _userDal.GetById(id);
		}

		public void TInsert(User t)
		{
			_userDal.Insert(t);
		}

		public void TUpdate(User t)
		{
			_userDal.Update(t);
		}
	}
}
