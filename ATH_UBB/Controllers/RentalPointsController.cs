using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ATH_UBB.Data;
using ATH_UBB.Model;
using IRepositoryService;
using RepositoryService;

namespace ATH_UBB.Controllers
{
    public class RentalPointsController : Controller
    {
        private readonly IRepositoryService<RentalPoint> _context;

       
        public RentalPointsController(ApplicationDbContext context)
        {
            _context = new RepositoryService<RentalPoint>(context);
            _context.Add(new RentalPoint()
            {
                Id = Guid.NewGuid(),
                City = "Kęty",
                Adres = "costam",
                Vehicles = new List<Vehicle>()
               
            });
        }

        // GET: RentalPoints
        public async Task<IActionResult> Index()
        {
              var RentalPointIndex =  _context.GetAllRecords();
            return View(await RentalPointIndex.ToListAsync()); 
        }

        // GET: RentalPoints/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if ( _context.GetAllRecords() == null)
            {
                return NotFound();
            }

            var rentalPoint = await _context.GetAllRecords()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rentalPoint == null)
            {
                return NotFound();
            }

            return View(rentalPoint);
        }

        // GET: RentalPoints/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RentalPoints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Adres,City")] RentalPoint rentalPoint)
        {
            if (!ModelState.IsValid)
            {
                rentalPoint.Id = Guid.NewGuid();
                _context.Add(rentalPoint);
                _context.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(rentalPoint);
        }

        // GET: RentalPoints/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (_context.GetAllRecords() == null)
            {
                return NotFound();
            }

            var rentalPoint =_context.GetSingle(id);
            if (rentalPoint == null)
            {
                return NotFound();
            }
            return View(rentalPoint);
        }

        // POST: RentalPoints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Adres,City")] RentalPoint rentalPoint)
        {
            if (id != rentalPoint.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Edit(rentalPoint);
                    _context.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalPointExists(rentalPoint.Id))
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
            return View(rentalPoint);
        }

        // GET: RentalPoints/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (_context.GetAllRecords() == null)
            {
                return NotFound();
            }

            var rentalPoint = await _context.GetAllRecords().FirstOrDefaultAsync(m => m.Id == id);
            if (rentalPoint == null)
            {
                return NotFound();
            }

            return View(rentalPoint);
        }

        // POST: RentalPoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.GetAllRecords() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RentalPoints'  is null.");
            }
            var rentalPoint = _context.GetSingle(id);
            if (rentalPoint != null)
            {
                _context.Delete(rentalPoint);
            }
            
            _context.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool RentalPointExists(Guid id)
        {
          return (_context.GetAllRecords()?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
