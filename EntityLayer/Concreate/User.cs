using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concreate
{
    public class User:IdentityUser
    {
        [Key]
        public int Id { get; set; }

        public string NameSurname { get; set; }
        public string? ImageUrl { get; set; }

        public int? StudentLoginCode { get; set; }
        public virtual StudentInformation StudentLogin { get; set; }

		public string? Number { get; set; }


		public DateTime CreatedDate { get; set; }

        public ICollection<Entry> Entries { get; set; }


        public ICollection<Friend> Friends1s { get; set; }
        public ICollection<Friend> Friends2s { get; set; }

        public ICollection<FriendRequest> SentFriendRequests { get; set; }
        public ICollection<FriendRequest> ReceivedFriendRequests { get; set; }

        public ICollection<Message> MessagesSenders { get; set; }
        public ICollection<Message> MessagesReceivers { get; set; }


        public ICollection<UpVote> UpVotes { get; set; }

        public ICollection<Heading> Headings { get; set; }

    }
}
