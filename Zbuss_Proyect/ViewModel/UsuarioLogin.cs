using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zbuss_Proyect.ViewModel
{
    public class UsuarioLogin
    {
        public int IdUsuario { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public bool Estado { get; set; }
        public int Rol { get; set; }
    }
}
