using ControleDeVendas.Api.Model;
using System.Net;
using System.Text.Json;
using static ConsoleAppControleDeVendas.ExecCliente;

namespace ConsoleAppControleDeVendas
{
    internal class ExecProduto
    {
        static void ShowProdutos(List<ControleDeVendas.Api.Model.Produto>? produtos)
        {
            int count = 0;
            foreach (ControleDeVendas.Api.Model.Produto produto in produtos)
            {
                Console.Write($"[{++count}]\t");
                ShowProduto(produto);
            }
        }
        static void ShowProduto(ControleDeVendas.Api.Model.Produto Produto)
        {
            Console.WriteLine($"Descricao: {Produto.Descricao}\tValorUnitario:{Produto.ValorUnitario}");
        }
        static async Task<Uri> CreateProdutoAsync(ControleDeVendas.Api.Model.Produto produto)
        {
            HttpResponseMessage response = await Program.client.PostAsJsonAsync(
                $"api/Produto/Post{produto}", produto);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }
        public class ListaProdutos
        {
            public List<Produto> Produtos { get; set; } = new List<Produto>();
        }
        static async Task<ListaProdutos> GetProdutoAsync(string path)
        {
            ListaProdutos produtos;
            using (HttpResponseMessage response = await Program.client.GetAsync(path + $"api/Produto/"))
            {
                produtos = response.IsSuccessStatusCode
                    ? JsonSerializer.Deserialize<ListaProdutos>(await response.Content.ReadAsAsync<string>()) ?? new ListaProdutos()
                    : new ListaProdutos();
            }
            return produtos;
        }
        static async Task<ControleDeVendas.Api.Model.Produto> UpdateProdutoAsync(ControleDeVendas.Api.Model.Produto produto)
        {
            HttpResponseMessage response = await Program.client.PutAsJsonAsync(
                $"api/Produto/Put{produto.Id},{produto}", produto);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            produto = await response.Content.ReadAsAsync<Produto>();
            return produto;
        }
        static async Task<HttpStatusCode> DeleteProdutoAsync(string id)
        {
            HttpResponseMessage response = await Program.client.DeleteAsync(
                $"api/Produto/{id}");
            return response.StatusCode;
        }

        public static async Task RunAsync(Program.enum_opcao opcao = Program.enum_opcao.enumGet)
        {
            ListaProdutos produtos = new ListaProdutos();
            try
            {
                switch (opcao)
                {
                    case Program.enum_opcao.enumCreate:
                        // Create
                        Uri url =
                            await CreateProdutoAsync(
                                new Produto
                                    {
                                        Id = 0,
                                        Descricao = "Produto de Teste 01",
                                        ValorUnitario = 1000.99
                                    });
                        Console.WriteLine($"Criado em {Program.client.BaseAddress}");
                        break;

                    case Program.enum_opcao.enumGet:

                        // Get 
                        produtos = await GetProdutoAsync(Program.client.BaseAddress.ToString());
                        Console.WriteLine($"Listado em {Program.client.BaseAddress}");
                        ShowProdutos(produtos.Produtos);
                        break;

                    case Program.enum_opcao.enumUpdate:

                        // Update
                        Produto produto = await UpdateProdutoAsync(new Produto
                            {
                                Id = 1,
                                Descricao = "Produto Alterado",
                                ValorUnitario = 9999.99
                            });

                        Console.WriteLine($"Atualizado em {Program.client.BaseAddress}");
                        ShowProduto(produto);
                        break;

                    case Program.enum_opcao.enumDelete:
                        // Delete 
                        HttpStatusCode statusCode = await DeleteProdutoAsync(new Produto { Id = 6 }.Id.ToString());
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
