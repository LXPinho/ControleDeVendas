using ControleDeVendas.Api.Model;
using System.Net;
using System.Text.Json;

namespace ConsoleAppControleDeVendas
{
    internal class ExecPedidoItens
    {
        static void ShowPedidosItens(List<PedidoItens>? pedidosItens)
        {
            int count = 0;
            foreach (PedidoItens pedidoItem in pedidosItens)
            {
                Console.Write($"[{++count}]\t");
                ShowPedidoItens(pedidoItem);
            }
        }
        static void ShowPedidoItens(PedidoItens pedidoItens)
        {
            Console.WriteLine(
                $"IdPedido: {pedidoItens.PedidoPai.Id}"
              + $"\tIdProduto: {pedidoItens.ProdutoItem.Id}"
              + $"\tQuantidade:{pedidoItens.Quantidade}");
        }
        static async Task<Uri> CreatePedidoItensAsync(PedidoItens pedidoItens)
        {
            HttpResponseMessage response = await Program.client.PostAsJsonAsync(
                $"api/PedidoItens/Post{pedidoItens}", pedidoItens);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }
        public class ListaPedidosItens
        {
            public List<PedidoItens> PedidosItens { get; set; } = new List<PedidoItens>();
        }
        static async Task<ListaPedidosItens> GetPedidoItensAsync(string path)
        {
            ListaPedidosItens pedidos;
            using (HttpResponseMessage response = await Program.client.GetAsync(path + $"api/PedidoItens/"))
            {
                pedidos = response.IsSuccessStatusCode
                    ? JsonSerializer.Deserialize<ListaPedidosItens>(await response.Content.ReadAsAsync<string>()) ?? new ListaPedidosItens()
                    : new ListaPedidosItens();
            }
            return pedidos;
        }
        static async Task<PedidoItens> UpdatePedidoItensAsync(PedidoItens pedidoItens)
        {
            HttpResponseMessage response = await Program.client.PutAsJsonAsync(
                $"api/PedidoItens/Put{pedidoItens.Id},{pedidoItens}", pedidoItens);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            pedidoItens = await response.Content.ReadAsAsync<PedidoItens>();
            return pedidoItens;
        }
        static async Task<HttpStatusCode> DeletePedidoItensAsync(string id)
        {
            HttpResponseMessage response = await Program.client.DeleteAsync(
                $"api/PedidoItens/{id}");
            return response.StatusCode;
        }

        public static async Task RunAsync(Program.enum_opcao opcao = Program.enum_opcao.enumGet)
        {
            ListaPedidosItens pedidosItens = new ListaPedidosItens();
            try
            {
                switch (opcao)
                {
                    case Program.enum_opcao.enumCreate:
                        // Create
                        Uri url =
                            await CreatePedidoItensAsync(
                                new PedidoItens
                                    {
                                        Id = 0,
                                        PedidoPai = new Pedido { Id = 1 },
                                        ProdutoItem = new Produto { Id = 1 },
                                        Quantidade = 100
                                    });;
                        Console.WriteLine($"Criado em {Program.client.BaseAddress}");
                        break;

                    case Program.enum_opcao.enumGet:
                          
                        // Get 
                        pedidosItens = await GetPedidoItensAsync(Program.client.BaseAddress.ToString());
                        Console.WriteLine($"Listado em {Program.client.BaseAddress}");
                        ShowPedidosItens(pedidosItens.PedidosItens);
                        break;

                    case Program.enum_opcao.enumUpdate:

                        // Update
                        PedidoItens pedidoItens = await UpdatePedidoItensAsync(new PedidoItens
                        {
                            Id = 1,
                            PedidoPai = new Pedido { Id = 1 },
                            ProdutoItem = new Produto { Id = 1 },
                            Quantidade = 99
                        });

                        // Get 
                        Console.WriteLine($"Atualizado em {Program.client.BaseAddress}");
                        ShowPedidoItens(pedidoItens);
                        break;

                    case Program.enum_opcao.enumDelete:
                        // Delete 
                        HttpStatusCode statusCode = await DeletePedidoItensAsync(new ControleDeVendas.Api.Model.Pedido { Id = 6 }.Id.ToString());
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
