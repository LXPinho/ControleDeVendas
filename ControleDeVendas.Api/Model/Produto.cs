using System.ComponentModel;

namespace ControleDeVendas.Api.Model
{
    public class Produto : IBaseModel
    {
        public int Id { get; set; } = 0;
        public  string Descricao { get; set; } = string.Empty;
        public double ValorUnitario { get; set; } = .0;
    }
}
