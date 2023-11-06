using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PruebaCristianArroyo.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PruebaCristianArroyo.Controllers
{
    public class TablasController : Controller
    {
        private readonly PruebaCristianContext _context;

        public TablasController(PruebaCristianContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> GenerarTala(string usr)
        {
            int TablasUsuario = Convert.ToInt32(usr);

            //Leemos las cartas guardadas en la base de datos
            List<Carta> cartas = _context.Cartas.ToList();
            //Instanciamos el metodo random
            Random rand = new Random();


            //Generacion de Tablas dependiendo del usuario

            Tabla tablaList = new Tabla();
            var rows = from o in _context.Tablas
                       select o;
            //Limpiamos las tablas para generarlas dependiendo el usuario
            foreach (var row in rows)
            {
                _context.Tablas.Remove(row);
            }
            _context.SaveChanges();

            //ciclo para generar las tbalas dependiendo el usuario
            for (int tablaR = 1; tablaR <= TablasUsuario; tablaR++)
            {
                //Creamos una nueva lista con el metodo random
                var CartasRandom = cartas.OrderBy(_ => rand.Next()).ToList();

                //se guardan dependiendo el numero de carta
                for (int numCar = 0; numCar < 16; numCar++)
                {
                    tablaList = new Tabla();
                    tablaList.NumCarta = tablaR;
                    tablaList.IdCarta = CartasRandom[numCar].Id;
                    _context.Add(tablaList);

                    _context.SaveChanges();
                }

            }

            //Leemos las Tabla guardadas en la base de datos
            var Tables = _context.Tablas.ToList();

            //Creamos una lista con el modelo de la tabla con imagen
            List<ViewTabla> TablaImagen = new List<ViewTabla>();


            for(int i = 0;i<Tables.Count;i++)
            {
                ViewTabla elementos = new ViewTabla();
                var result = cartas.Find(x => x.Id == Tables[i].IdCarta);
                if (result != null)
                {
                    elementos.NumCarta = Tables[i].NumCarta;
                    elementos.Imagen = result.Imagen;
                    TablaImagen.Add(elementos);
                }
            }

            ViewBag.Tabla = TablaImagen;


            return View("Index");
        }

        // GET: Tablas
        public async Task<IActionResult> Index()
        {
           
              return _context.Tablas != null ? 
                          View(await _context.Tablas.ToListAsync()) :
                          Problem("Entity set 'PruebaCristianContext.Tablas'  is null.");
        }

        // GET: Tablas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tablas == null)
            {
                return NotFound();
            }

            var tabla = await _context.Tablas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tabla == null)
            {
                return NotFound();
            }

            return View(tabla);
        }

        // GET: Tablas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tablas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdCarta")] Tabla tabla)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tabla);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tabla);
        }

        // GET: Tablas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tablas == null)
            {
                return NotFound();
            }

            var tabla = await _context.Tablas.FindAsync(id);
            if (tabla == null)
            {
                return NotFound();
            }
            return View(tabla);
        }

        // POST: Tablas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdCarta")] Tabla tabla)
        {
            if (id != tabla.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tabla);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                   
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tabla);
        }

        // GET: Tablas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tablas == null)
            {
                return NotFound();
            }

            var tabla = await _context.Tablas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tabla == null)
            {
                return NotFound();
            }

            return View(tabla);
        }

        // POST: Tablas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tablas == null)
            {
                return Problem("Entity set 'PruebaCristianContext.Tablas'  is null.");
            }
            var tabla = await _context.Tablas.FindAsync(id);
            if (tabla != null)
            {
                _context.Tablas.Remove(tabla);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TablaExists(int id)
        {
          return (_context.Tablas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
