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
    public class MessageService : IMessageService
    {
        IMessageDal _messageDal;
        public MessageService(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public void TDelete(Message t)
        {
            _messageDal.Delete(t);
        }

        public Message TFind(Expression<Func<Message, bool>> where)
        {
            return _messageDal.Find(where);
        }

		public Message TFind(Expression<Func<Message, bool>> where, params Expression<Func<Message, object>>[] includes)
		{
			return _messageDal.Find(where, includes);
		}

		public List<Message> TGetAll()
        {
            return _messageDal.List();
        }

        public List<Message> TGetAll(Expression<Func<Message, bool>> where)
        {
			return _messageDal.List(where);
		}

		public List<Message> TGetAll(Expression<Func<Message, bool>>? where, params Expression<Func<Message, object>>[] includes)
		{
			return _messageDal.List(where, includes);
		}

		public Message TGetById(int id)
        {
            return _messageDal.GetById(id);
        }

        public void TInsert(Message t)
        {
            _messageDal.Insert(t);
        }

        public void TUpdate(Message t)
        {
			_messageDal.Update(t);
		}
    }
}
