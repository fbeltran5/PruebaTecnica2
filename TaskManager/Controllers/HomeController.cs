using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly TaskManagerContext _context;

        public HomeController(TaskManagerContext context)
        {
            _context = context;
        }

        // Acción para la página de inicio donde mostramos las tareas
        public IActionResult Index()
        {
            var tareas = _context.Tasks.Include(t => t.Category).ToList();
            return View(tareas);
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
