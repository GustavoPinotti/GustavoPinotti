using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ProjetoFinal.Models
{
    public class Usuario
    {

       [Key]

        public int id { get; set; }

        [Required(ErrorMessage = "O login deve ser informado")]
        public String Login { get; set; }

        [Required(ErrorMessage = "A senha deve ser inserida")]
        public String Password { get; set; }

      


    }


    
}
