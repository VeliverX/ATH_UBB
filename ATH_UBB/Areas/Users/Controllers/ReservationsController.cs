using ATH_UBB.Data;
using ATH_UBB.Model;
using ATH_UBB.Models;
using AutoMapper;
using IRepositoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryService;

namespace ATH_UBB.Areas.Users.Controllers
{
    [Area("Users")]
    [Authorize(Roles = "User")]
    public class ReservationsController : Controller
    {
        private readonly IRepositoryService<Reservation> _reservationRepository;
        private readonly IRepositoryService<Vehicle> _vehicleRepository;
        private readonly IMapper _mapper;
        public ReservationsController(ApplicationDbContext context, IMapper mapper)
        {
            _reservationRepository = new RepositoryService<Reservation>(context);
            _vehicleRepository = new RepositoryService<Vehicle>(context);
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Guid id, [Bind("StartDate,EndDate,User")] ReservationViewModel reservation)
        {
            if (ModelState.IsValid)
            {
                var vehicle = _vehicleRepository.GetSingle(id);
                if (vehicle.IsReserved)
                {
                    reservation.Id = Guid.NewGuid();
                    reservation.StartDay = DateTime.Now;
                    reservation.VehicleId = id;
                    _reservationRepository.Add(_mapper.Map<Reservation>(reservation));
                    _reservationRepository.Save();
                    vehicle.IsReserved = false;
                    _vehicleRepository.Edit(vehicle);
                    _vehicleRepository.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(reservation);
        }

    }
}
