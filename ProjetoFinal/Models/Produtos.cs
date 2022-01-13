namespace ProjetoFinal.Models
{
    public class Produtos
    {

        public int Id { get; set; } 
        public string Name { get; set; }
        public int Quantidade { get; set; }
        public double Preco { get; set; }

        
        public virtual Departamento ? Departamento { get; set; } 
        
        public int DepartamentoId { get; set; }

        public Produtos()
        {
        }

        public Produtos(int id, string name, int quantidade, double preco)
        {
            Id = id;
            Name = name;
            Quantidade = quantidade;
            Preco = preco;
        }

        public void RemoverQuantidade(int quantidade)
        {

            Quantidade -= quantidade;

        }

        public void AdicionarQuantidade(int quantidade)
        {

            Quantidade += quantidade;

        }
    }
}
