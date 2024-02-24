using EntityLayer.Concreate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TatliSozluk.MvcCoreWebUI.ViewComponents.Main
{
    public class _PopularContent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
