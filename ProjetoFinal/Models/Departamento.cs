using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using ProjetoFinal.Models;

namespace ProjetoFinal.Models
{
    public class Departamento
    {

        
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection <Produtos> Produtos { get; set; } = new List<Produtos>();    

        public Departamento()
        {
        }

        public Departamento(int id, string name)
        {
            Id = id;
            Name = name;
        }

       

    }

}
