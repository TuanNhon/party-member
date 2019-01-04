using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebApp1.Data;
using WebApp1.Models;
using WebApp1.ViewModels;
using WebApp1.Authorization;
using WebApp1.Services;

namespace WebApp1.Controllers
{
    public class MeetingController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<PartyMember> _userManager;
        private readonly AlertService _alertService;

        public MeetingController(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<PartyMember> userManager,
            AlertService alertService)
            :base(context, authorizationService, userManager)
        {
            _context = context;
            _authorizationService = authorizationService;
            _userManager = userManager;
            _alertService = alertService;
        }

        // GET: Meeting
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Meeting.ToListAsync());
        }

        // GET: Meeting/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //Khởi tạo
            MeetingViewModel MeetingVM;
            List<FilesUpload> filesVM = new List<FilesUpload>();

            if (id == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meeting
                .FirstOrDefaultAsync(m => m.MeetingID == id);

            if (meeting == null)
            {
                return NotFound();
            }

            string topDomain = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}"; ;
            string[] filePreview = { "https://docs.google.com/gview?url=", "&embedded=true" };
            string ownerName = (await _userManager.FindByIdAsync(meeting.OwnerID))?.BirthName;
            filesVM = (from f in _context.FilesUpload
                       where f.MeetingID == meeting.MeetingID
                       select f)?.ToList();
            filesVM.ForEach(delegate (FilesUpload file) {
                file.FilePath = $"{filePreview[0]}{topDomain + file.FilePath.Replace('\\', '/')}{filePreview[1]}";
            });

            MeetingVM = new MeetingViewModel(meeting, filesVM, ownerName);

            return View(MeetingVM);
        }

        // GET: Meeting/Create
        public IActionResult Create()
        {
            var isAuthorized = User.IsInRole(Constants.MeetingAdministratorsRole);

            if (!isAuthorized)
            {
                _alertService.Warning("Bạn không đủ quyền");
                return RedirectToAction("Index");
            }
            return View();
        }

        // POST: Meeting/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MeetingID,OwnerID,MeetingTitle,MeetingDate,Summary")] Meeting meeting)
        {
            var isAuthorized = User.IsInRole(Constants.MeetingAdministratorsRole);

            if (!isAuthorized)
            {
                _alertService.Warning("Bạn không đủ quyền");
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                return View(meeting);
            }
            meeting.OwnerID = _userManager.GetUserId(User);
            _context.Add(meeting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Meeting/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var isAuthorized = User.IsInRole(Constants.MeetingAdministratorsRole);

            if (!isAuthorized)
            {
                _alertService.Warning("Bạn không đủ quyền");
                return RedirectToAction("Index");
            }

            if (id == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meeting.FindAsync(id);
            if (meeting == null)
            {
                return NotFound();
            }
            return View(meeting);
        }

        // POST: Meeting/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MeetingID,MeetingTitle,MeetingDate,Summary")] Meeting meeting)
        {
            var isAuthorized = User.IsInRole(Constants.MeetingAdministratorsRole);

            if (!isAuthorized)
            {
                _alertService.Warning("Bạn không đủ quyền");
                return RedirectToAction("Index");
            }

            if (id != meeting.MeetingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meeting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeetingExists(meeting.MeetingID))
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
            return View(meeting);
        }

        // GET: Meeting/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var isAuthorized = User.IsInRole(Constants.MeetingAdministratorsRole);

            if (!isAuthorized)
            {
                _alertService.Warning("Bạn không đủ quyền");
                return RedirectToAction("Index");
            }

            if (id == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meeting
                .FirstOrDefaultAsync(m => m.MeetingID == id);
            if (meeting == null)
            {
                return NotFound();
            }

            return View(meeting);
        }

        // POST: Meeting/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isAuthorized = User.IsInRole(Constants.MeetingAdministratorsRole);

            if (!isAuthorized)
            {
                _alertService.Warning("Bạn không đủ quyền");
                return RedirectToAction("Index");
            }

            var meeting = await _context.Meeting.FindAsync(id);
            _context.Meeting.Remove(meeting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeetingExists(int id)
        {
            return _context.Meeting.Any(e => e.MeetingID == id);
        }
    }
}
