using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Web;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebApp1.Services;
using WebApp1.Data;

namespace WebApp1.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IHostingEnvironment _appEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AlertService _alertService { get; }

        public HomeController(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<PartyMember> userManager,
            AlertService alertService,
            IHostingEnvironment appEnvironment,
            IHttpContextAccessor httpContextAccessor)
            : base(context, authorizationService, userManager)
        {
            _appEnvironment = appEnvironment;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _alertService = alertService;
        }
        // GET: Home/Index
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        // GET: Home/About
        [AllowAnonymous]
        public IActionResult About()
        {
            return View();
        }
    }
}
