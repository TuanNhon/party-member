using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp1.Utilities;
using System.Collections.Generic;

namespace WebApp1.ViewComponents
{
    public class AlertsViewComponent : ViewComponent
    {
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<IViewComponentResult> InvokeAsync()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            var alerts = TempData.ContainsKey(Alert.TempDataKey)
                ? TempData.Get<List<Alert>>(Alert.TempDataKey)
                : new List<Alert>();
            return View(alerts);
        }
    }
}
