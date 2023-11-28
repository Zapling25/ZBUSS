
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Zbuss_Proyect.Models
{
    public partial class TbDetalleViaje
    {
        public int Idviaje { get; set; }
        public int Idbus { get; set; }
        [DataType(DataType.Date)]
        public DateTime? FechaSalida { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan HoraSalida { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan HoraLlegada { get; set; }
        [DataType(DataType.Text)]
        [MaxLength(30)]
        public string PuntoPartida { get; set; }
        //[NotMapped]
        //public List<SelectListItem> Lugares { get; } = new List<SelectListItem>
        //{
        //    new SelectListItem { Value = "LIMA", Text = "LIMA"},
        //    new SelectListItem { Value = "CARAZ", Text = "CARAZ"},
        //    new SelectListItem { Value = "CARHUAZ", Text = "CARHUAZ"},
        //    new SelectListItem { Value = "HUACHO", Text = "HUACHO"},
        //    new SelectListItem { Value = "HUARAL", Text = "HUARAL"},
        //    new SelectListItem { Value = "HUARAZ", Text = "HUARAZ"},
        //    new SelectListItem { Value = "YUNGAY", Text = "YUNGAY"},
        //};

        [DataType(DataType.Text)]
        [MaxLength(30)]
        public string PuntoLlegada { get; set; }
        public int NroAsientos { get; set; }
        public int DuracionViaje { get; set; }

        public virtual TbBus IdbusNavigation { get; set; }
    }
}
