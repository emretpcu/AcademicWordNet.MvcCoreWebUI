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
	public class UpVoteService : IUpVote
	{
		IUpVoteDal _upVoteDal;
        public UpVoteService(IUpVoteDal upVote)
        {
            _upVoteDal= upVote;
        }

		public void DeleteUpVotesByEntryId(int entryId)
		{
			var upVotes = _upVoteDal.List(u => u.EntryId == entryId);

			foreach (var upVote in upVotes)
			{
				_upVoteDal.Delete(upVote);
			}
		}

		public void TDelete(UpVote t)
		{
			_upVoteDal.Delete(t);
		}

		public UpVote TFind(Expression<Func<UpVote, bool>> where)
		{
			return _upVoteDal.Find(where);
		}

		public UpVote TFind(Expression<Func<UpVote, bool>> where, params Expression<Func<UpVote, object>>[] includes)
		{
			return _upVoteDal.Find(where, includes);
		}

		public List<UpVote> TGetAll()
		{
			return _upVoteDal.List();
		}

		public List<UpVote> TGetAll(Expression<Func<UpVote, bool>> where)
		{
			return _upVoteDal.List(where);
		}

		public List<UpVote> TGetAll(Expression<Func<UpVote, bool>>? where, params Expression<Func<UpVote, object>>[] includes)
		{
			return _upVoteDal.List(where,includes);
		}

		public UpVote TGetById(int id)
		{
			return _upVoteDal.GetById(id);
		}

		public void TInsert(UpVote t)
		{
			_upVoteDal.Insert(t);
		}

		public void TUpdate(UpVote t)
		{
			_upVoteDal.Update(t);
		}
	}
}
