using EntityLayer.Concreate;

namespace TatliSozluk.MvcCoreWebUI.Models
{
	public class AssignAdminViewModel
	{
		public string UserName { get; set; } = string.Empty;
		public string UsernameAgain { get; set; } = string.Empty;

        public IEnumerable<User> Users { get; set; }
    }
}
