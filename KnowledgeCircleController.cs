using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeineApp.Data;

namespace MeineApp.Controllers
{
    public class KnowledgeCircleController : Controller
    {
        private readonly KcDbContext _context;

        public KnowledgeCircleController(KcDbContext context)
        {
            _context = context;
        }

        // GET: KnowledgeCircle
        public async Task<IActionResult> Index()
        {
            var kcs = _context.KnowledgeCircles
                .Include(kc => kc.Speaker)
                .OrderBy(kc => kc.Datum)
                .AsNoTracking();
            
            return View(await kcs.ToListAsync());
        }

        // GET: KnowledgeCircle/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var knowledgeCircle = await _context.KnowledgeCircles
                .Include(kc => kc.Speaker)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (knowledgeCircle == null)
            {
                return NotFound();
            }

            return View(knowledgeCircle);
        }

        // GET: KnowledgeCircle/Create
        public IActionResult Create()
        {
            PopulateSpeakerDropdown();
            return View();
        }

        // POST: KnowledgeCircle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SpeakerId,Thema,Datum")] KnowledgeCircle knowledgeCircle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(knowledgeCircle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateSpeakerDropdown(knowledgeCircle.SpeakerId);
            return View(knowledgeCircle);
        }

        // GET: KnowledgeCircle/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var knowledgeCircle = await _context.KnowledgeCircles
                .AsNoTracking()
                .SingleOrDefaultAsync(kc => kc.ID == id);
            if (knowledgeCircle == null)
            {
                return NotFound();
            }

            PopulateSpeakerDropdown(knowledgeCircle.SpeakerId);
            return View(knowledgeCircle);
        }

        // POST: KnowledgeCircle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SpeakerId,Thema,Datum")] KnowledgeCircle knowledgeCircle)
        {
            if (id != knowledgeCircle.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(knowledgeCircle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KnowledgeCircleExists(knowledgeCircle.ID))
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

            PopulateSpeakerDropdown(knowledgeCircle.SpeakerId);
            return View(knowledgeCircle);
        }

        private void PopulateSpeakerDropdown(object selectedSpeaker = null)
        {
            var speakerQuery = from s in _context.Speakers
                               orderby s.Vorname
                               select s;

            ViewBag.SpeakerId = new SelectList(speakerQuery.AsNoTracking(), "ID", "KurzZeichen", selectedSpeaker);
        }

        // GET: KnowledgeCircle/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var knowledgeCircle = await _context.KnowledgeCircles
                .Include(kc => kc.Speaker)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (knowledgeCircle == null)
            {
                return NotFound();
            }

            return View(knowledgeCircle);
        }

        // POST: KnowledgeCircle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var knowledgeCircle = await _context.KnowledgeCircles.FindAsync(id);
            _context.KnowledgeCircles.Remove(knowledgeCircle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KnowledgeCircleExists(int id)
        {
            return _context.KnowledgeCircles.Any(e => e.ID == id);
        }
    }
}
