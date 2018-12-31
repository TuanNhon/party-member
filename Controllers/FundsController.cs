using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
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
using WebApp1.ViewModels;

namespace WebApp1.Controllers
{
    public class FundsController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<PartyMember> _userManager;
        public AlertService _alertService { get; }
        public FundsController(ApplicationDbContext context, IAuthorizationService authorizationService,
            UserManager<PartyMember> userManager, IHostingEnvironment hostingEnvironment, AlertService alertService)
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
            List<FundsViewModel> fundsViewModel = new List<FundsViewModel>();
            TotalFunds totalFunds = await _context.TotalFunds.LastOrDefaultAsync();
            var fundsList = await _context.Funds
                .Join(_context.PartyMember, f => f.TransactionUserID, u => u.Id, (f, u) => new { F = f, U = u.BirthName })
                .OrderByDescending(fu => fu.F.FundID).ToListAsync();
            ViewBag.TotalFunds = totalFunds.TotalFundsValue;
            foreach (var item in fundsList)
            {
                fundsViewModel.Add(new FundsViewModel(item.F, null, item.U));
            }
            return View(fundsViewModel);
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
        public async Task<IActionResult> Create(
            [Bind("_funds, _totalFunds")] FundsViewModel fundsViewModel)
        {
            var isAuthorized = User.IsInRole(Constants.MeetingManagersRole);

            if (!isAuthorized)
            {
                _alertService.Warning("Bạn không phải là Thủ Quỹ");
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                // logic
                Funds funds = fundsViewModel._funds;
                var OwnerID = (await _userManager.GetUserAsync(User)).Id;
                var totalFunds = (from f in _context.TotalFunds select f).LastOrDefault();
                funds.OwnerID = OwnerID;
                funds.TotalFund = totalFunds.TotalFundsValue;
                if (funds.isCollect)
                    funds.TotalFund += funds.Currency;
                else
                    funds.TotalFund -= funds.Currency;
                totalFunds.TotalFundsValue = funds.TotalFund;

                // save change
                _context.Add(funds);
                _context.Update(totalFunds);
                await _context.SaveChangesAsync();

                // return view
                return RedirectToAction(nameof(Index));
            }
            return View(fundsViewModel);
        }

        [HttpPost]
        public JsonResult CreateData(string Prefix = "")
        {
            var TransactionUser = (from u in _context.PartyMember.ToList()
                                   select new
                                   {
                                       ID = u.Id,
                                       Name = (u.BirthName ?? "Đảng viên") + " - " + u.Email
                                   }).Where(u => u.Name.ToLower().Contains((Prefix ?? "").ToLower()));
            return Json(TransactionUser);
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
            var totalFunds = (from tF in _context.TotalFunds select tF).Last();
            string userName = (from u in _context.PartyMember
                               where u.Id == funds.TransactionUserID
                               select u.BirthName).First();
            if (funds == null)
            {
                return NotFound();
            }
            ViewBag.OldTotalFunds = funds.isCollect
                ? totalFunds.TotalFundsValue - funds.Currency
                : totalFunds.TotalFundsValue + funds.Currency;
            return View(new FundsViewModel(funds, null, userName));
        }

        // POST: Funds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("_funds, _totalFunds")] FundsViewModel fundsViewModel, int oldTotalFunds = 0)
        {
            var isAuthorized = User.IsInRole(Constants.MeetingManagersRole);

            if (!isAuthorized)
            {
                _alertService.Warning("Bạn không phải là Thủ Quỹ");
                return RedirectToAction("Index");
            }
            if (id != fundsViewModel._funds.FundID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var totalFunds = (from tF in _context.TotalFunds select tF).Last();
                    fundsViewModel._funds.TotalFund = fundsViewModel._funds.isCollect
                        ? oldTotalFunds + fundsViewModel._funds.Currency
                        : oldTotalFunds - fundsViewModel._funds.Currency;

                    totalFunds.TotalFundsValue = fundsViewModel._funds.TotalFund;

                    _context.Update(totalFunds);
                    _context.Update(fundsViewModel._funds);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FundsExists(fundsViewModel._funds.FundID))
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
            ViewBag.OldTotalFunds = oldTotalFunds;
            return View(fundsViewModel);
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
            var totalFunds = await _context.TotalFunds.LastAsync();

            totalFunds.TotalFundsValue = funds.isCollect
                ? totalFunds.TotalFundsValue - funds.Currency
                : totalFunds.TotalFundsValue + funds.Currency;

            _context.Update(totalFunds);
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
