using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Zbuss_Proyect.ViewModel
{
    public class vmBus
    {
        public int Idbus { get; set; }
        [DataType(DataType.Text)]
        [MaxLength(6)]
        public string Placa { get; set; }
        public int Capacidad { get; set; }
        public int Pisos { get; set; }
    }
}
