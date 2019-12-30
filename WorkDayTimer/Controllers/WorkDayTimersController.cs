using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkDayTimerWebApp.Data;
using WorkDayTimerWebApp.Models;

namespace WorkDayTimerWebApp.Controllers
{
    public class WorkDayTimersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkDayTimersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WorkDayTimers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.WorkDayTimers.Include(w => w.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: WorkDayTimers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workDayTimer = await _context.WorkDayTimers
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workDayTimer == null)
            {
                return NotFound();
            }

            return View(workDayTimer);
        }

        // GET: WorkDayTimers/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: WorkDayTimers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Name,Date,WorkDayHours,TimerTime,IsRunning,MaxTimerCount")] WorkDayTimer workDayTimer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workDayTimer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", workDayTimer.UserId);
            return View(workDayTimer);
        }

        // GET: WorkDayTimers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workDayTimer = await _context.WorkDayTimers.FindAsync(id);
            if (workDayTimer == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", workDayTimer.UserId);
            return View(workDayTimer);
        }

        // POST: WorkDayTimers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Name,Date,WorkDayHours,TimerTime,IsRunning,MaxTimerCount")] WorkDayTimer workDayTimer)
        {
            if (id != workDayTimer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workDayTimer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkDayTimerExists(workDayTimer.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", workDayTimer.UserId);
            return View(workDayTimer);
        }

        // GET: WorkDayTimers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workDayTimer = await _context.WorkDayTimers
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workDayTimer == null)
            {
                return NotFound();
            }

            return View(workDayTimer);
        }

        // POST: WorkDayTimers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workDayTimer = await _context.WorkDayTimers.FindAsync(id);
            _context.WorkDayTimers.Remove(workDayTimer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkDayTimerExists(int id)
        {
            return _context.WorkDayTimers.Any(e => e.Id == id);
        }
    }
}
