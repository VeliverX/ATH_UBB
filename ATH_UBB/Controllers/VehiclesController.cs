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
    public class VehiclesController : Controller
    {
        private readonly IRepositoryService<Vehicle> _context;

        public VehiclesController(ApplicationDbContext context)
        {
            _context = new RepositoryService<Vehicle>(context);

            _context.Add(new Vehicle()
            {
                Id = Guid.NewGuid(),
                Name = "costam",
                Brand = "makita",
                Description = " super rower",
                Price = 999,
                Type = new VehicleType() { Id = Guid.NewGuid(), TypeName = "rower"}

            });
                
            
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var VehicleIndex = _context.GetAllRecords();
            return View(await VehicleIndex.ToListAsync());
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.GetAllRecords() == null)
            {
                return NotFound();
            }

            var vehicle = await _context.GetAllRecords()
                .Include(v => v.Reserv)
                .Include(v => v.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Brand,Price,Description,TypeId,ReservationId,RentalId")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                vehicle.Id = Guid.NewGuid();
                _context.Add(vehicle);
                _context.Save();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "Id", vehicle.ReservationId);
            //ViewData["TypeId"] = new SelectList(_context.VehicleTypes, "Id", "Id", vehicle.TypeId);
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if ( _context.GetAllRecords() == null)
            {
                return NotFound();
            }

            var vehicle =  _context.GetSingle(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            //ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "Id", vehicle.ReservationId);
            //ViewData["TypeId"] = new SelectList(_context.VehicleTypes, "Id", "Id", vehicle.TypeId);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Brand,Price,Description,TypeId,ReservationId,RentalId")] Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Edit(vehicle);
                    _context.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Id))
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
            //ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "Id", vehicle.ReservationId);
            //ViewData["TypeId"] = new SelectList(_context.VehicleTypes, "Id", "Id", vehicle.TypeId);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if ( _context.GetAllRecords() == null)
            {
                return NotFound();
            }

            var vehicle =await _context.GetAllRecords()
                .Include(v => v.Reserv)
                .Include(v => v.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.GetAllRecords() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Vehicles'  is null.");
            }
            var vehicle =  _context.GetSingle(id);
            if (vehicle != null)
            {
                _context.Delete(vehicle);
            }
            
            _context.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(Guid id)
        {
          return (_context.GetAllRecords()?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
