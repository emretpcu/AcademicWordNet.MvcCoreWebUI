using BusinessLayer.Abstract;
using BusinessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concreate;
using HakinSozluk.MvcCoreWebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.CodeAnalysis.CSharp;
using System.Linq;
using System.Text;
using TatliSozluk.MvcCoreWebUI.Models;

namespace TatliSozluk.MvcCoreWebUI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private EntryService _entryService = new EntryService(new EfEntryDal());
        private StudentInformationService _studentInfoService = new StudentInformationService(new EfStudentInformationDal());

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        IMailService _mailService;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IMailService mailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mailService = mailService;

        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(UserSignInViewModel u)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(u.Username, u.Password, false, true);
                if (result.Succeeded)
                {
                    ViewBag.Username = u.Username;
                    var user = await _userManager.FindByNameAsync(u.Username);
                    ViewBag.Role = await _userManager.GetRolesAsync(user);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(UserSignUpViewModel p)
        {
            if (ModelState.IsValid)
            {
                User usr = new User()
                {
                    UserName = p.Username,
                    Email = p.Email,
                    NameSurname = p.NameSurname,
                    CreatedDate = DateTime.Now,
                    Number = p.Number,
                    StudentLoginCode = p.Code,
                    EmailConfirmed = true,
                };

                var existingUser = await _userManager.FindByEmailAsync(usr.Email);
                var checkLoginCode = _userManager.Users.FirstOrDefault(x => x.StudentLoginCode == usr.StudentLoginCode);
				if (existingUser == null&&checkLoginCode==null)
                {
                    var check = _studentInfoService.TFind(x => x.Number == usr.Number && x.LoginCode == usr.StudentLoginCode, x => x.User);
                    if (check != null)
                    {
                        usr.NameSurname = check.NameSurname;
                        var result = await _userManager.CreateAsync(usr, p.Password);

                        if (result.Succeeded)
                        {
                            TempData["SuccessfullyRegistration"] = "Başarılı bir şekilde kayıt oldunuz !!";
                            return RedirectToAction("Login", "Account");
                        }
                        else
                        {
                            foreach (var item in result.Errors)
                            {
                                ModelState.AddModelError("", item.Description);
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Bazı bilgiler eksik.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Bu maile sahip bir kullanıcı zaten bulunmakta.");
                }
            }
            return View(p);
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            ViewBag.Role = null;
            ViewBag.Username = null;
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Profile(string username)
        {
            ProfileViewModel profileModel = new ProfileViewModel();
            User usr = await _userManager.FindByNameAsync(username);
            string userId = await _userManager.GetUserIdAsync(usr);

            profileModel.Entries = _entryService.TGetAll(x => x.UserId == userId && x.Heading.IsApporoved == true && x.Status == true && x.Heading.Category.CategoryStatus == true, i => i.User, i => i.Heading).OrderByDescending(c => c.CreatedDate).ToList();
            var account = await _userManager.FindByIdAsync(userId);
            profileModel.User = account;
            return View(profileModel);
        }
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User model)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);

            user.NameSurname = model.NameSurname;
            user.UserName = model.UserName;
            if (user.Email != model.Email)
            {
                user.Email = model.Email;
                user.EmailConfirmed = false;
            }
            user.ImageUrl = model.ImageUrl;
            var Result = await _userManager.UpdateAsync(user);
            if (!Result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı güncellenemedi.");
                return View(model);
            }
            else
            {
                TempData["Update"] = "Güncelleme Başarılı Lütfen Yeni Bilgilerinizle Tekrar Giriş Yapınız.";
                await _signInManager.SignOutAsync();
                return RedirectToAction("Login", "Account");
            }
        }
        //[HttpGet]
        //public async Task<IActionResult> EmailConfirmation()
        //{
        //    Random rnd = new Random();
        //    int code = rnd.Next(1000, 9999);
        //    TempData["usercode"] = code;

        //    var user = await _userManager.GetUserAsync(HttpContext.User);
        //    var mailCheck = await _userManager.IsEmailConfirmedAsync(user);
        //    if (mailCheck != true)
        //    {
        //        _mailService.SendMailAsync(user.Email, "Mail Doğrulama", "<p>Mail doğrulama kodunuz:</p> <br/> <strong>" + code + "</strong>");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }

        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> EmailConfirmation(string code)
        //{
        //    if (code == TempData["usercode"].ToString())
        //    {
        //        var user = await _userManager.GetUserAsync(HttpContext.User);
        //        user.EmailConfirmed = true;
        //        var Result = await _userManager.UpdateAsync(user);
        //        if (!Result.Succeeded)
        //        {
        //            TempData["EmailError"] = "Mail Doğrulanamadı.";
        //            return RedirectToAction("Edit", "Account");
        //        }
        //        else
        //        {
        //            TempData["EmailSuccess"] = "Başarılı bir şekilde mailinizi doğruladınız.";
        //            return RedirectToAction("Edit", "Account");
        //        }

        //    }
        //    else
        //    {
        //        TempData["EmailError"] = "Mail Doğrulanamadı.";
        //        return RedirectToAction("Edit", "Account");
        //    }


        //}
        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel viewModel)
        {
            try
            {
                User current = await _userManager.GetUserAsync(HttpContext.User);
                bool chckPass = await _userManager.CheckPasswordAsync(current, viewModel.CurrentPassword);
                if (chckPass == true)
                {
                    if (viewModel.ConfirmPassword == viewModel.NewPassword)
                    {
                        var change = await _userManager.ChangePasswordAsync(current, viewModel.CurrentPassword, viewModel.NewPassword);
                        if (change.Succeeded)
                        {
                            TempData["ChangeSuccess"] = "Başarılı bir şekilde şifrenizi değiştirdiniz yeni şifrenizle giriş yapabilirsiniz.";
                            await _signInManager.SignOutAsync();
                            return RedirectToAction("Login", "Account");
                        }
                        else
                        {
                            foreach (var item in change.Errors)
                            {
                                ModelState.AddModelError("", item.Description);
                            }
                            return View(viewModel);
                        }
                    }
                    else
                    {
                        return View(viewModel);
                    }
                }
                else
                {
                    return View(viewModel);
                }
            }
            catch (Exception)
            {
                return View(viewModel);
            }
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ResetPassword(string userId, string code)
        {
            try
            {
                if (string.IsNullOrEmpty(code))
                {
                    // Kod belirtilmemişse hata mesajı göster
                    TempData["Error4Reset"] = "Bir şeyler ters gitti tekrar dene.";
                }
                int loginCode = int.Parse(userId);
                var user = _userManager.Users.FirstOrDefault(x => x.StudentLoginCode == loginCode);

                if (user == null)
                {
                    TempData["Error4Reset"] = "Doğrulama kodu geçersiz veya süresi dolmuş.";
                }
                else
                {
                    int length = 10;
                    const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                    StringBuilder res = new StringBuilder();
                    Random rnd = new Random();
                    while (0 < length--)
                    {
                        res.Append(valid[rnd.Next(valid.Length)]);
                    }
                    res.Append("1Aa-");

                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _userManager.ResetPasswordAsync(user, token, res.ToString());
                    if (result.Succeeded)
                    {
                        TempData["TempPassword"] = "Şifren başarı ile değiştirildi !! Geçici şifreniz: " + res.ToString() + " ,en kısa zamanda profilinizden değiştirmeniz önerilir.";
                        return View();
                    }
                }
            }
            catch (Exception)
            {
                TempData["Error4Reset"] = "Doğrulama kodu geçersiz veya süresi dolmuş.";
                return View();
            }
            return View();
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.StudentLoginCode == model.loginCode && x.Number == model.number);

            if (user == null)
            {
                // Kullanıcı hakkında bilgi bulunamadı veya e-posta doğrulanmamışsa hata mesajı döndür
                return RedirectToAction("ForgotPassword", "Account");
            }
            else
            {
                // Kullanıcı için şifre sıfırlama tokeni oluştur ve e-posta adresine gönder
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.StudentLoginCode, code = code }, protocol: HttpContext.Request.Scheme);
                //await _emailSender.SendEmailAsync(model.Email, "Şifre Sıfırlama",

                _mailService.SendMailAsync(user.Email, "Şifre Sıfırlama Talebi", $"Lütfen şifrenizi sıfırlamak için <a href='{callbackUrl}'>buraya tıklayın</a>.");
                TempData["Waiting4Code"] = "Başarıyla mail adresine geçici şifre sıfırlama talebi gönderdik.";
                return RedirectToAction("ForgotPassword", "Account");
            }


            // ModelState geçerli değilse, formu tekrar göster
            return View(model);
        }
    }
}
