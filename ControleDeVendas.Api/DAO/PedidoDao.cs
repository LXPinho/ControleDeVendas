using ControleDeVendas.Api.Model;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;

namespace ControleDeVendas.Api.DAO
{
    public class PedidoDao :IBaseDao
    {
        private string ConnectionString { get; set; }
        public PedidoDao()
        {
            ConnectionString = BaseDao.getConnectionString();
        }
        public bool Alterar(IBaseModel baseModel)
        {
            bool result = false;
            Pedido pedido = (Pedido)baseModel;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;            
                    command.CommandType = CommandType.Text;
                    command.CommandText = "Update Pedido Set IdCliente = @IdCliente, DataDaVenda = @DataDaVenda where Id=@Id";
                    command.Parameters.AddWithValue("@Id", pedido.Id);
                    command.Parameters.AddWithValue("@IdCliente", pedido.ClientePedido.Id);
                    command.Parameters.AddWithValue("@DataDaVenda", pedido.DataDaVenda);

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
            Pedido Pedido = (Pedido)baseModel;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;            
                    command.CommandType = CommandType.Text;
                    command.CommandText = "Delete Pedido where Id=@Id";
                    command.Parameters.AddWithValue("@Id", Pedido.Id);

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
            Pedido pedido = (Pedido)baseModel;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;            
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT into Pedido (IdCliente, DataDaVenda) VALUES (@IdCliente, @DataDaVenda)";
                    command.Parameters.AddWithValue("@IdCliente", pedido.ClientePedido.Id);
                    command.Parameters.AddWithValue("@DataDaVenda", pedido.DataDaVenda);

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
        private class ListaPedidos
        {
            public List<Pedido> Pedidos { get; set; } = new List<Pedido>();
        }
        public String Listar(IBaseModel baseModel)
        {
            String result = string.Empty;
            Pedido pedido = (Pedido)baseModel;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;            
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT Pedido.Id, "
                                                + "Pedido.IdCliente, "
                                                + "Cliente.Nome, "
                                                + "Cliente.Telefone, "
                                                + "Cliente.Email, "
                                                + "Pedido.DataDaVenda "
                                         + "FROM Pedido "
                                         + "Inner Join Cliente on Pedido.IdCliente = Cliente.Id;";

                    if (pedido.Id > 0)
                    {
                        command.CommandText += " Where Id = @Id";
                        command.Parameters.AddWithValue("@Id", pedido.Id);
                    }

                    try
                    {
                        connection.Open();
                        SqlDataReader recordsAffected = command.ExecuteReader();
                        ListaPedidos pedidosDestino = new ListaPedidos();
                        while (recordsAffected != null && recordsAffected.Read())
                        {
                            Pedido pedidoDestino = new Pedido();

                            pedidoDestino.Id = Convert.ToInt32(recordsAffected["Id"].ToString());

                            pedidoDestino.ClientePedido.Id = Convert.ToInt32(recordsAffected["IdCliente"]);
                            pedidoDestino.ClientePedido.Nome = Convert.ToString(recordsAffected["Nome"]);
                            pedidoDestino.ClientePedido.Telefone = Convert.ToString(recordsAffected["Telefone"]);
                            pedidoDestino.ClientePedido.Email = Convert.ToString(recordsAffected["Email"]);
                            
                            pedidoDestino.DataDaVenda = Convert.ToDateTime(recordsAffected["DataDaVenda"]);
                            
                            pedidosDestino.Pedidos.Add(pedidoDestino);

                        }
                        result = JsonSerializer.Serialize(pedidosDestino);
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
