using ControleDeVendasWebApplication.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ControleDeVendasWebApplication
{
    internal class ExecCliente
    {
        static public HttpClient client = new HttpClient();
        public enum enum_opcao { enumCreate, enumGet, enumUpdate, enumDelete }
        public int Id { get; internal set; }
        public class ListaClientes
        {
            public List<Cliente> Clientes { get; set; } = new List<Cliente>();
        }
        static public ListaClientes ListaClientesItens { get; set; } = new ListaClientes();
        static public void LimpaListaClientes()
        {
            if (ListaClientesItens != null && ListaClientesItens.Clientes.Count() > 0)
            {
                ListaClientesItens.Clientes.Clear();
            }
        }
        static public Cliente ClienteItem { get; set; } = new Cliente();
        static public void LimpaClienteItem()
        {
            ClienteItem.Id = 0;
            ClienteItem.Nome = string.Empty;
            ClienteItem.Telefone = string.Empty;
            ClienteItem.Email = string.Empty;
        }

        static void ShowClientes(List<Cliente>? clientes)
        {
            int count = 0;
            foreach (Cliente cliente in clientes)
            {
                Console.Write($"[{++count}]\t");
                ShowCliente(cliente);
            }
        }
        static void ShowCliente(Cliente cliente)
        {
            Console.WriteLine($"Nome: {cliente.Nome}\tEmail:{cliente.Email}\tTelefone: {cliente.Telefone}");
        }
        static async Task<Uri> CreateClienteAsync(Cliente cliente)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                $"api/Cliente/Post{cliente}", cliente);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }
        static async Task<ListaClientes> GetClienteAsync(string path)
        {
            ListaClientes clientes;
            using (HttpResponseMessage response = await client.GetAsync(path + $"api/Cliente/"))
            {
                clientes = response.IsSuccessStatusCode
                    ? JsonSerializer.Deserialize<ListaClientes>(await response.Content.ReadAsAsync<string>()) ?? new ListaClientes()
                    : new ListaClientes();
            }
            return clientes;
        }
        static async Task<Cliente> UpdateClienteAsync(Cliente cliente)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/Cliente/Put{cliente.Id},{cliente}", cliente);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            cliente = await response.Content.ReadAsAsync<Cliente>();
            return cliente;
        }
        static async Task<HttpStatusCode> DeleteClienteAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/Cliente/{id}");
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

            ListaClientesItens = new ListaClientes();
            try
            {
                switch (opcao)
                {
                    case enum_opcao.enumCreate:
                        // Create
                        Uri url = await CreateClienteAsync(ClienteItem);
                        break;

                    case enum_opcao.enumGet:

                        // Get 
                        ListaClientesItens = await GetClienteAsync(client.BaseAddress.ToString());
                        break;

                    case enum_opcao.enumUpdate:
                        // Update
                        Cliente cliente = await UpdateClienteAsync(ClienteItem);
                        break;

                    case enum_opcao.enumDelete:
                        // Delete 
                        HttpStatusCode statusCode = await DeleteClienteAsync(ClienteItem.Id.ToString());
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
