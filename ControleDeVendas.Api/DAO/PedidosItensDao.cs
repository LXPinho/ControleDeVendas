using ControleDeVendas.Api.Model;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;

namespace ControleDeVendas.Api.DAO
{
    public class PedidosItensDao : IBaseDao
    {
        private string ConnectionString { get; set; }
        public PedidosItensDao()
        {
            ConnectionString = BaseDao.getConnectionString();
        }
        public bool Alterar(IBaseModel baseModel)
        {
            bool result = false;
            PedidoItens pedidoItens = (PedidoItens)baseModel;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "Update PedidoItens Set IdPedido = @IdPedido, IdProduto = @IdProduto, Quantidade = @Quantidade where Id=@Id";
                    command.Parameters.AddWithValue("@Id", pedidoItens.Id);
                    command.Parameters.AddWithValue("@IdPedido", pedidoItens.PedidoPai.Id);
                    command.Parameters.AddWithValue("@IdProduto", pedidoItens.ProdutoItem.Id);
                    command.Parameters.AddWithValue("@Quantidade", pedidoItens.Quantidade);

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
            PedidoItens pedidoItens = (PedidoItens)baseModel;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "Delete PedidoItens where Id=@Id";
                    command.Parameters.AddWithValue("@Id", pedidoItens.Id);

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
            PedidoItens pedidoItens = (PedidoItens)baseModel;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT into PedidoItens (IdPedido, IdProduto, Quantidade) VALUES (@IdPedido, @IdProduto, @Quantidade)";
                    command.Parameters.AddWithValue("@IdPedido", pedidoItens.PedidoPai.Id);
                    command.Parameters.AddWithValue("@IdProduto", pedidoItens.ProdutoItem.Id);
                    command.Parameters.AddWithValue("@Quantidade", pedidoItens.Quantidade);

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
        private class ListaPedidosItens
        {
            public List<PedidoItens> PedidosItens { get; set; } = new List<PedidoItens>();
        }
        public String Listar(IBaseModel baseModel)
        {
            String result = string.Empty;
            PedidoItens pedidoItens = (PedidoItens)baseModel;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT  PedidoItens.Id,"
                                                + "PedidoITens.IdPedido,"
                                                + "Pedido.IdCliente, "
                                                + "Pedido.DataDaVenda,"
                                                + "Cliente.Nome, "
                                                + "Cliente.Telefone, "
                                                + "Cliente.Email, "
                                                + "PedidoItens.IdProduto, "
                                                + "Produto.Descricao, "
                                                + "Produto.ValorUnitario, "
                                                + "PedidoItens.Quantidade "
                                        + "FROM PedidoItens  "
                                        + "Inner Join Produto on PedidoItens.IdProduto = Produto.Id "
                                        + "Inner Join Pedido on PedidoItens.IdPedido = Pedido.Id "
                                        + "Inner Join Cliente on Pedido.IdCliente = Cliente.Id; ";
                    if (pedidoItens.Id > 0)
                    {
                        command.CommandText += " Where Id = @Id";
                        command.Parameters.AddWithValue("@Id", pedidoItens.Id);
                    }

                    try
                    {
                        connection.Open();
                        SqlDataReader recordsAffected = command.ExecuteReader();
                        ListaPedidosItens pedidosDestino = new ListaPedidosItens();
                        while (recordsAffected != null && recordsAffected.Read())
                        {
                            PedidoItens pedidoItensDestino = new PedidoItens();

                            pedidoItensDestino.Id = Convert.ToInt32(recordsAffected["Id"].ToString());
                            
                            pedidoItensDestino.PedidoPai.Id = Convert.ToInt32(recordsAffected["IdPedido"]);
                            pedidoItensDestino.PedidoPai.DataDaVenda = Convert.ToDateTime(recordsAffected["DataDaVenda"]);

                            pedidoItensDestino.PedidoPai.ClientePedido.Id = Convert.ToInt32(recordsAffected["IdCliente"]);
                            pedidoItensDestino.PedidoPai.ClientePedido.Nome = Convert.ToString(recordsAffected["Nome"]);
                            pedidoItensDestino.PedidoPai.ClientePedido.Telefone = Convert.ToString(recordsAffected["Telefone"]);
                            pedidoItensDestino.PedidoPai.ClientePedido.Email = Convert.ToString(recordsAffected["Email"]);

                            pedidoItensDestino.ProdutoItem.Id = Convert.ToInt32(recordsAffected["IdProduto"]);
                            pedidoItensDestino.ProdutoItem.Descricao = Convert.ToString(recordsAffected["Descricao"]);
                            pedidoItensDestino.ProdutoItem.ValorUnitario = Convert.ToDouble(recordsAffected["ValorUnitario"]);

                            pedidoItensDestino.Quantidade = Convert.ToInt32(recordsAffected["Quantidade"]);

                            pedidosDestino.PedidosItens.Add(pedidoItensDestino);

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
