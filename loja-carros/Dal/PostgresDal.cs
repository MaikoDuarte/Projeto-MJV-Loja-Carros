using Npgsql;
using loja_carros.Models;

namespace loja_carros.Dal
{
    public class PostgresDal : IPostgresDal
    {
        private readonly string strConnection;

        private readonly IConfiguration _configuration;

        public PostgresDal(IConfiguration configuration)
        {
            _configuration = configuration;
            strConnection = _configuration.GetConnectionString("conn_postgres");
            
        }

        public List<Carro> ListarCarros()
        {
            List<Carro> listaCarros = new List<Carro>();

            using (NpgsqlConnection conn = new NpgsqlConnection(strConnection))
            {
                conn.Open();
                Console.WriteLine("Conexão aberta");

                NpgsqlCommand sqlCommand = new NpgsqlCommand();
                sqlCommand.CommandText = "SELECT * FROM carros";
                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.Connection = conn;

                NpgsqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Carro carro = new Carro();
                    if (!reader.IsDBNull(0))
                        carro.Id = reader.GetInt32(0);

                    if (!reader.IsDBNull(1))
                        carro.Marca = reader.GetString(1);

                    if (!reader.IsDBNull(2))
                        carro.Modelo = reader.GetString(2);

                    if (!reader.IsDBNull(3))
                        carro.Ano = reader.GetInt32(3);

                    if (!reader.IsDBNull(4))
                        carro.Cor = reader.GetString(4);

                    if (!reader.IsDBNull(5))
                        carro.Preco = reader.GetFloat(5);

                    listaCarros.Add(carro);
                }
            }

            return listaCarros;
        }

        public bool InserirCarro(int carroId, string marca, string modelo, int ano, string cor, float preco)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(strConnection))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Conexão aberta");

                    NpgsqlCommand sqlCommand = new NpgsqlCommand();
                    sqlCommand.CommandText =
                        "INSERT INTO carros (marca, modelo, ano, cor, preco) VALUES (@marca, @modelo, @ano, @cor, @preco)";
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.Connection = conn;

                    sqlCommand.Parameters.AddWithValue("@marca", marca);
                    sqlCommand.Parameters.AddWithValue("@modelo", modelo);
                    sqlCommand.Parameters.AddWithValue("@ano", ano);
                    sqlCommand.Parameters.AddWithValue("@cor", cor);
                    sqlCommand.Parameters.AddWithValue("@preco", preco);

                    int linhasAfetadas = sqlCommand.ExecuteNonQuery();
                    return linhasAfetadas > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                    return false;
                }
                finally
                {
                    conn.Close();
                    Console.WriteLine("Conexão fechada");
                }
            }
        }

        public bool DeletarCarro(int id)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(strConnection))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Conexão aberta");

                    NpgsqlCommand sqlCommand = new NpgsqlCommand();
                    sqlCommand.CommandText = "DELETE FROM carros WHERE id = @id";
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.Connection = conn;

                    sqlCommand.Parameters.AddWithValue("id", id);

                    int linhasAfetadas = sqlCommand.ExecuteNonQuery();
                    return linhasAfetadas > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                    return false;
                }
                finally
                {
                    conn.Close();
                    Console.WriteLine("Conexão fechada");
                }
            }
        }

        public Carro SelecionarCarro(int id)
        {
            Carro carro = new Carro();
            using (NpgsqlConnection conn = new NpgsqlConnection(strConnection))
            {
                conn.Open();
                Console.WriteLine("Conexão aberta");

                NpgsqlCommand sqlCommand = new NpgsqlCommand();
                sqlCommand.CommandText = "select * from carros where id=@id"; 
                sqlCommand.Parameters.AddWithValue("@id", id);
                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.Connection = conn;

                NpgsqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    if (!reader.IsDBNull(0)) 
                        carro.Id = reader.GetInt32(0);
                    if (!reader.IsDBNull(1)) 
                        carro.Marca = reader.GetString(1);

                    if (!reader.IsDBNull(2))
                        carro.Modelo = reader.GetString(2);

                    if (!reader.IsDBNull(3))
                        carro.Ano = reader.GetInt32(3);

                    if (!reader.IsDBNull(4))
                        carro.Cor = reader.GetString(4);

                    if (!reader.IsDBNull(5))
                        carro.Preco = reader.GetFloat(5);
                }
            }

            return carro;
        }

        public bool AtualizarCarro(int id, string marca, string modelo, int ano, string cor, float preco)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(strConnection))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Conexão aberta");

                    NpgsqlCommand sqlCommand = new NpgsqlCommand();
                    sqlCommand.CommandText =
                        @"UPDATE carros SET marca = @marca, modelo = @modelo, ano = @ano, cor = @cor, preco = @preco WHERE id = @id";
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.Connection = conn;

                    sqlCommand.Parameters.AddWithValue("id", id);
                    sqlCommand.Parameters.AddWithValue("marca", marca);
                    sqlCommand.Parameters.AddWithValue("modelo", modelo);
                    sqlCommand.Parameters.AddWithValue("ano", ano);
                    sqlCommand.Parameters.AddWithValue("cor", cor);
                    sqlCommand.Parameters.AddWithValue("preco", preco);

                    int linhasAfetadas = sqlCommand.ExecuteNonQuery();
                    return linhasAfetadas > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                    return false;
                }
                finally
                {
                    conn.Close();
                    Console.WriteLine("Conexão fechada");
                }
            }
        }

    }
    }

