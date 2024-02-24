using DataAccessLayer.Abstract;
using DataAccessLayer.Concrate;
using DataAccessLayer.Concrate.Repositories;
using EntityLayer.Concreate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
	public class EfEntryDal : GenericRepository<Entry>, IEntryDal
	{
	}
}
