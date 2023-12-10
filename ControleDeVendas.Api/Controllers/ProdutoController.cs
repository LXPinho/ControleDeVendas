using ControleDeVendas.Api.DAO;
using ControleDeVendas.Api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ControleDeVendas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        // GET: api/<Produto>
        [HttpGet]
        public string? Get()
        {
            return new BaseDao(new ProdutoDao()).Listar(new Produto());
        }

        // GET api/<Produto>/5
        [HttpGet("{id}")]
        public string? Get(int id)
        {
            return new BaseDao(new ProdutoDao()).Listar(new Produto { Id = id });
        }

        // POST api/<Produto>
        //[HttpPostt("{id,descricao,valorUnitario}")]
        [HttpPost]
        public ActionResult Post(string descricao, double valoUnitario)
        {
            return Ok(new BaseDao(new ProdutoDao()).Inserir(new Produto { Descricao = descricao, ValorUnitario = valoUnitario}) ? "SUCESSO" : "FALHA"); ;
        }
        [HttpPost("Post{produto}")]
        public ActionResult Post(Produto produto) 
        {
            return Ok(new BaseDao(new ProdutoDao()).Inserir(new Produto { Descricao = produto.Descricao, ValorUnitario = produto.ValorUnitario}));
        }
        // PUT api/<Cliente>/5
        //[HttpPut("{id,nome,telefone,email}")]
        [HttpPut]
        public ActionResult Put(int id, string descricao, double valorUnitario)
        {
            return Ok(new BaseDao(new ProdutoDao()).Alterar(new Produto { Id = id, Descricao = descricao, ValorUnitario = valorUnitario }) ? "SUCESSO" : "FALHA"); ;
        }
        [HttpPut("Put{id},{produto}")]
        public Produto Put(int id, Produto produto)
        {
            return (new BaseDao(new ProdutoDao()).Alterar(new Produto { Id = id, Descricao = produto.Descricao, ValorUnitario = produto.ValorUnitario }) ? produto : new Produto()); 
        }

        // DELETE api/<Cliente>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(new BaseDao(new ProdutoDao()).Excluir(new Produto { Id = id }) ? "SUCESSO" : "FALHA"); ;
        }
    }
}
