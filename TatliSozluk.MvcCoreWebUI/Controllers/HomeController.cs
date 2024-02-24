using BusinessLayer.Abstract;
using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concreate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.NetworkInformation;
using TatliSozluk.MvcCoreWebUI.Models;

namespace TatliSozluk.MvcCoreWebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Context _context;

        public HomeController(ILogger<HomeController> logger,Context context)
        {
            _logger = logger;
            _context = context;
        }
		EntryService _entryService = new EntryService(new EfEntryDal());
		public IActionResult Index()
		{
			return View(_entryService.TGetAll(x =>x.Status==true&& x.CreatedDate >= DateTime.Now.AddMonths(-1)&& x.Heading.IsApporoved==true&&x.Heading.Category.CategoryStatus==true, c => c.User, c => c.Heading).OrderByDescending(x=>x.UpVote).ToList());
		}

		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}