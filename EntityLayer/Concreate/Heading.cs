using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concreate
{
    public class Heading
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        public int CategoryId { get; set; }
        
        public  Category Category { get; set; }
        public DateTime CreatedDate { get; set; }

        public string FromWhoId { get; set; }
        public User FromWho { get; set; }

		public bool BeenOpenedBefore { get; set; }
		public bool IsApporoved { get; set; }
		

        public ICollection<Entry> Entries { get; set; }
    }
}
