using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concreate;
using Microsoft.AspNetCore.Authorization;
using TatliSozluk.MvcCoreWebUI.Models;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.AspNetCore.Identity;
using System.Data;
using Microsoft.CodeAnalysis;
using HakinSozluk.MvcCoreWebUI.Models;
using Microsoft.AspNetCore.Hosting;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Claims;
using ExcelDataReader;

namespace TatliSozluk.MvcCoreWebUI.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller
	{
		HeadingService _headingService = new HeadingService(new EfHeadingDal());
		UserService _userServiceManuel = new UserService(new EfUserDal());
		CategoryService _categoryService = new CategoryService(new EfCategoryDal());
		EntryService _entryService = new EntryService(new EfEntryDal());
		StudentInformationService _studentInfoService = new StudentInformationService(new EfStudentInformationDal());
		MessageService _messageService = new MessageService(new EfMessageDal());
		UpVoteService _upVoteService = new UpVoteService(new EfUpVoteDal());
		BadWordService _badWordService = new BadWordService(new EfBadWordDal());
		private readonly UserManager<User> _userManager;
		public AdminController(UserManager<User> userManager)
		{
			_userManager = userManager;
		}
		public IActionResult Index()
		{
			AdminDashboardViewModel viewModel = new AdminDashboardViewModel();
			viewModel.HeadingsToday = _headingService.TGetAll(x => x.CreatedDate.Date == DateTime.Today && x.IsApporoved == true).Count;
			viewModel.AllEntries = _entryService.TGetAll(x => x.CreatedDate.Date == DateTime.Today && x.Heading.IsApporoved == true).Count;
			viewModel.MessagesToday = _messageService.TGetAll(x => x.CreatedAt.Date == DateTime.Today).Count;
			viewModel.AllHeadings = _headingService.TGetAll(x => x.IsApporoved == true).Count;
			viewModel.AllEntries = _entryService.TGetAll(x => x.Heading.IsApporoved == true).Count;
			viewModel.AllMessages = _messageService.TGetAll().Count;
			viewModel.Categories = _categoryService.TGetAll().Count;
			viewModel.WaitingForApporove = _headingService.TGetAll(x => x.IsApporoved == false && x.BeenOpenedBefore == false).Count;
			viewModel.AllHeadingsWithoutApporove = _headingService.TGetAll().Count;
			viewModel.BadWords = _badWordService.TGetAll().Count;

			return View(viewModel);
		}
		public IActionResult PendingHeadingApprovals()
		{
			return View(_headingService.TGetAll(x => x.IsApporoved == false && x.BeenOpenedBefore == false, x => x.FromWho, x => x.Category));
		}
		public async Task<IActionResult> EnableHeading(int id)
		{
			bool isApporovedBefore = false;
			var heading = _headingService.TFind(x => x.Id == id);
			if (heading.BeenOpenedBefore == true)
			{
				isApporovedBefore = true;
			}

			heading.IsApporoved = true;
			heading.BeenOpenedBefore = true;
			_headingService.TUpdate(heading);
			TempData["SuccessfullyEditedHeading"] = "Başarılı bir şekilde düzenlendi.";

			if (isApporovedBefore == true)
			{
				return RedirectToAction("deactivatedheadings", "admin");
			}
			else
			{
				return RedirectToAction("pendingheadingapprovals", "admin");
			}
		}
		public async Task<IActionResult> DeleteHeading(int id)
		{
			var heading = _headingService.TFind(x => x.Id == id);
			bool isApporovedBefore = false;

			if (heading.BeenOpenedBefore == true)
			{
				isApporovedBefore = true;
				var headingEntries = _entryService.TGetAll(x => x.HeadingId == heading.Id);
				foreach (var entry in headingEntries)
				{
					_upVoteService.DeleteUpVotesByEntryId(entry.Id);
				}
			}
			_headingService.TDelete(heading);
			TempData["SuccessfullyEditedHeading"] = "Başlık başarılı bir şekilde silindi.";
			if (isApporovedBefore == true)
			{
				return RedirectToAction("deactivatedheadings", "admin");
			}
			else
			{
				return RedirectToAction("pendingheadingapprovals", "admin");
			}
		}
		[HttpGet]
		public IActionResult EditCategory()
		{
			EditCategoryViewModel viewModel = new EditCategoryViewModel();
			viewModel.Categories = _categoryService.TGetAll();
			return View(viewModel);
		}
		[HttpPost]
		public IActionResult EditCategory(EditCategoryViewModel c)
		{
			c.Category.CategoryStatus = true;
			_categoryService.TInsert(c.Category);
			TempData["SuccessfullyAddedCategory"] = "Başarılı şekilde eklendi.";
			c.Category = null;
			c.Categories = _categoryService.TGetAll();
			return View(c);
		}
		public async Task<IActionResult> DisableCategory(int id)
		{
			var checkIt = _categoryService.TFind(x => x.Id == id);
			if (checkIt.CategoryStatus == false || checkIt == null)
			{
				return RedirectToAction("editcategory", "admin");
			}
			else
			{
				checkIt.CategoryStatus = false;
				_categoryService.TUpdate(checkIt);
				TempData["SuccessfullyDisabledCategory"] = "Başarılı şekilde devre dışı bırakıldı.";
				return RedirectToAction("editcategory", "admin");
			}
		}
		public async Task<IActionResult> DisableEntry(int id)
		{
			var findEntry = _entryService.TFind(x => x.Id == id);
			if (findEntry == null)
			{
				return RedirectToAction("index", "home");
			}
			else
			{
				findEntry.Status = false;
				_entryService.TUpdate(findEntry);
				TempData["SuccessfullyDisabledEntry"] = "Başarılı şekilde devre dışı bırakıldı.";
				return RedirectToAction("deactivatedentries", "admin");
			}
		}
		public IActionResult DeactivatedEntries()
		{
			EditEntryViewModel editEntryViewModel = new EditEntryViewModel();
			editEntryViewModel.Entries = _entryService.TGetAll(x => x.Status == false, x => x.User, x => x.Heading);
			return View(editEntryViewModel);
		}
		public async Task<IActionResult> EnableCategory(int id)
		{
			var checkIt = _categoryService.TFind(x => x.Id == id);
			if (checkIt.CategoryStatus == true || checkIt == null)
			{
				return RedirectToAction("editcategory", "admin");
			}
			else
			{
				checkIt.CategoryStatus = true;
				_categoryService.TUpdate(checkIt);
				TempData["SuccessfullyEnabledCategory"] = "Başarılı şekilde aktifleştirildi.";
				return RedirectToAction("editcategory", "admin");
			}
		}
		public async Task<IActionResult> DeleteCategory(int id)
		{
			var checkIt = _categoryService.TFind(x => x.Id == id);
			if (checkIt == null)
			{
				return RedirectToAction("editcategory", "admin");
			}
			else
			{
				_categoryService.TDelete(checkIt);
				TempData["SuccessfullyDeletedCategory"] = "Başarılı şekilde silindi";
				return RedirectToAction("editcategory", "admin");
			}
		}
		[HttpGet]
		public IActionResult EditBadWords()
		{
			BadWordViewModel viewModel = new BadWordViewModel();
			viewModel.BadWords = _badWordService.TGetAll();
			return View(viewModel);
		}
		[HttpPost]
		public IActionResult EditBadWords(BadWordViewModel userValues)
		{
			try
			{
				var checkIt = _badWordService.TFind(x => x.Word == userValues.BadWord.Word);
				if (checkIt == null)
				{
					_badWordService.TInsert(userValues.BadWord);
					TempData["SuccessfullyAddedBadWord"] = "Başarılı şekilde eklendi.";
					userValues.BadWord = null;
					userValues.BadWords = _badWordService.TGetAll();
					return View(userValues);
				}
				else
				{
					TempData["WentWrongForBadWord"] = "Bir hata oluştu";
					return RedirectToAction("editbadwords", "admin");
				}
			}
			catch (Exception)
			{
				TempData["WentWrongForBadWord"] = "Bir hata oluştu";
				return RedirectToAction("editbadwords", "admin");
			}
		}
		public IActionResult DeleteBadWord(int id)
		{
			var checkIt = _badWordService.TFind(x => x.Id == id);
			if (checkIt == null)
			{
				return RedirectToAction("editbadwords", "admin");
			}
			else
			{
				_badWordService.TDelete(checkIt);
				TempData["SuccessfullyDeletedBadWord"] = "Başarılı şekilde silindi";
				return RedirectToAction("editbadwords", "admin");
			}
		}
		public IActionResult DisableHeading(int id)
		{
			var heading = _headingService.TFind(x => x.Id == id);
			if (heading.IsApporoved == true && heading.BeenOpenedBefore == true)
			{
				heading.IsApporoved = false;
				_headingService.TUpdate(heading);
			}
			return RedirectToAction("deactivatedheadings", "admin");
		}
		public IActionResult DeleteEntry(int id)
		{
			try
			{
				var entry = _entryService.TFind(x => x.Id == id);

				_upVoteService.DeleteUpVotesByEntryId(entry.Id);

				_entryService.TDelete(entry);
				TempData["SuccessfullyDeletedEntry"] = "Başarılı şekilde silindi.";
				return RedirectToAction("deactivatedentries", "admin");
			}
			catch (Exception)
			{

				return RedirectToAction("Index", "admin");
			}

		}
		public async Task<IActionResult> EnableEntry(int id)
		{
			var checkIt = _entryService.TFind(x => x.Id == id);
			if (checkIt.Status == true || checkIt == null)
			{
				return RedirectToAction("deactivatedentries", "admin");
			}
			else
			{
				checkIt.Status = true;
				_entryService.TUpdate(checkIt);
				TempData["SuccessfullyEnabledEntry"] = "Başarılı bir şekilde aktifleştirildi.";
				return RedirectToAction("deactivatedentries", "admin");
			}
		}
		public IActionResult DeactivatedHeadings()
		{
			return View(_headingService.TGetAll(x => x.IsApporoved == false && x.BeenOpenedBefore == true, x => x.FromWho, x => x.Category));
		}
		[HttpGet]
		public async Task<IActionResult> AssignAdmin()
		{
			AssignAdminViewModel model = new AssignAdminViewModel();
			model.Users = await _userManager.GetUsersInRoleAsync("Admin");
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> AssignAdmin(AssignAdminViewModel oun)
		{
			if (oun.UserName == oun.UsernameAgain && oun.UserName != null && oun.UserName != "")
			{
				try
				{
					var user = await _userManager.FindByNameAsync(oun.UserName);

					if (user != null)
					{
						var addRole = await _userManager.AddToRoleAsync(user, "Admin");
						TempData["SuccessfullyAddedAdmin"] = "Başarıyla yeni admin atandı.";
						return RedirectToAction("AssignAdmin", "Admin");
					}
				}
				catch (Exception)
				{
					TempData["WeDontHave"] = "Böyle bir kullanıcı yok.";
					return RedirectToAction("AssignAdmin", "Admin");
				}

			}
			else
			{
				TempData["WeDontHave"] = "Böyle bir kullanıcı yok.";
				return RedirectToAction("AssignAdmin", "Admin");
			}
			return View();
		}
		public async Task<IActionResult> RemoveAdmin(AssignAdminViewModel oun)
		{
			var findUser = await _userManager.FindByNameAsync(oun.UserName);
			if (findUser != null)
			{
				// Admin rolünü kullanıcıdan kaldır
				var result = await _userManager.RemoveFromRoleAsync(findUser, "Admin");

				// İşlem başarılıysa TempData kullanarak bir mesaj gösterebilirsiniz
				if (result.Succeeded)
				{
					TempData["SuccessfullyRemovedAdmin"] = $"Admin rolü başarıyla kaldırıldı: {oun.UserName}";
				}
				else
				{
					// İşlem başarısızsa hata mesajını gösterebilirsiniz
					TempData["FailedToRemoveAdmin"] = $"Admin rolü kaldırılırken bir hata oluştu: {oun.UserName}";
				}
			}
			else
			{
				// Kullanıcı bulunamazsa hata mesajını gösterebilirsiniz
				TempData["UserNotFound"] = $"Kullanıcı bulunamadı: {oun.UserName}";
			}

			// AssignAdmin action'ına yönlendir
			return RedirectToAction("assignadmin", "admin");
		}
		[HttpGet]
		public IActionResult GetChats(GetChatsViewModel getChatsViewModel)
		{
			getChatsViewModel.Users = _userManager.Users;
			return View(getChatsViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> GetChats(string clickedUsername)
		{
			GetChatsViewModel getChatsViewModel = new GetChatsViewModel();
			getChatsViewModel.Users = _userManager.Users;

			User getUser = await _userManager.FindByNameAsync(clickedUsername);
			var userId = await _userManager.GetUserIdAsync(getUser);

			var messages = _messageService.TGetAll(x => x.SenderId == userId || x.ReceiverId == userId, x => x.Sender, x => x.Receiver);

			getChatsViewModel.Messages = messages;

			return View(getChatsViewModel);
		}
		[HttpGet]
		public IActionResult GetStudentsInformations()
		{
			StudentInfoViewModel studentInfoViewModel = new StudentInfoViewModel();
			studentInfoViewModel.StudentInformations = _studentInfoService.TGetAll();
			return View(studentInfoViewModel);
		}

		[HttpGet]
		public IActionResult GetStudentsAccount()
		{
			var users=_userServiceManuel.TGetAll(null,x=>x.StudentLogin);

			return View(users);
		}

		public async Task<IActionResult> DeleteAccount(string username)
		{
			try
			{
				var findUser = _userManager.Users.FirstOrDefault(x => x.UserName == username);
				string userId = await _userManager.GetUserIdAsync(findUser);

				var findUpVotes = _upVoteService.TGetAll(x => x.UserId == userId);
				foreach (var upvote in findUpVotes)
				{
					_upVoteService.TDelete(upvote);
				}

				var findEntries=_entryService.TGetAll(x=>x.UserId==userId);
				foreach (var entry in findEntries)
				{
                    _upVoteService.DeleteUpVotesByEntryId(entry.Id);
					_entryService.TDelete(entry);
				}

				var findMessages=_messageService.TGetAll(x=>x.SenderId==userId||x.ReceiverId==userId); 
				foreach (var message in findMessages)
				{
					_messageService.TDelete(message);
				}

				var findHeadings = _headingService.TGetAll(x => x.FromWhoId == userId);
				foreach (var heading in findHeadings)
				{
				
					var findHeadingEntry= _entryService.TGetAll(x => x.HeadingId == heading.Id);
					foreach (var entry in findHeadingEntry)
					{
						_upVoteService.DeleteUpVotesByEntryId(entry.Id);
						_entryService.TDelete(entry);
					}
					_headingService.TDelete(heading);
				}


				var deleteIt=await _userManager.DeleteAsync(findUser);

				return RedirectToAction("getstudentsaccount","admin");
			}
			catch (Exception)
			{
				return RedirectToAction("getstudentsaccount", "admin");
			}
		}

		public async Task<IActionResult> UpdateClass(int id)
		{
			try
			{
				var allStudent = _studentInfoService.TGetAll();
				foreach (var student in allStudent)
				{
					if (id == 1)
					{
						if (student.Class.StartsWith("9"))
						{
							student.Class = "10" + student.Class.Substring(1);
						}
						else if (student.Class.StartsWith("10"))
						{
							student.Class = "11" + student.Class.Substring(2);
						}
						else if (student.Class.StartsWith("11"))
						{
							student.Class = "12" + student.Class.Substring(2);
						}
						else if (student.Class.StartsWith("12"))
						{
							student.Class = "MEZUN";
							student.Number = null;
						}
						else if (student.Class == "MEZUN")
						{
							continue;
						}
						_studentInfoService.TUpdate(student);
					}
					else if (id == 0)
					{
						if (student.Class.StartsWith("10"))
						{
							student.Class = "9" + student.Class.Substring(2);
						}
						else if (student.Class.StartsWith("11"))
						{
							student.Class = "10" + student.Class.Substring(2);
						}
						else if (student.Class.StartsWith("12"))
						{
							student.Class = "11" + student.Class.Substring(2);
						}
						else if (student.Class == "MEZUN")
						{
							continue;
						}
						_studentInfoService.TUpdate(student);
					}
				}
			}
			catch (Exception)
			{

				return RedirectToAction("GetStudentsInformations", "Admin");

			}
			return RedirectToAction("GetStudentsInformations", "Admin");
		}
		[HttpPost]
		public async Task<IActionResult> UploadClass(IFormFile excelFile, [FromServices] IWebHostEnvironment hostingEnvironment)
		{
			try
			{
				string fileName = Path.Combine(hostingEnvironment.WebRootPath, "files", excelFile.FileName);

				using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
				{
					await excelFile.CopyToAsync(fileStream);

					fileStream.Flush();

				}

				List<StudentInformation> students = GetStudentInformations(fileName);
				if (students != null)
				{
					foreach (var student in students)
					{
						try
						{
							_studentInfoService.TInsert(student);
						}
						catch (Exception)
						{
							continue;
						}
					}
				}

				return RedirectToAction("GetStudentsInformations", "Admin");
			}
			catch (Exception)
			{
				return RedirectToAction("GetStudentsInformations", "Admin");
			}
		}
		[HttpGet]
		public IActionResult UpdateStudent(int id)
		{
			try
			{
				var user = _userManager.Users.FirstOrDefault(x => x.StudentLoginCode == id);
				var info = _studentInfoService.TFind(x => x.LoginCode == id, x => x.User);
				UpdateStudentViewModel updateStudentViewModel = new UpdateStudentViewModel();

				if (user == null)
				{
					updateStudentViewModel.isAccountActive = false;

				}
				else
				{
					updateStudentViewModel.isAccountActive = true;
				}
				updateStudentViewModel.User = user;
				updateStudentViewModel.StudentInformation = info;
				return View(updateStudentViewModel);
			}
			catch (Exception)
			{
				return RedirectToAction("getstudentsinformations", "admin");
			}
			
		}
		[HttpPost]
		public async Task<IActionResult> UpdateStudent(UpdateStudentViewModel model)
		{
			try
			{

				var info = _studentInfoService.TFind(x => x.LoginCode == model.StudentInformation.LoginCode);
				info.NameSurname = model.StudentInformation.NameSurname;
				info.Number = model.StudentInformation.Number;
				info.Class = model.StudentInformation.Class;

				_studentInfoService.TUpdate(info);


				var findUsr = _userManager.Users.FirstOrDefault(x => x.StudentLoginCode == model.StudentInformation.LoginCode);
				if (findUsr!=null)
				{
					string token = await _userManager.GenerateChangeEmailTokenAsync(findUsr, model.User.Email);
					await _userManager.ChangeEmailAsync(findUsr, model.User.Email, token);
					findUsr.Email = model.User.Email;
					findUsr.UserName = model.User.UserName;
					findUsr.Number = model.StudentInformation.Number;

					var update = await _userManager.UpdateAsync(findUsr);

				}


				TempData["success4update"] = "Başarılı bir şekilde güncellendi !";
				return View(model);
			}
			catch (Exception)
			{
				TempData["fail4update"] = "Bir şeyler ters gitti.";
				return View(model);
			}
		
		}
		public List<StudentInformation> GetStudentInformations(string fName)
		{
			try
			{
				List<StudentInformation> studentInformations = new List<StudentInformation>();
				var fileName = fName;
				System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
				using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
				{
					using (var reader = ExcelReaderFactory.CreateReader(stream))
					{
						while (reader.Read())
						{
							try
							{
								studentInformations.Add(new StudentInformation()
								{
									NameSurname = reader.GetValue(0).ToString(),
									Class = reader.GetValue(1).ToString(),
									Number = reader.GetValue(2).ToString(),
								});
							}
							catch (Exception)
							{
								break;
							}
						}
					}

					return studentInformations;
				}
			}
			catch (Exception)
			{

				return null;
			}
		}

		public IActionResult DeleteStudent(int id)
		{
			try
			{
				var find = _studentInfoService.TFind(x=>x.LoginCode==id);
				try
				{
					var findUser = _userManager.Users.FirstOrDefault(x => x.StudentLoginCode == id);
					_userManager.DeleteAsync(findUser);
				}
				catch (Exception)
				{

					throw;
				}
				_studentInfoService.TDelete(find);
			}
			catch (Exception)
			{
				return RedirectToAction("getstudentsinformations", "admin");
			}
			return RedirectToAction("getstudentsinformations", "admin");
		}
	}
}

