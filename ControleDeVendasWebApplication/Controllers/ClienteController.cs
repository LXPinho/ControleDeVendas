using ControleDeVendasWebApplication.Models;
using ControleDeVendasWebApplication.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeVendasWebApplication.Controllers
{
    public class ClienteController : Controller
    {
        // GET: ClienteController1
        public ActionResult Index()
        {
            List<Cliente> listaCliente;
            ExecCliente.RunAsync(ExecCliente.enum_opcao.enumGet).GetAwaiter().GetResult();

            listaCliente = ExecCliente.ListaClientesItens.Clientes.Select(i => i).ToList<Cliente>();

            //if (!listaCliente.Any())
            //{
            //    // Teste
            //    listaCliente =
            //        new List<Cliente>
            //            {
            //                new Cliente { Id = 1, Nome = "Cliente 1", Telefone = "111111111", Email = "cliente1@gmail.com"},
            //                new Cliente { Id = 2, Nome = "Cliente 2", Telefone = "222222222", Email = "cliente2@gmail.com"},
            //                new Cliente { Id = 3, Nome = "Cliente 3", Telefone = "333333331", Email = "cliente3@gmail.com"}
            //            };
            //}

            return View(listaCliente);
        }

        // GET: ClienteController1/Details/5
        public ActionResult Details(int id)
        {
            ExecCliente.RunAsync(ExecCliente.enum_opcao.enumGet).GetAwaiter().GetResult();
            Cliente? Cliente = ExecCliente.ListaClientesItens.Clientes
                                .Select(i => i)
                                .ToList<Cliente>()
                                .Where(i => i.Id == id)
                                .FirstOrDefault<Cliente>();
            return View(Cliente);
        }

        // GET: ClienteController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClienteController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                ExecCliente.LimpaClienteItem();
                ExecCliente.ClienteItem.Id = 0;
                ExecCliente.ClienteItem.Nome = Convert.ToString(collection["Nome"]); ;
                ExecCliente.ClienteItem.Telefone = Convert.ToString(collection["Telefone"]); ;
                ExecCliente.ClienteItem.Email = Convert.ToString(collection["Email"]); ;
                ExecCliente.RunAsync(ExecCliente.enum_opcao.enumCreate).GetAwaiter().GetResult();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClienteController1/Edit/5
        public ActionResult Edit(int id)
        {
            ExecCliente.RunAsync(ExecCliente.enum_opcao.enumGet).GetAwaiter().GetResult();
            Cliente? cliente = ExecCliente.ListaClientesItens.Clientes
                                .Select(i => i)
                                .ToList<Cliente>()
                                .Where(i => i.Id == id)
                                .FirstOrDefault<Cliente>();
            return View(cliente);
        }

        // POST: ClienteController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                ExecCliente.LimpaClienteItem();
                ExecCliente.ClienteItem.Id = id;
                ExecCliente.ClienteItem.Nome = Convert.ToString(collection["Nome"]); ;
                ExecCliente.ClienteItem.Telefone = Convert.ToString(collection["Telefone"]); ;
                ExecCliente.ClienteItem.Email = Convert.ToString(collection["Email"]); ;
                ExecCliente.RunAsync(ExecCliente.enum_opcao.enumUpdate).GetAwaiter().GetResult();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClienteController1/Delete/5
        public ActionResult Delete(int id)
        {
            ExecCliente.RunAsync(ExecCliente.enum_opcao.enumGet).GetAwaiter().GetResult();
            Cliente? cliente = ExecCliente.ListaClientesItens.Clientes
                                .Select(i => i)
                                .ToList<Cliente>()
                                .Where(i => i.Id == id)
                                .FirstOrDefault<Cliente>();
            return View(cliente);
        }

        // POST: ClienteController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                ExecCliente.LimpaClienteItem();
                ExecCliente.ClienteItem.Id = id;
                ExecCliente.RunAsync(ExecCliente.enum_opcao.enumDelete).GetAwaiter().GetResult();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
