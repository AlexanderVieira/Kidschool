using Microsoft.AspNetCore.Mvc;
using Universal.EBI.MVC.Services.Interfaces;

namespace NSE.WebApp.MVC.Extensions
{
    public class PaginacaoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IPagedList modelPaged)
        {            
            return View(modelPaged);
        }
    }
}