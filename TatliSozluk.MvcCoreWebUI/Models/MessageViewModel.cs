using EntityLayer.Concreate;

namespace TatliSozluk.MvcCoreWebUI.Models
{
    public class MessageViewModel
    {
        public IEnumerable<Message> Messages { get; set; }
        public Message MessageForReply { get; set; }
        public Message Message { get; set; }
        public bool IsSendBox { get; set; }
        public bool IsReply { get; set; }
        public bool IsGetMessageReply { get; set; }
    }
}
