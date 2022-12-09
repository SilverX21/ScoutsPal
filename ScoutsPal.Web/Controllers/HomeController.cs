using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScoutsPal.Web.Models;
using Serilog;
using System.Diagnostics;

namespace ScoutsPal.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly Serilog.ILogger _serilogLogger;

        public HomeController()
        {
            _serilogLogger = Log.ForContext<HomeController>();
        }

        public IActionResult Index()
        {
            
           return View();
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

        [Authorize]
        public async Task<IActionResult> Login()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            //return RedirectToAction("Index", new { accessToken = accessToken });
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}