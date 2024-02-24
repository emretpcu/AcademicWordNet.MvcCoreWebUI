using BusinessLayer.Abstract;
using BusinessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concreate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using TatliSozluk.MvcCoreWebUI.Models;
using Microsoft.Build.Framework;
using Microsoft.AspNetCore.Authorization;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace TatliSozluk.MvcCoreWebUI.Controllers
{
    public class EntryController : Controller
    {
        HeadingService _headingService = new HeadingService(new EfHeadingDal());
        EntryService _entryService = new EntryService(new EfEntryDal());
        UpVoteService _upVoteService = new UpVoteService(new EfUpVoteDal());
        BadWordService _badWordService = new BadWordService(new EfBadWordDal());
        private readonly UserManager<User> _userManager;
        public EntryController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index(int id)
        {
            return View();
        }
        [Authorize]
        public IActionResult AddEntry(Entry ct)
        {
            var headingStatus = _headingService.TFind(x => x.Id == ct.HeadingId);
            if (headingStatus.IsApporoved == false)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var badWords = _badWordService.TGetAll();
                string userBadWords = "";

                foreach (var badWord in badWords)
                {
                    // Boşlukları temizle ve StringComparison.OrdinalIgnoreCase kullanarak karşılaştırma yap
                    if (ct.Explanation.Replace(" ", "").IndexOf(badWord.Word.Replace(" ", ""), StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        if (userBadWords == "")
                        {
                            userBadWords = badWord.Word;
                        }
                        else
                        {
                            userBadWords += ", " + badWord.Word;
                        }
                    }
                }

                if (userBadWords != "")
                {
                    TempData["BadWordsFound"] = "Yasaklı kelime kullandığınız için entry gönderilememiştir.";
                    return RedirectToAction("Index", "Heading", new { id = ct.HeadingId });
                }
                else
                {
                    var userId = _userManager.GetUserId(HttpContext.User);
                    ct.UserId = userId;
                    ct.CreatedDate = DateTime.Now;
                    ct.UpVote = 0;
                    ct.Status = true;
                    _entryService.TInsert(ct);
                }
            }


            return RedirectToAction("Index", "Heading", new { id = ct.HeadingId });
        }
        [Authorize]
        [HttpPost]
        public IActionResult UpTo(int id)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var entry = _entryService.TFind(c => c.Id == id);

            if (entry == null)
            {
                // Belirtilen ID'ye sahip girdi bulunamadı
                return NotFound();
            }

            var upVote = _upVoteService.TFind(x => x.UserId == userId && x.EntryId == id);
            var hasUpvoted = upVote != null;

            try
            {
                if (!hasUpvoted)
                {
                    // Kullanıcı daha önce oy kullanmamışsa, upvote sayısını artır
                    entry.UpVote++;
                    _entryService.TUpdate(entry);

                    UpVote newUpVote = new UpVote
                    {
                        EntryId = entry.Id,
                        UserId = userId
                    };

                    _upVoteService.TInsert(newUpVote);
                }
                else
                {
                    // Kullanıcı daha önce oy kullandıysa, upvote sayısını azalt
                    entry.UpVote--;
                    _entryService.TUpdate(entry);

                    _upVoteService.TDelete(upVote);
                }

                return Json(new { upvoteCount = entry.UpVote, hasUpvoted });
            }
            catch (Exception ex)
            {
                // Hata yönetimi: Loglama veya uygun bir hata mesajı döndürebilirsiniz
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        public IActionResult Details(int id)
        {
            try
            {
                var entry = _entryService.TFind(x => x.Id == id && x.Status == true, x => x.Heading, x => x.User);
                if (entry != null && entry.Heading.IsApporoved == true)
                {
                    return View(entry);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }

        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var findEntry = _entryService.TFind(x => x.Id == id,x=>x.User);
                if (findEntry.User.UserName==User.Identity.Name)
                {
                    _upVoteService.DeleteUpVotesByEntryId(findEntry.Id);
                    _entryService.TDelete(findEntry);
                }
                return RedirectToAction("Profile", "Account", new { username = findEntry.User.UserName });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index","Home");
            }
        }
    }
}
