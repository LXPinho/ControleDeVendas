namespace ControleDeVendas.Api.Model
{
    public class PedidoItens : IBaseModel
    {
        public int Id { get; set; } = 0;
        public Pedido PedidoPai { get; set; } = new Pedido();
        public Produto ProdutoItem { get; set; } = new Produto();
        public int Quantidade { get; set;} = 0;
    }
}
