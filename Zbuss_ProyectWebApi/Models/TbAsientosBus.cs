using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WSZbuss_Proyect.Models
{
    public partial class TbAsientosBus
    {
        public int Idasiento { get; set; }
        public int Idbus { get; set; }
        public int CodAsiento { get; set; }
        public string Inclinacion { get; set; }
        public decimal Precio { get; set; }
        public int PisoBus { get; set; }
        public bool Estado { get; set; }
    }
}
