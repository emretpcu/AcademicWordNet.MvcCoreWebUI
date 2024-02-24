using EntityLayer.Concreate;

namespace TatliSozluk.MvcCoreWebUI.Models
{
	public class ProfileViewModel
	{
		public IEnumerable<Entry> Entries { get; set; }
		public User User { get; set; }
	}
}
