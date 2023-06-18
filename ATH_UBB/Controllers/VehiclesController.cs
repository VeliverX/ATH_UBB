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
using AutoMapper;
using ATH_UBB.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;

namespace ATH_UBB.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly IRepositoryService<Vehicle> _context;
        private readonly IMapper _mapper;
        private readonly IValidator<VehicleDetailViewModel> _validator;
        public VehiclesController(ApplicationDbContext context, IMapper mapper, IValidator<VehicleDetailViewModel> validator)
        {
            _context = new RepositoryService<Vehicle>(context);

            _context.Add(new Vehicle()
            {
                Id = Guid.NewGuid(),
                Name = "costam",
                Brand = "makita",
                Description = " super rower",
                Price = 999,
                IsReserved = false,
                Type = new VehicleType() { Id = Guid.NewGuid(), TypeName = "rower"},
                RentalPoint = new RentalPoint() { Id = Guid.NewGuid(), Adres = "Mickiewicza 1", City="Kety"}
                

            });
            _mapper = mapper;
            _validator = validator;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var VehicleIndex = _context.GetAllRecords();
            List<VehicleItemViewModel> vehiclesViewModelsIndex = new List<VehicleItemViewModel>();
            foreach (var item in VehicleIndex)
            {
                vehiclesViewModelsIndex.Add(_mapper.Map<VehicleItemViewModel>(item));
            }

            return View(vehiclesViewModelsIndex);
        }

        // GET: Vehicles/Details/5
        
        public async Task<IActionResult> Details(Guid? id)
        {
            if ( _context.GetAllRecords() == null)
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

            return View(_mapper.Map<VehicleDetailViewModel>(vehicle));
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
        public async Task<IActionResult> Create([Bind("Id,price,brand,model,localization,description,isAvailable")] VehicleDetailViewModel vehicle)
        {

            ValidationResult result = await _validator.ValidateAsync(vehicle);

            if (result.IsValid)
            {
                vehicle.Id = Guid.NewGuid();
                _context.Add(_mapper.Map<Vehicle>(vehicle));
                _context.Save();
                
                return RedirectToAction(nameof(Index));

            }
            result.Errors.ForEach(error => ModelState.AddModelError(error.PropertyName, error.ErrorMessage));
            return View(vehicle);

            //ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "Id", vehicle.ReservationId);
            //ViewData["TypeId"] = new SelectList(_context.VehicleTypes, "Id", "Id", vehicle.TypeId);

        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if ( _context.GetAllRecords() == null)
            {
                return NotFound();
            }
            var vehicle = _context.GetAllRecords()
                .Include(v => v.Reserv)
                .Include(v => v.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            //var vehicle =  _context.GetSingle(id).;
            if (vehicle == null)
            {
                return NotFound();
            }
            //ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "Id", vehicle.ReservationId);
            //ViewData["TypeId"] = new SelectList(_context.VehicleTypes, "Id", "Id", vehicle.TypeId);
            return View(_mapper.Map<VehicleDetailViewModel>(vehicle));
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,price,brand,model,localization,description,isAvailable")] VehicleDetailViewModel vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Edit(_mapper.Map<Vehicle>(vehicle));
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
            return View(_mapper.Map<Vehicle>(vehicle));
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

            return View(_mapper.Map<VehicleDetailViewModel>(vehicle));
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
            var vehicle = _context.GetAllRecords()
                .Include(v => v.Reserv)
                .Include(v => v.Type)
                .FirstOrDefault(m => m.Id == id);
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

      
        public IActionResult EndReservation(Guid id)
        {
           
            var vehicle = _context.GetSingle(id);                
            vehicle.IsReserved = true;
            _context.Edit(vehicle);
            _context.Save();    
            return Redirect("/Vehicles");
        }
    }
}
