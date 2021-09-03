using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace User.Models
{
    public class CompanyMoldels
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este Campo é Obrigatorio!")]
        public String Name { get; set; }

        public String descricao { get; set; }

        public String localizacao { get; set; }

        public String categoria { get; set; }

        public String logotipo { get; set; }


    }
}
