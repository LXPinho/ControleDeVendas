using ControleDeVendasWebApplication.Models;
using ControleDeVendasWebApplication.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using static ControleDeVendasWebApplication.Models.Pedido;

namespace ControleDeVendasWebApplication.Controllers
{
    public class PedidoController : Controller
    {
        // GET: PedidoController
        public ActionResult Index()
        {
             List<Pedido> listaPedidos;
             ExecPedido.RunAsync(ExecPedido.enum_opcao.enumGet).GetAwaiter().GetResult();

             listaPedidos = ExecPedido.ListaPedidosItens.Pedidos.Select(i => i).ToList<Pedido>();

            //if (!listaPedidos.Any())
            //{
            //    // Teste
            //    listaPedidos =
            //        new List<Pedido>
            //            {
            //            new Pedido { Id = 1, ClientePedido = new Cliente{ Id = 1 }, DataDaVenda = DateTime.Now },
            //            new Pedido { Id = 2, ClientePedido = new Cliente{ Id = 2 }, DataDaVenda = DateTime.Now },
            //            new Pedido { Id = 2, ClientePedido = new Cliente{ Id = 3 }, DataDaVenda = DateTime.Now }
            //            };
            //}

            IEnumerable<Pedido> Model = listaPedidos.AsEnumerable();
            return View(Model);
        }

        // GET: PedidoController/Details/5
        public ActionResult Details(int id)
        {
            ExecPedido.RunAsync(ExecPedido.enum_opcao.enumGet).GetAwaiter().GetResult();
            Pedido? pedido = ExecPedido.ListaPedidosItens.Pedidos
                                .Select(i => i)
                                .ToList<Pedido>()
                                .Where(i => i.Id == id)
                                .FirstOrDefault<Pedido>();
            return View(pedido);
        }

        // GET: PedidoController/Create
        public ActionResult Create()
        {
            Pedido pedido = new Pedido();
            ExecCliente.RunAsync(ExecCliente.enum_opcao.enumGet).GetAwaiter().GetResult();
            pedido.ListaClientesPedido = ExecCliente.ListaClientesItens.Clientes.Select(i => i).ToList<Cliente>();
            return View(pedido);
        }

        // POST: PedidoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                ExecPedido.LimpaPedidoItem();
                ExecPedido.PedidoItem.Id = 0;
                ExecPedido.PedidoItem.ClientePedido.Id = Convert.ToInt32(collection["ClientePedido.Id"]); ;
                ExecPedido.RunAsync(ExecPedido.enum_opcao.enumCreate).GetAwaiter().GetResult();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PedidoController/Edit/5
        public ActionResult Edit(int id)
        {
            ExecPedido.RunAsync(ExecPedido.enum_opcao.enumGet).GetAwaiter().GetResult();
            Pedido? pedido = ExecPedido.ListaPedidosItens.Pedidos
                                .Select(i => i)
                                .ToList<Pedido>()
                                .Where(i => i.Id == id)
                                .FirstOrDefault<Pedido>();
            ExecCliente.RunAsync(ExecCliente.enum_opcao.enumGet).GetAwaiter().GetResult();
            if (pedido != null)
            {
                pedido.ListaClientesPedido = ExecCliente.ListaClientesItens.Clientes.Select(i => i).ToList<Cliente>();
            }
            return View(pedido);
        }

        // POST: PedidoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                ExecPedido.LimpaPedidoItem();
                ExecPedido.PedidoItem.Id = id;
                ExecPedido.PedidoItem.ClientePedido.Id = Convert.ToInt32(collection["ClientePedido.Id"]); ;
                ExecPedido.PedidoItem.DataDaVenda = Convert.ToDateTime(collection["DataDaVenda"]); ;
                ExecPedido.RunAsync(ExecPedido.enum_opcao.enumUpdate).GetAwaiter().GetResult();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PedidoController/Delete/5
        public ActionResult Delete(int id)
        {
            ExecPedido.RunAsync(ExecPedido.enum_opcao.enumGet).GetAwaiter().GetResult();
            Pedido? pedido = ExecPedido.ListaPedidosItens.Pedidos
                                .Select(i => i)
                                .ToList<Pedido>()
                                .Where(i => i.Id == id)
                                .FirstOrDefault<Pedido>();
            return View(pedido);
        }

        // POST: PedidoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                ExecPedido.LimpaPedidoItem();
                ExecPedido.PedidoItem.Id = id;
                ExecPedido.RunAsync(ExecPedido.enum_opcao.enumDelete).GetAwaiter().GetResult();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
