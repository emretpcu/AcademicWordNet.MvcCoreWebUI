using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concreate
{
    public class FriendRequest
    {
        [Key]
        public int Id { get; set; }

        public string SenderId { get; set; }
        public User Sender { get; set; }


        public string ReceiverId { get; set; }
        public User Receiver { get; set; }


        public bool IsAccepted { get; set; }

        public DateTime RequestDate { get; set; } = DateTime.Now;
    }
}
