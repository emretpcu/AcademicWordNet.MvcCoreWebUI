using BusinessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concreate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TatliSozluk.MvcCoreWebUI.Models;

namespace TatliSozluk.MvcCoreWebUI.ViewComponents.Layout
{
	public class _CategoryList : ViewComponent
	{
		private readonly UserManager<User> _userManager;
		public _CategoryList(UserManager<User> userManager)
		{
			_userManager = userManager;
		}
		CategoryService categoryManager = new CategoryService(new EfCategoryDal());
		public IViewComponentResult Invoke()
		{
			LayoutViewModel layoutViewModel = new LayoutViewModel();
			layoutViewModel.Categories = categoryManager.TGetAll(c => c.CategoryStatus == true);
			layoutViewModel.Users = _userManager.Users;
			return View(layoutViewModel);
		}
	}
}
