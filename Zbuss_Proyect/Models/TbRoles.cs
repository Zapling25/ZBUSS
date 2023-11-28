using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Zbuss_Proyect.Models
{
    public class TbRoles
    {
        public TbRoles()
        {
            TbUsuarios = new HashSet<TbUsuarios>();
        }

        public int Id { get; set; }
        [DataType(DataType.Text)]
        [MaxLength(30)]
        public string Descripcion { get; set; }
        public bool Estado { get; set; }

        public virtual ICollection<TbUsuarios> TbUsuarios { get; set; }
    }
}
