using EntityLayer.Concreate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace DataAccessLayer.Concrate
{
	public class Context : IdentityDbContext<User>
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//var configuration = new ConfigurationBuilder()
			//	.SetBasePath(Directory.GetCurrentDirectory())
			//	.AddJsonFile("appsettings.json")
			//	.Build();
			//var connectionString = configuration.GetConnectionString("DefaultConnection");
			//optionsBuilder.UseSqlServer(connectionString);

			var connectionString = "Your Connection String";
			optionsBuilder.UseSqlServer(connectionString);
		}
		public DbSet<Category> Categories { get; set; }
		public DbSet<Heading> Headings { get; set; }
		public DbSet<Entry> Entries { get; set; }
		public DbSet<Friend> Friends { get; set; }
		public DbSet<StudentInformation> StudentInformations{ get; set; }
		public DbSet<FriendRequest> FriendRequests { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<UpVote> UpVotes { get; set; }
		public DbSet<BadWord> BadWords { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Entry>()
				.HasOne(m => m.User)
				.WithMany(u => u.Entries)
				.HasForeignKey(m => m.UserId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Friend>()
				.HasOne(m => m.User1)
				.WithMany(u => u.Friends1s)
				.HasForeignKey(m => m.User1Id)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Friend>()
				.HasOne(m => m.User2)
				.WithMany(u => u.Friends2s)
				.HasForeignKey(m => m.User2Id)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Message>()
				.HasOne(m => m.Sender)
				.WithMany(u => u.MessagesSenders)
				.HasForeignKey(m => m.SenderId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Message>()
				.HasOne(m => m.Receiver)
				.WithMany(u => u.MessagesReceivers)
				.HasForeignKey(m => m.ReceiverId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<FriendRequest>()
				.HasOne(fr => fr.Sender)
				.WithMany(u => u.SentFriendRequests)
				.HasForeignKey(fr => fr.SenderId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<FriendRequest>()
				.HasOne(fr => fr.Receiver)
				.WithMany(u => u.ReceivedFriendRequests)
				.HasForeignKey(fr => fr.ReceiverId)
				.OnDelete(DeleteBehavior.Restrict);


			modelBuilder.Entity<UpVote>()
				   .HasOne(u => u.Entry)
				   .WithMany(c => c.UpVotes)
				   .HasForeignKey(u => u.EntryId)
				   .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Heading>()
				.HasOne(h => h.FromWho)
				.WithMany(u => u.Headings)
				.HasForeignKey(h => h.FromWhoId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<User>()
					   .HasOne(u => u.StudentLogin)
					   .WithMany() // Assuming StudentInformation.Users is a collection navigation property
					   .HasForeignKey(u => u.StudentLoginCode)
					   .OnDelete(DeleteBehavior.Cascade);

			//modelBuilder.Entity<StudentInformation>()
			//	   .HasOne(u => u.Users)
			//	   .WithMany() // Assuming StudentInformation.Users is a collection navigation property
			//	   .HasForeignKey(u => u.StudentTcNumber)
			//	   .OnDelete(DeleteBehavior.Restrict);

		}
	}
}


