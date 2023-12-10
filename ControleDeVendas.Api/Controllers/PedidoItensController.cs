using ControleDeVendas.Api.DAO;
using ControleDeVendas.Api.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace ControleDeVendas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoItensController : ControllerBase
    {
        // GET: api/<PedidoItens>
        [HttpGet]
        public string? Get()
        {
            return new BaseDao(new PedidosItensDao()).Listar(new PedidoItens());
        }

        // GET api/<PedidoItens>/5
        [HttpGet("{id}")]
        public string? Get(int id)
        {
            return new BaseDao(new PedidosItensDao()).Listar(new PedidoItens { Id = id });
        }

        // POST api/<PedidoItens>
        //[HttpPostt("{idPedido,idProduto,quanridade}")]
        [HttpPost]
        public ActionResult Post(int idPedido, int idProduto, int quantidade)
        {
            return Ok(new BaseDao(new PedidosItensDao()).Inserir(new PedidoItens { PedidoPai = new Pedido { Id = idPedido }, ProdutoItem = new Produto { Id = idProduto }, Quantidade = quantidade}));
        }
        [HttpPost("Post{pedidoItens}")]
        public ActionResult Post(PedidoItens pedidoItens)
        {
            return Ok(new BaseDao(new PedidosItensDao()).Inserir(new PedidoItens { PedidoPai = new Pedido { Id = pedidoItens.PedidoPai.Id }, ProdutoItem = new Produto { Id = pedidoItens.ProdutoItem.Id }, Quantidade = pedidoItens.Quantidade }));
        } 

        // PUT api/<PedidoItens>/5
        //[HttpPut("{id,idPedido,idProduto,quanridade}")]
        [HttpPut]
        public ActionResult Put(int id, int idPedido, int idProduto, int quantidade)
        {
            return Ok(new BaseDao(new PedidosItensDao()).Alterar(new PedidoItens { Id = id, PedidoPai = new Pedido { Id = idPedido }, ProdutoItem = new Produto { Id = idProduto }, Quantidade = quantidade }) ? "SUCESSO" : "FALHA");
        }
        [HttpPut("Put{id},{pedidoItens}")]
        public PedidoItens Put(int id, PedidoItens pedidoItens)
        {
            return (new BaseDao(new PedidosItensDao()).Alterar(new PedidoItens { Id = id, PedidoPai = new Pedido { Id = pedidoItens.PedidoPai.Id }, ProdutoItem = new Produto { Id = pedidoItens.PedidoPai.Id }, Quantidade = pedidoItens.Quantidade}) ? pedidoItens : new PedidoItens());
        }

        // DELETE api/<PedidoItens>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(new BaseDao(new PedidosItensDao()).Excluir(new PedidoItens { Id = id }) ? "SUCESSO" : "FALHA");
        }
    }
}
