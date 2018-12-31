using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp1.Data;
using WebApp1.Models;

namespace WebApp1.Controllers
{
    public class PartyMemberInfoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PartyMemberInfoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PartyMemberInfo
        public async Task<IActionResult> Index()
        {
            return View(await _context.PartyMember.ToListAsync());
        }

        // GET: PartyMemberInfo/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partyMemberInfo = await _context.PartyMember
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partyMemberInfo == null)
            {
                return NotFound();
            }

            return View(partyMemberInfo);
        }

        //// GET: PartyMemberInfo/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: PartyMemberInfo/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,PhoneNumber,Email,BirthName,BirthDay,Gender,Ethnicity,Religion,Country,EduLevel,JobInfo,PartyDateInfo,PartyCardNum,MilitaryPoliceRetire,OutGoingInfo,InComingInfo,DiedInfo,LeaveInfo,Comment,Avatar")] PartyMemberInfo partyMemberInfo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(partyMemberInfo);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(partyMemberInfo);
        //}

        //// GET: PartyMemberInfo/Edit/5
        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var partyMemberInfo = await _context.PartyMember.FindAsync(id);
        //    if (partyMemberInfo == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(partyMemberInfo);
        //}

        //// POST: PartyMemberInfo/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, [Bind("Id,PhoneNumber,Email,BirthName,BirthDay,Gender,Ethnicity,Religion,Country,EduLevel,JobInfo,PartyDateInfo,PartyCardNum,MilitaryPoliceRetire,OutGoingInfo,InComingInfo,DiedInfo,LeaveInfo,Comment,Avatar")] PartyMemberInfo partyMemberInfo)
        //{
        //    if (id != partyMemberInfo.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(partyMemberInfo);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!PartyMemberInfoExists(partyMemberInfo.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(partyMemberInfo);
        //}

        //// GET: PartyMemberInfo/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var partyMemberInfo = await _context.PartyMember
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (partyMemberInfo == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(partyMemberInfo);
        //}

        //// POST: PartyMemberInfo/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    var partyMemberInfo = await _context.PartyMember.FindAsync(id);
        //    _context.PartyMember.Remove(partyMemberInfo);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool PartyMemberInfoExists(string id)
        //{
        //    return _context.PartyMember.Any(e => e.Id == id);
        //}
    }
}
