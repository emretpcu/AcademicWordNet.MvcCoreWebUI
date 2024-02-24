using BusinessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace TatliSozluk.MvcCoreWebUI.Controllers
{
	public class CategoryController : Controller
	{
		EntryService _entryService = new EntryService(new EfEntryDal());
		CategoryService _categoryService = new CategoryService(new EfCategoryDal());
		public IActionResult Index(int id)
		{
			var checkStatus = _categoryService.TFind(x => x.Id == id).CategoryStatus;
			if (checkStatus == true)
			{
				return View(_entryService.TGetAll(x => x.Status == true && x.Heading.CategoryId == id && x.Heading.IsApporoved == true, c => c.Heading, c => c.User).OrderByDescending(c => c.CreatedDate).ToList());
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}
	}
}
