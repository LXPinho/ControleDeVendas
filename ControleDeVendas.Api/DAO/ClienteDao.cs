using ControleDeVendas.Api.Model;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;

namespace ControleDeVendas.Api.DAO
{
    public class ClienteDao : IBaseDao
    {
        private string ConnectionString { get; set; }
        public ClienteDao()
        {
            ConnectionString = BaseDao.getConnectionString();
        }
        public bool Alterar(IBaseModel baseModel)
        {
            bool result = false;
            Cliente cliente = (Cliente)baseModel;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;            
                    command.CommandType = CommandType.Text;
                    command.CommandText = "Update Cliente Set Nome=@Nome, Telefone=@Telefone, Email=@Email where Id=@Id";
                    command.Parameters.AddWithValue("@Id", cliente.Id);
                    command.Parameters.AddWithValue("@Nome", cliente.Nome);
                    command.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                    command.Parameters.AddWithValue("@Email", cliente.Email);

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
            Cliente cliente = (Cliente)baseModel;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;            
                    command.CommandType = CommandType.Text;
                    command.CommandText = "Delete Cliente where Id=@Id";
                    command.Parameters.AddWithValue("@Id", cliente.Id);

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
            Cliente cliente = (Cliente)baseModel;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;            
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT into Cliente (Nome, Telefone, Email) VALUES (@Nome, @Telefone, @Email)";
                    command.Parameters.AddWithValue("@Nome", cliente.Nome);
                    command.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                    command.Parameters.AddWithValue("@Email", cliente.Email);

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
        private class ListaClientes
        {
            public List<Cliente> Clientes { get; set; } = new List<Cliente>();
        }
        public String Listar(IBaseModel baseModel)
        {
            String result = string.Empty;
            Cliente cliente = (Cliente)baseModel;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;            
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT Id, Nome, Telefone, Email From Cliente";
                    if(cliente.Id > 0)
                    {
                        command.CommandText += " Where Id = @Id";
                        command.Parameters.AddWithValue("@Id", cliente.Id);
                    }

                    try
                    {
                        connection.Open();
                        SqlDataReader recordsAffected = command.ExecuteReader();
                        ListaClientes clientesDestino = new ListaClientes();
                        while (recordsAffected != null && recordsAffected.Read())
                        {
                            Cliente clienteDestino = new Cliente();

                            clienteDestino.Id = Convert.ToInt32(recordsAffected["Id"]);
                            clienteDestino.Nome = (string)recordsAffected["Nome"];
                            clienteDestino.Telefone = (string)recordsAffected["Telefone"];
                            clienteDestino.Email = (string)recordsAffected["Email"];
                            clientesDestino.Clientes.Add(clienteDestino);
                        }
                        result = JsonSerializer.Serialize(clientesDestino);
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
