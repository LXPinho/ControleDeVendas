using ControleDeVendas.Api.Model;
using System.Net;
using System.Text.Json;

namespace ConsoleAppControleDeVendas
{
    internal class ExecCliente
    {
        public int Id { get; internal set; }

        static void ShowClientes(List<ControleDeVendas.Api.Model.Cliente>? clientes)
        {
            int count = 0;
            foreach (ControleDeVendas.Api.Model.Cliente cliente in clientes)
            {
                Console.Write($"[{++count}]\t");
                ShowCliente(cliente);
            }
        }
        static void ShowCliente(ControleDeVendas.Api.Model.Cliente cliente)
        {
            Console.WriteLine($"Nome: {cliente.Nome}\tEmail:{cliente.Email}\tTelefone: {cliente.Telefone}");
        }
        static async Task<Uri> CreateClienteAsync(ControleDeVendas.Api.Model.Cliente cliente)
        {
            HttpResponseMessage response = await Program.client.PostAsJsonAsync(
                $"api/Cliente/Post{cliente}", cliente);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }
        public class ListaClientes
        {
            public List<ControleDeVendas.Api.Model.Cliente> Clientes { get; set; } = new List<ControleDeVendas.Api.Model.Cliente>();
        }
        static async Task<ListaClientes> GetClienteAsync(string path)
        {
            ListaClientes clientes;
            using (HttpResponseMessage response = await Program.client.GetAsync(path + $"api/Cliente/"))
            {
                clientes = response.IsSuccessStatusCode
                    ? JsonSerializer.Deserialize<ListaClientes>(await response.Content.ReadAsAsync<string>()) ?? new ListaClientes()
                    : new ListaClientes();
            }
            return clientes;
        }
        static async Task<Cliente> UpdateClienteAsync(Cliente cliente)
        {
            HttpResponseMessage response = await Program.client.PutAsJsonAsync(
                $"api/Cliente/Put{cliente.Id},{cliente}", cliente);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            cliente = await response.Content.ReadAsAsync<Cliente>();
            return cliente;
        }
        static async Task<HttpStatusCode> DeleteClienteAsync(string id)
        {
            HttpResponseMessage response = await Program.client.DeleteAsync(
                $"api/Cliente/{id}");
            return response.StatusCode;
        }

        public static async Task RunAsync(Program.enum_opcao opcao = Program.enum_opcao.enumGet)
        {
            ListaClientes clientes = new ListaClientes();
            try
            {
                switch (opcao)
                {
                    case Program.enum_opcao.enumCreate:
                        // Create
                        Uri url = 
                            await CreateClienteAsync(
                                new Cliente
                                    {
                                        Id = 0,
                                        Nome = "Luciano Xavier de Pinho",
                                        Telefone = "99762015",
                                        Email = "lucianoxavierpinho@gmail.com"
                                    });
                        Console.WriteLine($"Criado em {Program.client.BaseAddress}");
                        break;

                    case Program.enum_opcao.enumGet:

                        // Get 
                        clientes = await GetClienteAsync(Program.client.BaseAddress.ToString());
                        Console.WriteLine($"Listado em {Program.client.BaseAddress}");
                        ShowClientes(clientes.Clientes);
                        break;

                    case Program.enum_opcao.enumUpdate:
                        // Update
                        Cliente cliente = await UpdateClienteAsync(new Cliente
                        {
                            Id = 1,
                            Nome = "Luciano Xavier de Pinho",
                            Telefone = "+5513997620154",
                            Email = "lucianoxavierpinho@gmail.com"
                        });

                        Console.WriteLine($"Atuallizado em {Program.client.BaseAddress}");
                        ShowCliente(cliente);
                        break;

                    case Program.enum_opcao.enumDelete:
                        // Delete 
                        HttpStatusCode statusCode = await DeleteClienteAsync(new Cliente { Id = 6 }.Id.ToString());
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
