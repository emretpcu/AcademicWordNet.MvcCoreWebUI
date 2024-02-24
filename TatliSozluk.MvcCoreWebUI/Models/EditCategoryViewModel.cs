using EntityLayer.Concreate;

namespace TatliSozluk.MvcCoreWebUI.Models
{
	public class EditCategoryViewModel
	{
        public Category Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
