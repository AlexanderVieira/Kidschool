using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Universal.EBI.MVC.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            await Task.Delay(100);
            return View();
        }
    }
}
