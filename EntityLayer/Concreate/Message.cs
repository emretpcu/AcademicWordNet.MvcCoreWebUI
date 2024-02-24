using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concreate
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; } = false;

        public string SenderId { get; set; }
        public User Sender { get; set; }


        public string ReceiverId { get; set; }
        public User Receiver { get; set; }
    }
}
