using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeVendasWebApplication.Models
{
    [Table("Produto")]
    public class Produto : IBaseModel
    {
        [Column("Id")]
        [Display(Name = "Código do Produto")]
        public int Id { get; set; } = 0;

        [Column("Descricao")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; } = "";

        [Column("ValorUnitario")]
        [Display(Name = "Valor Unitário")]
        public double ValorUnitario { get; set; } = .0;
    }
}
