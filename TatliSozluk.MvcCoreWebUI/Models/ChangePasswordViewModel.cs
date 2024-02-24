using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TatliSozluk.MvcCoreWebUI.Models
{
    public class ChangePasswordViewModel
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
