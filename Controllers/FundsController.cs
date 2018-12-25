using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp1.Authorization;
using WebApp1.Data;
using WebApp1.Models;
using WebApp1.Services;

namespace WebApp1.Controllers
{
    public class FundsController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<PartyMember> _userManager;
        public AlertService _alertService { get; }
        public FundsController(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<PartyMember> userManager,
            IHostingEnvironment hostingEnvironment,
            AlertService alertService)
            : base(context, authorizationService, userManager)
        {
            _context = context;
            _authorizationService = authorizationService;
            _userManager = userManager;
            _appEnvironment = hostingEnvironment;
            _alertService = alertService;
        }

        // GET: Funds
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Funds.ToListAsync());
        }

        // GET: Funds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funds = await _context.Funds
                .FirstOrDefaultAsync(m => m.FundID == id);
            if (funds == null)
            {
                return NotFound();
            }

            return View(funds);
        }

        // GET: Funds/Create
        public IActionResult Create()
        {
            var isAuthorized = User.IsInRole(Constants.MeetingManagersRole);

            if (!isAuthorized)
            {
                _alertService.Warning("Bạn không phải là Thủ Quỹ");
                return RedirectToAction("Index");
            }
            return View();
        }

        // POST: Funds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FundID,OwnerID,Currency,isCollect,TotalFund,UpatedDate,Description")] Funds funds)
        {
            var isAuthorized = User.IsInRole(Constants.MeetingManagersRole);

            if (!isAuthorized)
            {
                _alertService.Warning("Bạn không phải là Thủ Quỹ");
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var OwnerID = (await _userManager.GetUserAsync(User)).Id;
                funds.OwnerID = OwnerID;
                if (funds.isCollect)
                {
                    funds.TotalFund += funds.Currency;
                }
                else funds.TotalFund -= funds.Currency;
                _context.Add(funds);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(funds);
        }

        // GET: Funds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var isAuthorized = User.IsInRole(Constants.MeetingManagersRole);

            if (!isAuthorized)
            {
                _alertService.Warning("Bạn không phải là Thủ Quỹ");
                return RedirectToAction("Index");
            }
            if (id == null)
            {
                return NotFound();
            }

            var funds = await _context.Funds.FindAsync(id);
            if (funds == null)
            {
                return NotFound();
            }
            return View(funds);
        }

        // POST: Funds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FundID,OwnerID,Currency,isCollect,TotalFund,UpatedDate,Description")] Funds funds)
        {
            var isAuthorized = User.IsInRole(Constants.MeetingManagersRole);

            if (!isAuthorized)
            {
                _alertService.Warning("Bạn không phải là Thủ Quỹ");
                return RedirectToAction("Index");
            }
            if (id != funds.FundID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    List<Funds> oldFunds = (from f in _context.Funds
                                      where f.FundID == funds.FundID
                                      select f).ToList();
                    int oldTotalFunds = oldFunds[0].isCollect
                        ? oldFunds[0].TotalFund - oldFunds[0].Currency
                        : oldFunds[0].TotalFund + oldFunds[0].Currency;
                    funds.TotalFund = oldTotalFunds + funds.Currency;
                    _context.Update(funds);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FundsExists(funds.FundID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(funds);
        }

        // GET: Funds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var isAuthorized = User.IsInRole(Constants.MeetingManagersRole);

            if (!isAuthorized)
            {
                _alertService.Warning("Bạn không phải là Thủ Quỹ");
                return RedirectToAction("Index");
            }
            if (id == null)
            {
                return NotFound();
            }

            var funds = await _context.Funds
                .FirstOrDefaultAsync(m => m.FundID == id);
            if (funds == null)
            {
                return NotFound();
            }

            return View(funds);
        }

        // POST: Funds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isAuthorized = User.IsInRole(Constants.MeetingManagersRole);

            if (!isAuthorized)
            {
                _alertService.Warning("Bạn không phải là Thủ Quỹ");
                return RedirectToAction("Index");
            }
            var funds = await _context.Funds.FindAsync(id);
            _context.Funds.Remove(funds);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FundsExists(int id)
        {
            return _context.Funds.Any(e => e.FundID == id);
        }
    }
}
