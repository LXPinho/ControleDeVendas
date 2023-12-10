using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeVendasWebApplication.Models
{
    [Table("PedidoItens")]
    public class PedidoItens: IBaseModel
    {
        [Column("Id")]
        [Display(Name = "Código do Item Pedido")]
        public int Id { get; set; } = 0;
        
        [Column("PedidoPai")]
        [Display(Name = "Código do Pedido")]
        public Pedido PedidoPai { get; set; } = new Pedido();

        [Column("ProdutoItem")]
        [Display(Name = "ProdutoItem")]
        public Produto ProdutoItem { get; set; } = new Produto();

        [Column("Quantidade")]
        [Display(Name = "Quantidade")]
        public int Quantidade { get; set; } = 0;

        [Display(Name = "Lista de Produtos")]
        public List<Produto> ListaProdutosPedidoItens { get; set; } = new List<Produto>();
    }
}
