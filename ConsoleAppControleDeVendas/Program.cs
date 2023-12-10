using System.Net.Http.Headers;

namespace ConsoleAppControleDeVendas
{
    public class Program
    {
        static public HttpClient client = new HttpClient();
        public enum enum_opcao { enumCreate, enumGet, enumUpdate, enumDelete }

        static void Main(string[] args)
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:5206");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


            //for (int i = 0; i < 11; i++)
            {
                Console.WriteLine("");
                Console.WriteLine("Cliente");
                Console.WriteLine("================================================");
                ExecCliente.RunAsync(enum_opcao.enumCreate).GetAwaiter().GetResult();
                ExecCliente.RunAsync(enum_opcao.enumUpdate).GetAwaiter().GetResult();
                ExecCliente.RunAsync(enum_opcao.enumDelete).GetAwaiter().GetResult();
                ExecCliente.RunAsync(enum_opcao.enumGet).GetAwaiter().GetResult();

                Console.WriteLine("");
                Console.WriteLine("Produto");
                Console.WriteLine("================================================");
                ExecProduto.RunAsync(enum_opcao.enumCreate).GetAwaiter().GetResult();
                ExecProduto.RunAsync(enum_opcao.enumUpdate).GetAwaiter().GetResult();
                ExecProduto.RunAsync(enum_opcao.enumDelete).GetAwaiter().GetResult();
                ExecProduto.RunAsync(enum_opcao.enumGet).GetAwaiter().GetResult();

                Console.WriteLine("");
                Console.WriteLine("Pedido");
                Console.WriteLine("================================================");
                ExecPedido.RunAsync(enum_opcao.enumCreate).GetAwaiter().GetResult();
                ExecPedido.RunAsync(enum_opcao.enumUpdate).GetAwaiter().GetResult();
                ExecPedido.RunAsync(enum_opcao.enumDelete).GetAwaiter().GetResult();
                ExecPedido.RunAsync(enum_opcao.enumGet).GetAwaiter().GetResult();

                Console.WriteLine("");
                Console.WriteLine("Pedido Itens");
                Console.WriteLine("================================================");
                ExecPedidoItens.RunAsync(enum_opcao.enumCreate).GetAwaiter().GetResult();
                ExecPedidoItens.RunAsync(enum_opcao.enumUpdate).GetAwaiter().GetResult();
                ExecPedidoItens.RunAsync(enum_opcao.enumDelete).GetAwaiter().GetResult();
                ExecPedidoItens.RunAsync(enum_opcao.enumGet).GetAwaiter().GetResult();
            }

            Console.ReadLine();
        }

    }
}
