using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Web;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using WebApp1.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebApp1.Services;

namespace WebApp1.Controllers
{
    public abstract class BaseController : Controller
    {
        protected ApplicationDbContext Context { get; }
        protected IAuthorizationService AuthorizationService { get; }
        protected UserManager<PartyMember> UserManager { get; }
        public BaseController(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<PartyMember> userManager) : base()
        {
        }

    }
}
