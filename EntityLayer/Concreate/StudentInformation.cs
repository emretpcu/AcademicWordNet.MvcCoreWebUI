using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concreate
{
	public class StudentInformation
	{
		[Key]
		public int LoginCode { get; set; }
        public string Class { get; set; }
		public string Number { get; set; }
		public string NameSurname { get; set; }

        public ICollection<User> User { get; set; }
	}
}
