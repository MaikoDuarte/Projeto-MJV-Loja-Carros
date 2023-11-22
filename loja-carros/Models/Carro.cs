namespace loja_carros.Models
{
    public class Carro
    {
        public int Id { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public int Ano { get; set; }
        public string? Cor { get; set; }
        public float Preco { get; set; }
    }
}