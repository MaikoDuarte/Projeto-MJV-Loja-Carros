using loja_carros.Models;

namespace loja_carros.Dal;

public interface IPostgresDal
{
    List<Carro> ListarCarros();
    bool InserirCarro(int carroId, string marca, string modelo, int ano, string cor, float preco);
    bool DeletarCarro(int id);
    Carro SelecionarCarro(int id);
    bool AtualizarCarro(int id, string marca, string modelo, int ano, string cor, float preco);
}