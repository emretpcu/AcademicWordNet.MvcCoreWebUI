using BusinessLayer.Abstract;
using BusinessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concreate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TatliSozluk.MvcCoreWebUI.Models;

namespace TatliSozluk.MvcCoreWebUI.Controllers
{
    public class HeadingController : Controller
    {
		EntryService _entryService = new EntryService(new EfEntryDal());
		HeadingViewModel headingViewModel = new HeadingViewModel();
		CategoryService _categoryService = new CategoryService(new EfCategoryDal());
		HeadingService _headingService = new HeadingService(new EfHeadingDal());
		private readonly UserManager<User> _userManager;
        public HeadingController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index(int id)
        {
			headingViewModel.Heading = _headingService.TFind(c => c.Id == id && c.Category.CategoryStatus == true&&c.IsApporoved==true, x => x.Category);
            if (headingViewModel.Heading!=null)
            {
				var list = _entryService.TGetAll(c => c.HeadingId == id && c.Heading.IsApporoved == true&&c.Status==true, c => c.User, c => c.Heading);
				headingViewModel.Entries = list;
				headingViewModel.IsForMainHeadingPage = true;
				return View(headingViewModel);
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
        }

		[Authorize]
		[HttpGet]
		public async Task<IActionResult> AddHeading()
		{
			AddHeadingViewModel addHeadingViewModel = new AddHeadingViewModel();
			addHeadingViewModel.Categories = _categoryService.TGetAll(c => c.CategoryStatus == true);
			return View(addHeadingViewModel);
		}
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> AddHeading(AddHeadingViewModel addHeadingViewModel)
		{
			var userId = _userManager.GetUserId(HttpContext.User);
			var check = _headingService.TFind(h => h.Title == addHeadingViewModel.Heading.Title.ToLower());
			if (check == null && addHeadingViewModel.Entry.Explanation != null)
			{
				addHeadingViewModel.Heading.CreatedDate = DateTime.Now;
				addHeadingViewModel.Heading.IsApporoved = false;
				addHeadingViewModel.Heading.BeenOpenedBefore = false;
				addHeadingViewModel.Heading.FromWhoId = userId;
				_headingService.TInsert(addHeadingViewModel.Heading);
				var headingId = _headingService.TFind(h => h.Title == addHeadingViewModel.Heading.Title).Id;

				addHeadingViewModel.Entry.HeadingId = headingId;
				addHeadingViewModel.Entry.UserId = userId;
				addHeadingViewModel.Entry.CreatedDate = DateTime.Now;
				addHeadingViewModel.Entry.Status =true;
				_entryService.TInsert(addHeadingViewModel.Entry);

				TempData["SuccessfullyAddedHeading"] = "Başlık isteğiniz sıraya alındı en " +
					"kısa zamanda bir moderatör tarafından incelenip sisteme eklenecektir."; //null gelmesin diye
				return RedirectToAction("Index", "Home");
			}
			else
			{
				TempData["HeadingError"] = "Başlık Oluşturulamadı !!";
				return RedirectToAction("addheading", "heading");
			}
		}
	}
}
