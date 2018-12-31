﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp1.Data;
using WebApp1.Models;
using WebApp1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApp1.Controllers
{
    public class FilesUploadController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<PartyMember> _userManager;
        private readonly IHostingEnvironment _appEnvironment;

        public FilesUploadController(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<PartyMember> userManager,
            IHostingEnvironment hostingEnvironment)
            : base(context, authorizationService, userManager)
        {
            _context = context;
            _authorizationService = authorizationService;
            _userManager = userManager;
            _appEnvironment = hostingEnvironment;
        }

        // GET: FilesUpload
        public async Task<IActionResult> Index()
        {
            var listFiles = new List<FileViewModel>();
            string topDomain = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}"; ;
            string[] filePreview = { "https://docs.google.com/gview?url=", "&embedded=true" };
            try
            {
                (await _context.FilesUpload.ToListAsync()).ForEach(async file =>
                {
                    string ownerName = (await _userManager.FindByIdAsync(file.OwnerID))?.BirthName ?? string.Empty;
                    string meetingTitle = (from m in _context.Meeting
                                           where m.MeetingID == file.MeetingID
                                           select m).First().MeetingTitle;
                    file.FilePath = $"{filePreview[0]}{topDomain + file.FilePath.Replace('\\', '/')}{filePreview[1]}";
                    listFiles.Add(new FileViewModel(file, ownerName, meetingTitle));
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(listFiles);
        }

        // GET: FilesUpload/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filesUpload = await _context.FilesUpload
                .FirstOrDefaultAsync(m => m.FileID == id);
            if (filesUpload == null)
            {
                return NotFound();
            }

            return View(filesUpload);
        }

        // GET: FilesUpload/Create?MeetingID
        public IActionResult Create(int? MeetingID)
        {
            if (MeetingID != null)
            {
                Meeting meeting = (from m in _context.Meeting
                                   where m.MeetingID == MeetingID
                                   select m).FirstOrDefault();
                ViewBag.meeting = meeting;
            }
            return View();
        }

        // POST: FilesUpload/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 115343360)]   //Giới hạn file upload
        [RequestSizeLimit(115343360)]                               //Giới hạn file upload
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("FileUpload,BirthName,MeetingTitle,Extension,FileNameWithoutExt,Files")] FileViewModel fileViewModel)
        {
            // Khởi tạo biến
            string pathRoot = _appEnvironment.WebRootPath; //Lấy đường dẫn thu mục gốc
            bool first = true;

            // Trả về View nếu Model không hợp lệ
            if (!ModelState.IsValid)
            {
                return View(fileViewModel);
            }

            fileViewModel.FileUpload.OwnerID = _userManager.GetUserId(User);
            foreach (IFormFile file in fileViewModel.Files)
            {
                // Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                // Đường dẫn lưu file kèm tên file
                Guid id = Guid.NewGuid();
                string ext = Path.GetExtension(file.FileName);
                string relativePath = "\\Files\\" + id.ToString() + ext;
                string absolutePath = pathRoot + relativePath;

                //Ghi file lên server
                var stream = new FileStream(absolutePath, FileMode.Create);
                try { await file.CopyToAsync(stream); }
                catch (Exception ex) { throw ex; }
                finally { if (stream != null) stream.Close(); }

                if (first)
                {
                    fileViewModel.FileUpload.FilePath = relativePath;
                    fileViewModel.FileUpload.FileName = file.FileName;
                    fileViewModel.FileUpload.UploadDate = DateTime.Now;
                    _context.Add(fileViewModel.FileUpload);
                    first = false;
                }
                else
                {
                    FilesUpload nItems = new FilesUpload();
                    nItems.MeetingID = fileViewModel.FileUpload.MeetingID;
                    nItems.OwnerID = fileViewModel.FileUpload.OwnerID;
                    nItems.FileName = file.FileName;
                    nItems.UploadDate = DateTime.Now;
                    nItems.Description = fileViewModel.FileUpload.Description;
                    nItems.FilePath = relativePath;
                    _context.Add(nItems);
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ActionName("CreateData")]
        public JsonResult CreateDataYo(string Prefix = "")
        {
            var MeetingTitle = (from m in _context.Meeting.ToList()
                                select new
                                {
                                    MeetingTitle = m.MeetingTitle + " - ngày "
                                    + m.MeetingDate?.ToString("dd/MM/yyyy"),
                                    ID = m.MeetingID
                                })
                                .Where(m => (m.MeetingTitle.ToLower()).Contains((Prefix ?? "").ToLower())).ToList();
            return Json(MeetingTitle);
        }

        // GET: FilesUpload/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            FileViewModel_Edit FileVM;
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                FilesUpload file = (await _context.FilesUpload.FindAsync(id));

                if (file == null)
                {
                    return NotFound();
                }

                string ownerName = (await _userManager.FindByIdAsync(file.OwnerID))?.BirthName ?? string.Empty;
                string meetingTitle = (from m in _context.Meeting
                                       where m.MeetingID == file.MeetingID
                                       select m).First().MeetingTitle;
                FileVM = new FileViewModel_Edit(file, ownerName, meetingTitle);
                FileVM.FileNameWithoutExt = Path.GetFileNameWithoutExtension(file.FileName);
                FileVM.Extension = Path.GetExtension(file.FileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(FileVM);
        }

        // POST: FilesUpload/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("")] FileViewModel_Edit fileViewModel)
        {
            if (id != fileViewModel.FileUpload.FileID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    fileViewModel.FileUpload.FileName = fileViewModel.FileNameWithoutExt + fileViewModel.Extension;
                    _context.Update(fileViewModel.FileUpload);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilesUploadExists(fileViewModel.FileUpload.FileID))
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
            return View(fileViewModel.FileUpload);
        }

        // GET: FilesUpload/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filesUpload = await _context.FilesUpload
                .FirstOrDefaultAsync(m => m.FileID == id);
            if (filesUpload == null)
            {
                return NotFound();
            }

            return View(filesUpload);
        }

        // POST: FilesUpload/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filesUpload = await _context.FilesUpload.FindAsync(id);
            _context.FilesUpload.Remove(filesUpload);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilesUploadExists(int id)
        {
            return _context.FilesUpload.Any(e => e.FileID == id);
        }
    }
}
