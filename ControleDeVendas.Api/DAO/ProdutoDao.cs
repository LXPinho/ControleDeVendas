using ControleDeVendas.Api.Model;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;

namespace ControleDeVendas.Api.DAO
{
    public class ProdutoDao : IBaseDao
    {
        private string ConnectionString { get; set; }
        public ProdutoDao()
        {
            ConnectionString = BaseDao.getConnectionString();
        }
        public bool Alterar(IBaseModel baseModel)
        {
            bool result = false;
            Produto produto = (Produto)baseModel;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;            
                    command.CommandType = CommandType.Text;
                    command.CommandText = "Update Produto Set Descricao=@Descricao, ValorUnitario=@ValorUnitario where Id=@Id";
                    command.Parameters.AddWithValue("@Id", produto.Id);
                    command.Parameters.AddWithValue("@Descricao", produto.Descricao);
                    command.Parameters.AddWithValue("@ValorUnitario", produto.ValorUnitario);
                    
                    try
                    {
                        connection.Open();
                        int recordsAffected = command.ExecuteNonQuery();
                        result = recordsAffected > 0;
                    }
                    catch (SqlException)
                    {
                        // error here
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return result;
        }

        public bool Excluir(IBaseModel baseModel)
        {
            bool result = false;
            Produto produto = (Produto)baseModel;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;            
                    command.CommandType = CommandType.Text;
                    command.CommandText = "Delete Produto where Id=@Id";
                    command.Parameters.AddWithValue("@Id", produto.Id);

                    try
                    {
                        connection.Open();
                        int recordsAffected = command.ExecuteNonQuery();
                        result = recordsAffected > 0;
                    }
                    catch (SqlException)
                    {
                        // error here
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return result;
        }

        public bool Inserir(IBaseModel baseModel)
        {
            bool result = false;
            Produto produto = (Produto)baseModel;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;            
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT into Produto (Descricao, ValorUnitario) VALUES (@Descricao, @ValorUnitario)";
                    command.Parameters.AddWithValue("@Descricao", produto.Descricao);
                    command.Parameters.AddWithValue("@ValorUnitario", produto.ValorUnitario);

                    try
                    {
                        connection.Open();
                        int recordsAffected = command.ExecuteNonQuery();
                        result = recordsAffected > 0;
                    }
                    catch (SqlException)
                    {
                        // error here
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return result;
        }
        private class ListaProdutos
        {
            public List<Produto> Produtos { get; set; } = new List<Produto>();
        }
        public String Listar(IBaseModel baseModel)
        {
            String result = string.Empty;
            Produto produto = (Produto)baseModel;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;            
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT Id, Descricao, ValorUnitario From Produto";
                    if (produto.Id > 0)
                    {
                        command.CommandText += " Where Id = @Id";
                        command.Parameters.AddWithValue("@Id", produto.Id);
                    }

                    try
                    {
                        connection.Open();
                        SqlDataReader recordsAffected = command.ExecuteReader();
                        ListaProdutos produtosDestino = new ListaProdutos();
                        while (recordsAffected != null && recordsAffected.Read())
                        {
                            Produto produtoDestino = new Produto();

                            produtoDestino.Id = Convert.ToInt32(recordsAffected["Id"].ToString());
                            produtoDestino.Descricao = (string)recordsAffected["Descricao"];
                            produtoDestino.ValorUnitario = Convert.ToDouble(recordsAffected["ValorUnitario"]);
                            produtosDestino.Produtos.Add(produtoDestino);
                        }
                        result = JsonSerializer.Serialize(produtosDestino);
                    }
                    catch (SqlException)
                    {
                        // error here
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return result;
        }
    }
}
