using ControleDeVendasWebApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using static ControleDeVendasWebApplication.ExecPedido;

namespace ControleDeVendasWebApplication.Controllers
{
    public class PedidoItensController : Controller
    {
        // GET: PedidoItensController
        static private int PedidoPaiId { get; set; } = 0;
        public ActionResult Index(int ? Id = 0)
        {
            List<PedidoItens> listaPedidoItens;
            ExecPedidoItens.RunAsync(ExecPedidoItens.enum_opcao.enumGet).GetAwaiter().GetResult();

            if( Id > 0)
                PedidoPaiId = Id ?? 0;

            listaPedidoItens = ExecPedidoItens.ListaPedidosItensItem.PedidosItens.Select(i => i).ToList<PedidoItens>();

            //if (!listaPedidoItens.Any())
            //{
            //    // Teste
            //    listaPedidoItens =
            //        new List<PedidoItens>
            //            {
            //            new PedidoItens { Id = 1, PedidoPai = new Pedido { Id =1 }, ProdutoItem = new Produto{ Id = 1 }, Quantidade = 100 },
            //            new PedidoItens { Id = 2, PedidoPai = new Pedido { Id =1 }, ProdutoItem = new Produto{ Id = 2 }, Quantidade = 200 },
            //            new PedidoItens { Id = 3, PedidoPai = new Pedido { Id =1 }, ProdutoItem = new Produto{ Id = 3 }, Quantidade = 300 }
            //            };
            //}

            if(PedidoPaiId != 0)
            {
                listaPedidoItens = listaPedidoItens.Where( x => x.PedidoPai.Id == PedidoPaiId).ToList<PedidoItens>();
            }

            IEnumerable<PedidoItens> Model = listaPedidoItens.AsEnumerable();
            return View(Model);
        }

        // GET: PedidoItensController/Details/5
        public ActionResult Details(int id)
        {
            ExecPedidoItens.RunAsync(ExecPedidoItens.enum_opcao.enumGet).GetAwaiter().GetResult();
            PedidoItens? pedido = ExecPedidoItens.ListaPedidosItensItem.PedidosItens
                                .Select(i => i)
                                .ToList<PedidoItens>()
                                .Where(i => i.Id == id)
                                .FirstOrDefault<PedidoItens>();
            return View(pedido);
        }

        // GET: PedidoItensController/Create
        public ActionResult Create()
        {
            PedidoItens pedidoItens = new PedidoItens();
            ExecProduto.RunAsync(ExecProduto.enum_opcao.enumGet).GetAwaiter().GetResult();
            pedidoItens.ListaProdutosPedidoItens = ExecProduto.ListaProdutosItens.Produtos.Select(i => i).ToList<Produto>(); ;
            return View(pedidoItens);
        }

        // POST: PedidoItensController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                ExecPedidoItens.LimpaPedidoItem();
                ExecPedidoItens.PedidoItensItem.Id = 0;
                ExecPedidoItens.PedidoItensItem.PedidoPai.Id = Convert.ToInt32(collection["PedidoPai.Id"]) != 0 ? Convert.ToInt32(collection["PedidoPai.Id"]) : PedidoPaiId;
                ExecPedidoItens.PedidoItensItem.ProdutoItem.Id = Convert.ToInt32(collection["ProdutoItem.Id"]); 
                ExecPedidoItens.PedidoItensItem.Quantidade= Convert.ToInt32(collection["Quantidade"]); ;
                ExecPedidoItens.RunAsync(ExecPedidoItens.enum_opcao.enumCreate).GetAwaiter().GetResult();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PedidoItensController/Edit/5
        public ActionResult Edit(int id)
        {
            ExecPedidoItens.RunAsync(ExecPedidoItens.enum_opcao.enumGet).GetAwaiter().GetResult();
            PedidoItens? pedidoItens = ExecPedidoItens.ListaPedidosItensItem.PedidosItens
                                .Select(i => i)
                                .ToList<PedidoItens>()
                                .Where(i => i.Id == id)
                                .FirstOrDefault<PedidoItens>();

            ExecProduto.RunAsync(ExecProduto.enum_opcao.enumGet).GetAwaiter().GetResult();
            if (pedidoItens != null)
            {
                pedidoItens.ListaProdutosPedidoItens = ExecProduto.ListaProdutosItens.Produtos.Select(i => i).ToList<Produto>(); ;
            }
            return View(pedidoItens);
        }

        // POST: PedidoItensController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                ExecPedidoItens.LimpaListaPedidosItensItem();
                ExecPedidoItens.PedidoItensItem.Id = id;
                ExecPedidoItens.PedidoItensItem.PedidoPai.Id = Convert.ToInt32(collection["PedidoPai.Id"]) != 0 ? Convert.ToInt32(collection["PedidoPai.Id"]) : PedidoPaiId;
                ExecPedidoItens.PedidoItensItem.ProdutoItem.Id = Convert.ToInt32(collection["ProdutoItem.Id"]); ;
                ExecPedidoItens.PedidoItensItem.Quantidade = Convert.ToInt32(collection["Quantidade"]); ;
                ExecPedidoItens.RunAsync(ExecPedidoItens.enum_opcao.enumUpdate).GetAwaiter().GetResult();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PedidoItensController/Delete/5
        public ActionResult Delete(int id)
        {
            ExecPedidoItens.RunAsync(ExecPedidoItens.enum_opcao.enumGet).GetAwaiter().GetResult();
            PedidoItens? pedidoItens = ExecPedidoItens.ListaPedidosItensItem.PedidosItens
                                .Select(i => i)
                                .ToList<PedidoItens>()
                                .Where(i => i.Id == id)
                                .FirstOrDefault<PedidoItens>();
            return View(pedidoItens);
        }

        // POST: PedidoItensController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                ExecPedidoItens.LimpaListaPedidosItensItem();
                ExecPedidoItens.PedidoItensItem.Id = id;
                ExecPedidoItens.RunAsync(ExecPedidoItens.enum_opcao.enumDelete).GetAwaiter().GetResult();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
