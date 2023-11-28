using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

////using Zbuss_Proyect.Models;
using Zbuss_Proyect.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Http;
using Zbuss_Proyect.Models;

namespace Zbuss_Proyect.Controllers
{
    [Authorize()]
    public class BusesapiController : Controller
    {
        public string BaseUrl { get; set; }

        public BusesapiController()
        {
            this.BaseUrl = "https://localhost:44391/api/Bus";
        }

        //--------------------Contexto por eliminar---------------------
        //private readonly bd_VENTAS_ZBUSSContext _context;

        //public BusesController(bd_VENTAS_ZBUSSContext objcontext)
        //{
        //    _context = objcontext;
        //}
        //--------------------------------------------------------------

        public async Task<ActionResult> Index()
        {
            List<vmBus> list = new List<vmBus>();

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(BaseUrl);
            HttpResponseMessage resp = await client.GetAsync("");

            if (resp.IsSuccessStatusCode)
            {
                var readtask = resp.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<vmBus>>(readtask);
            }

            return View(list);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            vmBus _bus;
            HttpClient client = new HttpClient(); // declarando conector
            client.BaseAddress = new Uri(BaseUrl + "/" + id.ToString()); //se establece el url a llamar
            // invocamos verbo Get
            HttpResponseMessage resp = await client.GetAsync("");
            if (resp.IsSuccessStatusCode)
            {
                //si la llamada fue exitosa
                var readtask = resp.Content.ReadAsStringAsync().Result;
                // deseralizar
                _bus = JsonConvert.DeserializeObject<vmBus>(readtask);
                return View(_bus);
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(vmBus pBus)
        {
            try
            {
                vmBus BusResultado = new vmBus();
                HttpClient client = new HttpClient(); // declarando conector
                client.BaseAddress = new Uri(BaseUrl);
                StringContent content = new StringContent(JsonConvert.SerializeObject(pBus), Encoding.UTF8, "application/json");
                HttpResponseMessage resp = await client.PutAsync("", content);
                if (resp.IsSuccessStatusCode)
                {
                    var readtask = resp.Content.ReadAsStringAsync().Result;
                    BusResultado = JsonConvert.DeserializeObject<vmBus>(readtask);
                }


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        //private bool BusExists(int id)
        //{
        //    return _context.TbBus.Any(e => e.Idbus == id);
        //}

        public async Task<ActionResult> Details(int id)
        {
            vmBus _bus;
            HttpClient client = new HttpClient(); // declarando conector
            client.BaseAddress = new Uri(BaseUrl + "/" + id.ToString()); // Estableciendo URL del servicio
            // Capturando Respuesta HTTP >>>>>      Método Http Get
            HttpResponseMessage resp = await client.GetAsync("");
            if (resp.IsSuccessStatusCode)
            {
                //si la llamada fue exitosa
                var readtask = resp.Content.ReadAsStringAsync().Result;
                // deseralizar
                _bus = JsonConvert.DeserializeObject<vmBus>(readtask);
                return View(_bus);
            }

            return View();
        }
        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(vmBus pBus)
        {
            vmBus BusResultado = new vmBus();
            HttpClient client = new HttpClient(); // declarando conector
            client.BaseAddress = new Uri(BaseUrl);// Estableciendo URL del servicio
            // Serializar      de objeto a cadena
            StringContent content = new StringContent(JsonConvert.SerializeObject(pBus), Encoding.UTF8, "application/json");
            // Capturando Respuesta HTTP >>>>>      Método Http POST
            HttpResponseMessage resp = await client.PostAsync("", content);
            if (resp.IsSuccessStatusCode)
            {
                var readtask = resp.Content.ReadAsStringAsync().Result;
                //deserializa   de cadena a objeto
                BusResultado = JsonConvert.DeserializeObject<vmBus>(readtask);

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Alert = "No se puede crear. Ya existe un bus con la misma placa";
            return View();
        }

        public async Task<ActionResult> Delete(int id)
        {
            vmBus _Bus;
            HttpClient client = new HttpClient(); // declarando conector
            client.BaseAddress = new Uri(BaseUrl + "/" + id.ToString()); //se establece el url a llamar
            // invocamos verbo Get
            HttpResponseMessage resp = await client.GetAsync("");
            if (resp.IsSuccessStatusCode)
            {
                //si la llamada fue exitosa
                var readtask = resp.Content.ReadAsStringAsync().Result;
                // deseralizar
                _Bus = JsonConvert.DeserializeObject<vmBus>(readtask);
                return View(_Bus);
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            vmBus _Bus = new vmBus();

            HttpClient client = new HttpClient(); // declarando conector
            client.BaseAddress = new Uri(BaseUrl + "/" + id.ToString()); //se establece el url a llamar
                                                                         // invocamos verbo Get
            HttpResponseMessage resp = await client.DeleteAsync("");
            if (resp.IsSuccessStatusCode)
            {
                //si la llamada fue exitosa
                var readtask = resp.Content.ReadAsStringAsync().Result;
                // deseralizar
                _Bus = JsonConvert.DeserializeObject<vmBus>(readtask);

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Alert = "No se puede eliminar porque el bus está relacionado con otra tabla";
            return View();
        }

    }
}
