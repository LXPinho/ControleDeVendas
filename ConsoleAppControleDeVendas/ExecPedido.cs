using ControleDeVendas.Api.Model;
using System.Net;
using System.Text.Json;

namespace ConsoleAppControleDeVendas
{
    internal class ExecPedido
    {
        static void ShowPedidos(List<ControleDeVendas.Api.Model.Pedido>? pedidos)
        {
            int count = 0;
            foreach (ControleDeVendas.Api.Model.Pedido pedido in pedidos)
            {
                Console.Write($"[{++count}]\t");
                ShowPedido(pedido);
            }
        }
        static void ShowPedido(ControleDeVendas.Api.Model.Pedido pedido)
        {
            Console.WriteLine(
                $"IdCliente: {pedido.ClientePedido.Id}"
              + $"\tDataDaVenda:{pedido.DataDaVenda}");
        }
        static async Task<Uri> CreatePedidoAsync(ControleDeVendas.Api.Model.Pedido pedido)
        {
            HttpResponseMessage response = await Program.client.PostAsJsonAsync(
                $"api/Pedido/Post{pedido}", pedido);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }
        public class ListaPedidosItens
        {
            public List<Pedido> Pedidos { get; set; } = new List<Pedido>();
        }
        static async Task<ListaPedidosItens> GetPedidoAsync(string path)
        {
            ListaPedidosItens pedidos;
            using (HttpResponseMessage response = await Program.client.GetAsync(path + $"api/Pedido/"))
            {
                pedidos = response.IsSuccessStatusCode
                    ? JsonSerializer.Deserialize<ListaPedidosItens>(await response.Content.ReadAsAsync<string>()) ?? new ListaPedidosItens()
                    : new ListaPedidosItens();
            }
            return pedidos;
        }
        static async Task<Pedido> UpdatePedidoAsync(Pedido pedido)
        {
            HttpResponseMessage response = await Program.client.PutAsJsonAsync(
                $"api/Pedido/Put{pedido.Id},{pedido}", pedido);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            pedido = await response.Content.ReadAsAsync<Pedido>();
            return pedido;
        }
        static async Task<HttpStatusCode> DeletePedidoAsync(string id)
        {
            HttpResponseMessage response = await Program.client.DeleteAsync(
                $"api/Pedido/{id}");
            return response.StatusCode;
        }

        public static async Task RunAsync(Program.enum_opcao opcao = Program.enum_opcao.enumGet)
        {
            ListaPedidosItens pedidos = new ListaPedidosItens();
            try
            {
                switch (opcao)
                {
                    case Program.enum_opcao.enumCreate:
                        // Create
                        Uri url =
                            await CreatePedidoAsync(
                                new Pedido
                                    {
                                        Id = 0,
                                        ClientePedido = new Cliente { Id = 1 }
                                    });;
                        Console.WriteLine($"Criado em {Program.client.BaseAddress}");
                        break;

                    case Program.enum_opcao.enumGet:
                          
                        // Get 
                        pedidos = await GetPedidoAsync(Program.client.BaseAddress.ToString());
                        Console.WriteLine($"Listado em {Program.client.BaseAddress}");
                        ShowPedidos(pedidos.Pedidos);
                        break;

                    case Program.enum_opcao.enumUpdate:

                        // Update
                        Pedido pedido = await UpdatePedidoAsync(new Pedido
                        {
                            Id = 1,
                            ClientePedido = new Cliente { Id = 2 }
                        });

                        // Get 
                        Console.WriteLine($"Atualizado em {Program.client.BaseAddress}");
                        ShowPedido(pedido);
                        break;

                    case Program.enum_opcao.enumDelete:
                        // Delete 
                        HttpStatusCode statusCode = await DeletePedidoAsync(new ControleDeVendas.Api.Model.Pedido { Id = 6 }.Id.ToString());
                        Console.WriteLine($"Deletado (HTTP Status = {(int)statusCode})");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
