using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace User.Models
{
    public class UserModels
    {
        [Key]
        public int Id { get; set; }

        public String Name { get; set; }

       
        public String Email { get; set; }

        [Required(ErrorMessage = "Este Campo é Obrigatorio!")]
        public String Password { get; set; }

        public String Role { get; set; }

        public String Phone { get; set; }

        public String cpf { get; set; }

    }
}
