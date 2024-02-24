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
	public class StudentInformationService : IStudentInformationService
	{
		IStudentInformationDal _IStudentInfoDal;
        public StudentInformationService(IStudentInformationDal studentInfoDal)
        {
            _IStudentInfoDal = studentInfoDal;
        }
		public void TDelete(StudentInformation t)
		{
			_IStudentInfoDal.Delete(t);
		}

		public StudentInformation TFind(Expression<Func<StudentInformation, bool>> where, params Expression<Func<StudentInformation, object>>[] includes)
		{
			return _IStudentInfoDal.Find(where, includes);
		}

		public List<StudentInformation> TGetAll()
		{
			return _IStudentInfoDal.List();
		}

		public List<StudentInformation> TGetAll(Expression<Func<StudentInformation, bool>> where)
		{
			return _IStudentInfoDal.List(where);
		}

		public List<StudentInformation> TGetAll(Expression<Func<StudentInformation, bool>>? where, params Expression<Func<StudentInformation, object>>[] includes)
		{
			return _IStudentInfoDal.List(where,includes);
		}

		public StudentInformation TGetById(int id)
		{
			return _IStudentInfoDal.GetById(id);
		}

		public void TInsert(StudentInformation t)
		{
			_IStudentInfoDal.Insert(t);
		}

		public void TUpdate(StudentInformation t)
		{
			_IStudentInfoDal.Update(t);
		}
	}
}
