using System;
using System.IO;
using System.Web;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebApp1.Data;
using WebApp1.Services;

namespace WebApp1.Controllers
{
    public class FilesController : BaseController
    {
        private readonly IHostingEnvironment _appEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<PartyMember> _userManager;
        public AlertService alertService { get; }

        public FilesController(
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
            _userManager = userManager;
        }

        // GET: File/Index
        public IActionResult Index()
        {
            return View();
        }
        // POST: File/Index
        [HttpPost]
        public async Task<IActionResult> Index(List<IFormFile> files)
        {
            List<string> filePaths = new List<string>();
            if(files == null || files.Count == 0)
            {
                return new ChallengeResult();
            }
            string OwnerID = _userManager.GetUserId(User);
            string pathRoot = _appEnvironment.WebRootPath;
            foreach (IFormFile file in files)
            {
                // Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                string pathFile = pathRoot + "\\Files\\" + file.FileName;

                var stream = new FileStream(pathFile, FileMode.Create);
                try
                {
                    await file.CopyToAsync(stream);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if(stream != null) stream.Close();
                }

                filePaths.Add("\\Files\\" + file.FileName);
                // filePaths.Add(unixTimestamp.ToString());
            }
            ViewData["Paths"] = filePaths;
            return View();
        }
    }
}
