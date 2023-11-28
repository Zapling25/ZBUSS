using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WSZbuss_Proyect.Models
{
    public partial class TbPasajero
    {
        public int Iduser { get; set; }
        public string TpoDocumento { get; set; }
        public string NroDocumento { get; set; }
        public string Nombre { get; set; }
        public string ApePaterno { get; set; }
        public string ApeMaterno { get; set; }
        public string Correo { get; set; }
        public string Celular { get; set; }
        public string Sexo { get; set; }
        public DateTime FechaNac { get; set; }
        public string Estado { get; set; }
    }
}
