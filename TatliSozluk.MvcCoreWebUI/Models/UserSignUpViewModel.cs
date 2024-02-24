using Microsoft.Build.Framework;

namespace TatliSozluk.MvcCoreWebUI.Models
{
    public class UserSignUpViewModel
    {
        [Required]
        public string NameSurname { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
		[Required]
		public string Number { get; set; }
		[Required]
		public int Code { get; set; }
    }
}
