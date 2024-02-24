using EntityLayer.Concreate;

namespace TatliSozluk.MvcCoreWebUI.Models
{
	public class AddHeadingViewModel
	{
		public IEnumerable<Category> Categories{ get; set; }
		public Heading Heading { get; set; }	
		public Entry Entry { get; set; }

		public AddHeadingViewModel()
		{
			Entry = new Entry();
		}

	}
}
