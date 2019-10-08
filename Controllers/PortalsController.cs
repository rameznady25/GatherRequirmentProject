using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gather_Requirement_Project.Models;

namespace Gather_Requirement_Project.Controllers
{
    public class PortalsController : Controller
    {
        private readonly Context _context;

        public PortalsController(Context context)
        {
            _context = context;
        }

        // GET: Portals
        public async Task<IActionResult> Index()
        {
            return View(await _context.Portal.Include(p=>p.MainPortal).ToListAsync());
        }

        // GET: Portals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portal = await _context.Portal.Include(p=>p.MainPortal)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (portal == null)
            {
                return NotFound();
            }

            return View(portal);
        }

        // GET: Portals/Create
        public IActionResult Create()
        {
            ViewBag.mainPortalsList = _context.MainPortal.ToList();
            return View();
        }

        // POST: Portals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Portal portal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(portal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(portal);
        }

        // GET: Portals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.mainPortalsList = _context.MainPortal.ToList();

            var portal = await _context.Portal.FindAsync(id);
            if (portal == null)
            {
                return NotFound();
            }
            return View(portal);
        }

        // POST: Portals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Portal portal)
        {
            if (id != portal.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(portal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortalExists(portal.ID))
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
            return View(portal);
        }

        // GET: Portals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portal = await _context.Portal.Include(p => p.MainPortal)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (portal == null)
            {
                return NotFound();
            }

            return View(portal);
        }

        // POST: Portals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var portal = await _context.Portal.FindAsync(id);
            _context.Portal.Remove(portal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PortalExists(int id)
        {
            return _context.Portal.Any(e => e.ID == id);
        }
    }
}
