using EntityLayer.Concreate;

namespace TatliSozluk.MvcCoreWebUI.Models
{
	public class AdminDashboardViewModel
	{
		public int MessagesToday { get; set; }
		public int HeadingsToday { get; set; }
		public int EntriesToday { get; set; }

		public int AllMessages { get; set; }
		public int AllHeadings { get; set; }
		public int AllEntries { get; set; }
		public int Categories { get; set; }

		public int WaitingForApporove { get; set; }
		public int AllHeadingsWithoutApporove { get; set; }

		public int BadWords { get; set; }
	}
}
