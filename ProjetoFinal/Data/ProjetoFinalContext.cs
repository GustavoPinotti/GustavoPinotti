using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Models;

namespace ProjetoFinal.Data
{
    public class ProjetoFinalContext : DbContext
    {

       

        protected override void OnConfiguring(DbContextOptionsBuilder opt)
        {

            opt.UseSqlServer(@"Data Source=DESKTOP-3ST60PP; initial Catalog=ProjetoFInal; User ID=usuario;password=senha;language=Portuguese;Trusted_Connection=True");

        }

        public DbSet<Departamento> Departamentos { get; set; }

        public DbSet<Produtos> Produtos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        



    }
}
