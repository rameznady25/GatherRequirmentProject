using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gather_Requirement_Project.Models;
using Microsoft.AspNetCore.Authorization;

namespace Gather_Requirement_Project.Controllers
{
    [Authorize]

    public class UserStoriesController : Controller
    {
        private readonly Context _context;
        public UserStoriesController(Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? screenId)
        {
            ViewBag.ScID = screenId;

            var context = _context.UserStories.Where(s => s.ScreenID == screenId).Include(s => s.Screen);

            return View(await context.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userStories = await _context.UserStories
                .Include(u => u.Screen)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userStories == null)
            {
                return NotFound();
            }

            return View(userStories);
        }

        public IActionResult Create(int screenid)
        {
            ViewBag.ScID = screenid;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserStories userStories)
        {
            userStories.Screen = _context.Screens.FirstOrDefault(s => s.ID == userStories.ScreenID);
            if (userStories.Screen != null)
            {
                try
                {
                    _context.UserStories.Add(userStories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserStoriesExists(userStories.ID))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index), new { screenId = userStories.ScreenID });
            }
            return View(userStories);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var userStories = await _context.UserStories.FirstOrDefaultAsync(i => i.ID == id);
            if (userStories == null)
                return NotFound();

            return View(userStories);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserStories userStories)
        {
            userStories.Screen = _context.Screens.FirstOrDefault(s => s.ID == userStories.ScreenID);
            if (userStories.Screen != null)
            {
                try
                {
                    _context.Update(userStories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserStoriesExists(userStories.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { screenId = userStories.ScreenID });
            }
            return View(userStories);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userStories = await _context.UserStories
                .Include(u => u.Screen)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userStories == null)
            {
                return NotFound();
            }

            return View(userStories);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userStories = await _context.UserStories.FindAsync(id);
            _context.UserStories.Remove(userStories);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { screenId = userStories.ScreenID });
        }

        private bool UserStoriesExists(int id)
        {
            return _context.UserStories.Any(e => e.ID == id);
        }
    }
}
