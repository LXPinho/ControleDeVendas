using ControleDeVendasWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ControleDeVendasWebApplication.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: ProdutoController
        public ActionResult Index()
        {
            List<Produto> listaProdutos;
            ExecProduto.RunAsync(ExecProduto.enum_opcao.enumGet).GetAwaiter().GetResult();

            listaProdutos = ExecProduto.ListaProdutosItens.Produtos.Select(i => i).ToList<Produto>();

            //if (!listaProdutos.Any())
            //{
            //    // Teste
            //    listaProdutos =
            //        new List<Produto>
            //            {
            //            new Produto { Id = 1, Descricao = "Produto 1", ValorUnitario = 100 },
            //            new Produto { Id = 2, Descricao = "Produto 3", ValorUnitario = 200 },
            //            new Produto { Id = 3, Descricao = "Produto 3", ValorUnitario = 300 }
            //            };
            //}

            IEnumerable<ControleDeVendasWebApplication.Models.Produto> Model = listaProdutos.AsEnumerable();
            return View(Model);
        }

        // GET: ProdutoController/Details/5
        public ActionResult Details(int id)
        {
            ExecProduto.RunAsync(ExecProduto.enum_opcao.enumGet).GetAwaiter().GetResult();
            Produto? produto = ExecProduto.ListaProdutosItens.Produtos
                                .Select(i => i)
                                .ToList<Produto>()
                                .Where(i => i.Id == id)
                                .FirstOrDefault<Produto>();
            return View(produto);
        }

        // GET: ProdutoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProdutoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                ExecProduto.LimpaProdutoItem();
                ExecProduto.ProdutoItem.Descricao = Convert.ToString(collection["Descricao"]); ;
                ExecProduto.ProdutoItem.ValorUnitario = Convert.ToDouble(collection["ValorUnitario"].ToString()); ;
                ExecProduto.RunAsync(ExecProduto.enum_opcao.enumCreate).GetAwaiter().GetResult();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProdutoController/Edit/5
        public ActionResult Edit(int id)
        {
            ExecProduto.RunAsync(ExecProduto.enum_opcao.enumGet).GetAwaiter().GetResult();
            Produto? produto = ExecProduto.ListaProdutosItens.Produtos
                                .Select(i => i)
                                .ToList<Produto>()
                                .Where(i => i.Id == id)
                                .FirstOrDefault<Produto>();
            return View(produto);
        }

        // POST: ProdutoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                ExecProduto.LimpaProdutoItem();
                ExecProduto.ProdutoItem.Id = id;
                ExecProduto.ProdutoItem.Descricao = Convert.ToString(collection["Descricao"]); ;
                ExecProduto.ProdutoItem.ValorUnitario = Convert.ToDouble(collection["ValorUnitario"].ToString()); ;
                ExecProduto.RunAsync(ExecProduto.enum_opcao.enumUpdate).GetAwaiter().GetResult();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProdutoController/Delete/5
        public ActionResult Delete(int id)
        {
            ExecProduto.RunAsync(ExecProduto.enum_opcao.enumGet).GetAwaiter().GetResult();
            Produto? produto = ExecProduto.ListaProdutosItens.Produtos
                                .Select(i => i)
                                .ToList<Produto>()
                                .Where(i => i.Id == id)
                                .FirstOrDefault<Produto>();
            return View(produto);
        }

        // POST: ProdutoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                ExecProduto.LimpaProdutoItem();
                ExecProduto.ProdutoItem.Id = id;
                ExecProduto.RunAsync(ExecProduto.enum_opcao.enumDelete).GetAwaiter().GetResult();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public class ListaProdutos
        {
            public List<Produto> Produtos { get; set; } = new List<Produto>();
        }
    }
}
