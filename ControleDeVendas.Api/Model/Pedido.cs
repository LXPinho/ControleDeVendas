using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeVendas.Api.Model
{
    public class Pedido : IBaseModel
    {
        public int Id { get; set; } = 0;
        public Cliente ClientePedido { get; set; } = new Cliente();
        public DateTime DataDaVenda { get; set; }
    }
}
