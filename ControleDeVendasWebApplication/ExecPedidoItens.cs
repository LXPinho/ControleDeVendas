using ControleDeVendasWebApplication.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ControleDeVendasWebApplication
{
    internal class ExecPedidoItens
    {
        static public HttpClient client = new HttpClient();
        public enum enum_opcao { enumCreate, enumGet, enumUpdate, enumDelete }
        public class ListaPedidosItens
        {
            public List<PedidoItens> PedidosItens { get; set; } = new List<PedidoItens>();
        }
        static public ListaPedidosItens ListaPedidosItensItem { get; set; } = new ListaPedidosItens();
        static public void LimpaListaPedidosItensItem()
        {
            if (ListaPedidosItensItem != null &&  ListaPedidosItensItem.PedidosItens.Count() > 0)
            {
                ListaPedidosItensItem.PedidosItens.Clear();
            }
        }
        static public PedidoItens PedidoItensItem { get; set; } = new PedidoItens();
        static public void LimpaPedidoItem()
        {
            PedidoItensItem = new PedidoItens();
        }
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
            HttpResponseMessage response = await client.PostAsJsonAsync(
                $"api/PedidoItens/Post{pedidoItens}", pedidoItens);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }
        static async Task<ListaPedidosItens> GetPedidoItensAsync(string path)
        {
            ListaPedidosItens pedidos;
            using (HttpResponseMessage response = await client.GetAsync(path + $"api/PedidoItens/"))
            {
                pedidos = response.IsSuccessStatusCode
                    ? JsonSerializer.Deserialize<ListaPedidosItens>(await response.Content.ReadAsAsync<string>()) ?? new ListaPedidosItens()
                    : new ListaPedidosItens();
            }
            return pedidos;
        }
        static async Task<PedidoItens> UpdatePedidoItensAsync(PedidoItens pedidoItens)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/PedidoItens/Put{pedidoItens.Id},{pedidoItens}", pedidoItens);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            pedidoItens = await response.Content.ReadAsAsync<PedidoItens>();
            return pedidoItens;
        }
        static async Task<HttpStatusCode> DeletePedidoItensAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/PedidoItens/{id}");
            return response.StatusCode;
        }

        public static async Task RunAsync(enum_opcao opcao = enum_opcao.enumGet)
        {
            try
            {
                // Update port # in the following line.
                client.BaseAddress = new Uri("http://localhost:5206");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            }
            catch
            {
            }

            ListaPedidosItensItem = new ListaPedidosItens();
            try
            {
                switch (opcao)
                {
                    case enum_opcao.enumCreate:
                        // Create
                        Uri url = await CreatePedidoItensAsync(PedidoItensItem);
                        break;

                    case enum_opcao.enumGet:
                          
                        // Get 
                        ListaPedidosItensItem = await GetPedidoItensAsync(client.BaseAddress.ToString());
                        break;

                    case enum_opcao.enumUpdate:

                        // Update
                        PedidoItens pedidoItens = await UpdatePedidoItensAsync(PedidoItensItem);
                        break;

                    case enum_opcao.enumDelete:
                        // Delete 
                        HttpStatusCode statusCode = await DeletePedidoItensAsync(PedidoItensItem.Id.ToString());
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
