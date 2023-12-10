using ControleDeVendas.Api.DAO;
using ControleDeVendas.Api.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControleDeVendas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        // GET: api/<Movimentacao>
        [HttpGet]
        public string ? Get()
        {
            return new BaseDao(new PedidoDao()).Listar(new Pedido());
        }

        // GET api/<Movimentacao>/5
        [HttpGet("{id}")]
        public string ? Get(int id)
        {
            return new BaseDao(new PedidoDao()).Listar(new Pedido { Id = id});
        }

        // POST api/<Movimentacao>
        //[HttpPostt("{idCliente,idProduto,quanridade}")]
        [HttpPost]
        public ActionResult Post(int idCliente)
        {
            return Ok(new BaseDao(new PedidoDao()).Inserir(new Pedido { ClientePedido = new Cliente { Id = idCliente }, DataDaVenda = DateTime.Now }));
        }
        [HttpPost("Post{pedido}")]
        public ActionResult Post(Pedido pedido)
        {
            return Ok(new BaseDao(new PedidoDao()).Inserir(new Pedido { ClientePedido = new Cliente { Id = pedido.ClientePedido.Id }, DataDaVenda = DateTime.Now }));
        }

        // PUT api/<Movimentacao>/5
        //[HttpPut("{id,idCliente,idProduto,quanridade}")]
        [HttpPut]
        public ActionResult Put(int id, int idCliente )
        {
            return Ok(new BaseDao(new PedidoDao()).Alterar(new Pedido {Id = id, ClientePedido = new Cliente { Id = idCliente }, DataDaVenda = DateTime.Now }) ? "SUCESSO" : "FALHA");
        }
        [HttpPut("Put{id},{pedido}")]
        public Pedido Put(int id, Pedido pedido)
        {
            return (new BaseDao(new PedidoDao()).Alterar(new Pedido { Id = id, ClientePedido = new Cliente { Id = pedido.ClientePedido.Id }, DataDaVenda = DateTime.Now }) ? pedido : new Pedido());
        }

        // DELETE api/<Movimentacao>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(new BaseDao(new PedidoDao()).Excluir(new Pedido { Id = id }) ? "SUCESSO" : "FALHA");
        }
    }
}
