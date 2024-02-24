using EntityLayer.Concreate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TatliSozluk.MvcCoreWebUI.Models;

namespace TatliSozluk.MvcCoreWebUI.ViewComponents.Profile
{
	public class _ProfileLayout : ViewComponent
	{
		private readonly UserManager<User> _userManager;
		public _ProfileLayout(UserManager<User> userManager)
		{
			_userManager = userManager;
		}
		public IViewComponentResult Invoke()
		{
			string getUsername = _userManager.GetUserName(HttpContext.User);
			UserIdViewModel userIdView = new UserIdViewModel();
			userIdView.username = getUsername;
			return View(userIdView);
		}
	}
}
