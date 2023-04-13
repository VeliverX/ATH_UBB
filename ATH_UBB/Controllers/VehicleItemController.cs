using ATH_UBB.Data;
using ATH_UBB.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Drawing.Drawing2D;
using System.Security.Cryptography.X509Certificates;

namespace ATH_UBB.Controllers
{
    public class VehicleItemController : Controller
    {
        //private readonly IVehicleRepository _Vehiclecontext;


        //public VehicleItemController(IVehicleRepository context)
        //{
        //    _Vehiclecontext = context;
        //}
        static List<VehicleItemViewModel> vehicle = new List<VehicleItemViewModel>
        {
            new VehicleItemViewModel(){ Id = Guid.NewGuid(), isAvailable = true, localization="cos", model="cos", price = 1}
        };
       

        // GET: VehicleItemController
        public ActionResult Index()
        {
            //var vehicle = _Vehiclecontext.GetAll().Include(x => x.Reser).Include(x=> x.Type).Include(x=>x.Rental);

            //List<VehicleItemViewModel> list = new List<VehicleItemViewModel>();

            //foreach (var item in vehicle)
            //{
            //    var temp = new VehicleItemViewModel();

            //    temp.Id = item.IdVehicle;
            //    temp.price = item.price;
            //    temp.model = item.model;
            //    temp.isAvailable = item.Reser.isAvailable;
            //    temp.localization = item.Rental.localization;

            //list.Add(temp);

            //}

            return View(vehicle);
        }

        // GET: VehicleItemController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VehicleItemController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleItemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VehicleItemController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VehicleItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VehicleItemController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VehicleItemController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
