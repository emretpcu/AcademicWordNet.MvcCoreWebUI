using Microsoft.Build.Framework;

namespace TatliSozluk.MvcCoreWebUI.Models
{
    public class UserSignInViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
