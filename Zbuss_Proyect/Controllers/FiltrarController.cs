using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zbuss_Proyect.Models;

namespace Zbuss_Proyect.Controllers
{
    public class FiltrarController : Controller
    {
        private readonly bd_VENTAS_ZBUSSContext _context;

        public FiltrarController(bd_VENTAS_ZBUSSContext context)
        {
            _context = context;
        }
        public static List<TbDetalleViaje> _Viaje;

        public IActionResult Index()
        {
            if (_Viaje.Count() == 0)
            {
                ViewBag.Alert = "No se encontraron resultaron. Vuelve a la página principal e inténtelo de nuevo.";
                var bd_VENTAS_ZBUSSContext = _context.TbDetalleViaje.Include(t => t.IdbusNavigation);

                return View(bd_VENTAS_ZBUSSContext.ToList());

            }
            return View(_Viaje);
        }

        [HttpPost]
        public IActionResult Filtrar(TbDetalleViaje Viaje)
        {
            try
            {
                //    if(Viaje.FechaSalida == null || Viaje.PuntoPartida == null || Viaje.PuntoLlegada == null)
                //    {
                _Viaje = (from TbDetalleViaje v in _context.TbDetalleViaje
                              where v.FechaSalida == Viaje.FechaSalida &&
                                    v.PuntoLlegada == Viaje.PuntoLlegada &&
                                    v.PuntoPartida == Viaje.PuntoPartida
                              select v).Include(x => x.IdbusNavigation).ToList();
                //}
                //else
                //{
                //    _Viaje = (from TbDetalleViaje v in _context.TbDetalleViaje
                //              where v.FechaSalida == Viaje.FechaSalida &&
                //                    v.PuntoLlegada == Viaje.PuntoLlegada &&
                //                    v.PuntoPartida == Viaje.PuntoPartida
                //              select v).Include(x => x.IdbusNavigation).ToList();
                //}
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e) {
                throw e;
            }
        }
    }
}
