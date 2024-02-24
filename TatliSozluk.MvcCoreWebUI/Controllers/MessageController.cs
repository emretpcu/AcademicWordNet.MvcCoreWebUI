using BusinessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concreate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using TatliSozluk.MvcCoreWebUI.Models;

namespace TatliSozluk.MvcCoreWebUI.Controllers
{
	[Authorize]
	public class MessageController : Controller
	{
		private readonly UserManager<User> _userManager;
		MessageService _messageService = new MessageService(new EfMessageDal());
		public MessageController(UserManager<User> userManager)
		{
			_userManager = userManager;
		}
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			MessageViewModel messageViewModel = new MessageViewModel();
			var userId = _userManager.GetUserId(HttpContext.User);
			messageViewModel.Messages = _messageService.TGetAll(x => x.ReceiverId == userId, x => x.Sender, x => x.Receiver).OrderByDescending(x => x.CreatedAt).ToList();
			messageViewModel.IsSendBox = false;
			return View(messageViewModel);
		}
		[HttpPost]
		public async Task<IActionResult> Index(MessageViewModel messageViewModel)
		{
			try
			{
				var userId = _userManager.GetUserId(HttpContext.User);
				var receiverId="";
				if (messageViewModel.IsReply!=true)
				{
					var receiver = await _userManager.FindByNameAsync(messageViewModel.MessageForReply.ReceiverId);
					receiverId = await _userManager.GetUserIdAsync(receiver);

					if (receiverId==userId)
					{
						return RedirectToAction("index");
					}
				}
				else
				{
					if (messageViewModel.IsGetMessageReply==true)
					{
						receiverId = messageViewModel.Message.ReceiverId;
					}
					else
					{
						receiverId = messageViewModel.Message.SenderId;
					}
				}

				var senderId = _userManager.GetUserId(HttpContext.User);
				DateTime date = DateTime.Now;
				messageViewModel.MessageForReply.CreatedAt = date;
				messageViewModel.MessageForReply.IsRead = false;
				if (messageViewModel.IsReply != true)
				{
					messageViewModel.MessageForReply.ReceiverId = receiverId.ToString();
				}
				else
				{
					messageViewModel.MessageForReply.ReceiverId = receiverId;
				}
				messageViewModel.MessageForReply.SenderId = senderId;
				_messageService.TInsert(messageViewModel.MessageForReply);

				var messageId = _messageService.TFind(x => x.CreatedAt == date && x.ReceiverId == receiverId && x.SenderId == senderId).Id;
				return RedirectToAction("getmessage", "message", new
				{
					id = messageId
				});
			}
			catch (Exception)
			{
				return RedirectToAction("index");
			}
		}
		public async Task<IActionResult> GetMessage(int id)
		{
			MessageViewModel messageViewModel = new MessageViewModel();
			var userId = _userManager.GetUserId(HttpContext.User);
			messageViewModel.Message = _messageService.TFind(x => x.SenderId == userId && x.Id == id, x => x.Sender, x => x.Receiver);
			messageViewModel.IsGetMessageReply = true;
			return View(messageViewModel);
		}
		public async Task<IActionResult> Sent()
		{
			MessageViewModel messageViewModel = new MessageViewModel();
			var userId = _userManager.GetUserId(HttpContext.User);
			messageViewModel.Messages = _messageService.TGetAll(x => x.SenderId == userId, x => x.Sender, x => x.Receiver).OrderByDescending(x => x.CreatedAt).ToList();
			messageViewModel.IsSendBox=true;
			return View(messageViewModel);
		}
        public async Task<IActionResult> Read(int id)
        {
			MessageViewModel messageViewModel = new MessageViewModel();
			var userId = _userManager.GetUserId(HttpContext.User);
			messageViewModel.Message = _messageService.TFind(x => x.ReceiverId == userId && x.Id == id,x=>x.Sender,x=>x.Receiver);
			messageViewModel.IsGetMessageReply=false;
			return View(messageViewModel);
        }

	}
}

