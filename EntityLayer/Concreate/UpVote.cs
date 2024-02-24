using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concreate
{
	public class UpVote
	{
        public int Id { get; set; }
		public string UserId { get; set; }
		public User User { get; set; }
		public int EntryId { get; set; }
		public virtual Entry Entry { get; set; }

	}
}
