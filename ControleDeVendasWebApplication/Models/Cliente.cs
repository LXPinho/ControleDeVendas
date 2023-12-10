using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ControleDeVendasWebApplication.Models
{
    public class Cliente : IBaseModel
    {
        [Column("Id")]
        [Display(Name = "Código do Cliente")]
        public int Id { get; set; } = 0;

        [Column("Nome")]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = "";

        [Column("Telefone")]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; } = "";

        [Column("Email")]
        [Display(Name = "Email")]
        public string Email { get; set; } = "";
    }
}
