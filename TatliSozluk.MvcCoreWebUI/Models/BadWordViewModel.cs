using EntityLayer.Concreate;

namespace TatliSozluk.MvcCoreWebUI.Models
{
	public class BadWordViewModel
	{
        public BadWord BadWord { get; set; }
        public IEnumerable<BadWord> BadWords { get; set; }
    }
}
