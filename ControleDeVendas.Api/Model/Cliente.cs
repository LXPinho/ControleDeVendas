

namespace ControleDeVendas.Api.Model
{
    public class Cliente : IBaseModel
    {
        public int Id { get; set; } = 0;
        public string Nome { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
