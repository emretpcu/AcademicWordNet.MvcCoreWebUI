using EntityLayer.Concreate;

namespace TatliSozluk.MvcCoreWebUI.Models
{
	public class HeadingViewModel
	{
        public Heading Heading { get; set; }
		public IEnumerable<Entry> Entries { get; set; }

        public bool IsForMainHeadingPage { get; set; }
    }
}
