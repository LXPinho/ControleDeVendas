using ControleDeVendasWebApplication.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ControleDeVendasWebApplication
{
    public class ExecProduto
    {
        static public HttpClient client = new HttpClient();
        public enum enum_opcao { enumCreate, enumGet, enumUpdate, enumDelete }
        public class ListaProdutos
        {
            public List<Produto> Produtos { get; set; } = new List<Produto>();
        }
        static public ListaProdutos ListaProdutosItens { get; set; } = new ListaProdutos();   
        static public void LimpaListaProduto()
        {
            if(ListaProdutosItens != null && ListaProdutosItens.Produtos.Count() > 0)
            {
                ListaProdutosItens.Produtos.Clear();
            }
        }
        static public Produto ProdutoItem { get; set; } = new Produto();
        static public void LimpaProdutoItem()
        {
            ProdutoItem.Id = 0;
            ProdutoItem.Descricao = string.Empty;
            ProdutoItem.ValorUnitario = 0;
        }
        static void ShowProdutos(List<Produto>? produtos)
        {
            int count = 0;
            foreach (Produto produto in produtos)
            {
                Console.Write($"[{++count}]\t");
                ShowProduto(produto);
            }
        }
        static void ShowProduto(Produto Produto)
        {
            Console.WriteLine($"Descricao: {Produto.Descricao}\tValorUnitario:{Produto.ValorUnitario}");
        }
        static async Task<Uri> CreateProdutoAsync(Produto produto)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                $"api/Produto/Post{produto}", produto);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }
        static async Task<ListaProdutos> GetProdutoAsync(string path)
        {
            ListaProdutos produtos;
            using (HttpResponseMessage response = await client.GetAsync(path + $"api/Produto/"))
            {
                produtos = response.IsSuccessStatusCode
                    ? JsonSerializer.Deserialize<ListaProdutos>(await response.Content.ReadAsAsync<String>()) ?? new ListaProdutos()
                    : new ListaProdutos();
            }
            return produtos;
        }
        static async Task<Produto> UpdateProdutoAsync(Produto produto)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/Produto/Put{produto.Id},{produto}", produto);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            produto = await response.Content.ReadAsAsync<Produto>();

            return produto;
        }
        static async Task<HttpStatusCode> DeleteProdutoAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/Produto/{id}");
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

            ListaProdutosItens = new ListaProdutos();
            try
            {
                switch (opcao)
                {
                    case enum_opcao.enumCreate:
                        // Create
                        Uri url = await CreateProdutoAsync(ProdutoItem);
                        break;

                    case enum_opcao.enumGet:

                        // Get 
                        ListaProdutosItens = await GetProdutoAsync(client.BaseAddress.ToString());
                        break;

                    case enum_opcao.enumUpdate:

                        // Update
                        ProdutoItem = await UpdateProdutoAsync(ProdutoItem);
                        break;

                    case enum_opcao.enumDelete:
                        // Delete 
                        HttpStatusCode statusCode = await DeleteProdutoAsync(ProdutoItem.Id.ToString());
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
