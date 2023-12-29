namespace ProvaPub.Models.DTOs
{
    public class ProductsPut
    {
        public string Nome { get; set; }

        public ProductsPut(string nome)
        {
            Nome = nome;           
        }
    }
}
