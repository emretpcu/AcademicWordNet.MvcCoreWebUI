using EntityLayer.Concreate;

namespace HakinSozluk.MvcCoreWebUI.Models
{
	public class ForgotPasswordViewModel
	{
        public User user { get; set; }
        public int loginCode { get; set; }
        public string number { get; set; }
    }
}
