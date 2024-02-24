using EntityLayer.Concreate;

namespace TatliSozluk.MvcCoreWebUI.Models
{
	public class LayoutViewModel
	{
        public List<Category> Categories { get; set; }
		public IEnumerable<User> Users { get; set; }
	}
}
