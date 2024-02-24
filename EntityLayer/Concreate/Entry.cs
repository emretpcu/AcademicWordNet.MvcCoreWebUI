using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concreate
{
    public class Entry
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int HeadingId { get; set; }
        public Heading Heading { get; set; }

        public string Explanation { get; set; }

        public int UpVote { get; set; }
        public bool Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public ICollection<UpVote> UpVotes { get; set; }
	}
}
