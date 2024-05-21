using denemeKnowling.Data;
using denemeKnowling.Models; // Import the namespace where Egitmen class is defined
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace denemeKnowling.Controllers
{
    public class EgitmenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EgitmenController(ApplicationDbContext context)
        {
            _context = context;
        }

    public IActionResult Index()
    {
        var egt = _context.Egitmen;
        return View(egt);
    }   
    public IActionResult Egitmen(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var egt = _context.Egitmen.Find(id);
        if (egt == null)
        {
            return NotFound();
        }
        return View(egt);
    }
}
}
