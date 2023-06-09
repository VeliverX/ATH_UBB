﻿using System;
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
using FluentValidation;
using ATH_UBB.Models;
using FluentValidation.Results;

namespace ATH_UBB.Controllers
{
    public class RentalPointsController : Controller
    {
        private readonly IRepositoryService<RentalPoint> _context;
        private readonly IMapper _mapper;
        private readonly IValidator<RentalPointViewModel> _validator;

        public RentalPointsController(ApplicationDbContext context, IMapper mapper, IValidator<RentalPointViewModel> validator)
        {
            _context = new RepositoryService<RentalPoint>(context);
            _context.Add(new RentalPoint()
            {
                Id = Guid.NewGuid(),
                City = "Kęty",
                Adres = "costam",
                Vehicles = new List<Vehicle>()
               
            });
            _mapper = mapper;
            _validator = validator;
        }

        // GET: RentalPoints
        public async Task<IActionResult> Index()
        {
              var RentalPointIndex =  _context.GetAllRecords();
            List<RentalPointViewModel> rentalPointViewModelIndex = new List<RentalPointViewModel>();
            foreach (var item in RentalPointIndex)
            {
                rentalPointViewModelIndex.Add(_mapper.Map<RentalPointViewModel>(item));
            }
            return View(rentalPointViewModelIndex); 
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

            return View(_mapper.Map<RentalPointViewModel>(rentalPoint));
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
        public async Task<IActionResult> Create([Bind("Id,Adres,City")] RentalPointViewModel rentalPoint)
        {
            ValidationResult result = await _validator.ValidateAsync(rentalPoint);
            if (result.IsValid) //TO DO naprwić bo wykrzyknik by działało 
            {
                rentalPoint.Id = Guid.NewGuid();
                _context.Add(_mapper.Map<RentalPoint>(rentalPoint));
                _context.Save();
                return RedirectToAction(nameof(Index));
            }
            result.Errors.ForEach(error => ModelState.AddModelError(error.PropertyName, error.ErrorMessage));
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
            return View(_mapper.Map<RentalPointViewModel>(rentalPoint));
        }

        // POST: RentalPoints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Adres,City")] RentalPointViewModel rentalPoint)
        {
            if (id != rentalPoint.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Edit(_mapper.Map<RentalPoint>(rentalPoint));
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
            return View(_mapper.Map<RentalPoint>(rentalPoint));
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

            return View(_mapper.Map<RentalPointViewModel>(rentalPoint));
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
