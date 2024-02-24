using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concreate
{
    public class Friend
    {
        [Key]
        public int Id { get; set; }

        public string User1Id { get; set; }
        public virtual User User1 { get; set; }

        public string User2Id { get; set; }
        public virtual User User2 { get; set; }

        public DateTime FriendSince { get; set; } 


    }
}
