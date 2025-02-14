using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Controllers
{
    public class TareaController : Controller
    {
        private readonly TaskManagerContext _context;

        public TareaController(TaskManagerContext context)
        {
            _context = context;
        }

        // GET: Tarea/Create
        public IActionResult Create()
        {
            var categories = _context.Categories.ToList();
            var selectList = new SelectList(categories, "Id", "Name");

            var model = new Tarea
            {
                CategoriesList = selectList
            };

            return View(model);
        }


        // POST: Tarea/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Priority,DueDate,CategoryId")] Tarea tarea)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "Home");
            }
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name", tarea.CategoryId);
            return View(tarea);
        }

        // GET: Tarea/Edit/5
        public IActionResult Edit(int id)
        {
            var tarea = _context.Tasks.Include(t => t.CategoryId).FirstOrDefault(t => t.Id == id);
            if (tarea == null)
            {
                return NotFound();
            }

            var categories = _context.Categories.ToList();

            tarea.CategoriesList = new SelectList(categories, "Id", "Name", tarea.CategoryId);

            return View(tarea);
        }


        // POST: Tarea/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Priority,DueDate,CategoryId,IsCompleted")] Tarea tarea)
        {
            if (id != tarea.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Tasks.Any(e => e.Id == tarea.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), "Home");
            }
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name", tarea.CategoryId);
            return View(tarea);
        }

        // Acción para eliminar
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tasks
                .Include(t => t.CategoryId)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        // POST: Tarea/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarea = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(tarea);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
