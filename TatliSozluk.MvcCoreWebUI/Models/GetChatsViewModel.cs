using EntityLayer.Concreate;

namespace TatliSozluk.MvcCoreWebUI.Models
{
	public class GetChatsViewModel
	{
        public User User { get; set; }
        public IEnumerable<Message> Messages { get; set; }
        public string WithWhoUsername { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
