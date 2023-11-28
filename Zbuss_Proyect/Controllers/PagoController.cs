using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zbuss_Proyect.Models;

namespace Zbuss_Proyect.Controllers
{
    public class PagoController : Controller
    {
        private readonly bd_VENTAS_ZBUSSContext _context;

        public PagoController(bd_VENTAS_ZBUSSContext context)
        {
            _context = context;
        }

        public IActionResult Procesar()
        {
            return View();
        }

        public TbDetalleVenta ProcesarCompra(TbDetalleVenta pVenta)
        {
            if (!VentaExists(pVenta.Idasiento))
            {
                TbAsientosBus asientoBd = _context.TbAsientosBus.FirstOrDefault(x => x.Idasiento == pVenta.Idasiento);
                asientoBd.Estado = false;
                _context.Entry(asientoBd).State = EntityState.Modified;

                var subtotal = Convert.ToDouble(pVenta.SubTotal);
                var igv = subtotal * 0.18;

                var usuario = HttpContext.User.Identity.Name;
                var usuarioBd = _context.TbUsuarios.FirstOrDefault(x => x.Correo == usuario);

                TbDetalleVenta ventaBd = new TbDetalleVenta()
                {
                    Iduser = pVenta.Iduser,
                    Idasiento = pVenta.Idasiento,
                    FechaVenta = DateTime.Now,
                    SubTotal = pVenta.SubTotal,
                    MetodoPago = "TARJETA",
                    Igv = Convert.ToDecimal(igv),
                    Descuento = 0,
                    Total = pVenta.Total,
                    Idcuenta = usuarioBd.IdUsuario,
                    FechaViaje = pVenta.FechaViaje,
                    Estado = true
                };
                if (ModelState.IsValid)
                {
                    _context.TbDetalleVenta.Add(ventaBd);
                    _context.SaveChanges();

                    pVenta.Idventa = ventaBd.Idventa;

                    return ventaBd;
                }

                return null;
            }
            return null;
        }

        private bool VentaExists(int idAsiento)
        {
            return _context.TbDetalleVenta.Any(e => (e.Idasiento == idAsiento && e.Estado == true));
        }
    }
}
