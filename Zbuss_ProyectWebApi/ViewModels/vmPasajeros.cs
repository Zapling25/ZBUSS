using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSZbuss_Proyect.ViewModels
{
    public class vmPasajeros
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
