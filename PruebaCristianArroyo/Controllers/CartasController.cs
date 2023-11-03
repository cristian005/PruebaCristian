using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaCristianArroyo.Models;
using System.Web;


namespace PruebaCristianArroyo.Controllers
{
    public class CartasController : Controller
    {
        private readonly PruebaCristianContext _context;

        public CartasController(PruebaCristianContext context)
        {
            _context = context;
        }

        // GET: Cartas
        public async Task<IActionResult> Index()
        {
              return _context.Cartas != null ? 
                          View(await _context.Cartas.ToListAsync()) :
                          Problem("Entity set 'PruebaCristianContext.Cartas'  is null.");
        }

        // GET: Cartas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cartas == null)
            {
                return NotFound();
            }

            var carta = await _context.Cartas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carta == null)
            {
                return NotFound();
            }

            return View(carta);
        }

        // GET: Cartas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cartas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Imagen,IdTabla")] Carta carta,List<IFormFile> Imagen)
        {
            foreach (var item in Imagen)
            {
                if(item.Length>0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        carta.Imagen = stream.ToArray();
                    }
                }
            }

            if (carta.Imagen != null && carta.Nombre != null  && carta.Nombre != "")
            {
                _context.Add(carta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carta);
        }

       
        // GET: Cartas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cartas == null)
            {
                return NotFound();
            }

            var carta = await _context.Cartas.FindAsync(id);
            if (carta == null)
            {
                return NotFound();
            }
            return View(carta);
        }

        // POST: Cartas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Imagen,IdTabla")] Carta carta, List<IFormFile> Imagen)
        {
            if (id != carta.Id)
            {
                return NotFound();
            }

            foreach (var item in Imagen)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        carta.Imagen = stream.ToArray();
                    }
                }
            }

            if (carta.Imagen != null && carta.Nombre != null && carta.Nombre != "")
            {
                try
                {
                    _context.Update(carta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartaExists(carta.Id))
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
            return View(carta);
        }

        // GET: Cartas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cartas == null)
            {
                return NotFound();
            }

            var carta = await _context.Cartas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carta == null)
            {
                return NotFound();
            }

            return View(carta);
        }

        // POST: Cartas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cartas == null)
            {
                return Problem("Entity set 'PruebaCristianContext.Cartas'  is null.");
            }
            var carta = await _context.Cartas.FindAsync(id);
            if (carta != null)
            {
                _context.Cartas.Remove(carta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartaExists(int id)
        {
          return (_context.Cartas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
