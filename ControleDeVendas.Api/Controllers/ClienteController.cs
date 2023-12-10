using ControleDeVendas.Api.DAO;
using ControleDeVendas.Api.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControleDeVendas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        // GET: api/<Cliente>
        [HttpGet]
        public String? Get()
        {
            return new BaseDao(new ClienteDao()).Listar(new Cliente());
        }
        // GET api/<Cliente>/5
        [HttpGet("{id}")]
        public String? Get(int id)
        {
            return new BaseDao(new ClienteDao()).Listar(new Cliente { Id = id });
        }
        // POST api/<Cliente>
        //[HttpPostt("{id,nome,telefone,email}")]
        [HttpPost]
        public ActionResult Post(string nome, string telefone, string email)
        {
            return Ok(new BaseDao(new ClienteDao()).Inserir(new Cliente { Nome = nome, Telefone = telefone, Email = email }) ? "SUCESSO" : "FALHA"); ;
        }
        // POST api/<Cliente>
        [HttpPost("Post{cliente}")]
        public ActionResult Post(Cliente cliente)
        {
            return Ok(new BaseDao(new ClienteDao()).Inserir(new Cliente { Nome = cliente.Nome, Telefone = cliente.Telefone, Email = cliente.Email}) ? "SUCESSO" : "FALHA"); ;
        }
        // PUT api/<Cliente>/5
        //[HttpPut("{id,nome,telefone,email}")]
        [HttpPut]
        public ActionResult Put(int id, string nome, string telefone, string email)
        {
            return Ok(new BaseDao(new ClienteDao()).Alterar(new Cliente { Id = id,Nome = nome, Telefone = telefone, Email = email }) ? "SUCESSO" : "FALHA"); ;
        }
        [HttpPut("Put{id},{cliente}")]
        public Cliente Put(int id, Cliente cliente) 
        {
            return (new BaseDao(new ClienteDao()).Alterar(new Cliente { Id = id, Nome = cliente.Nome, Telefone = cliente.Telefone, Email = cliente.Email }) ? cliente : new Cliente());
        }
        // DELETE api/<Cliente>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(new BaseDao(new ClienteDao()).Excluir(new Cliente { Id = id }) ? "SUCESSO" : "FALHA"); ;
        }
    }
}
