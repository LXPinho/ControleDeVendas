using ControleDeVendas.Api.Model;

namespace ControleDeVendas.Api.DAO
{
    public interface IBaseDao
    {
        bool Inserir(IBaseModel baseModeldel);
        bool Alterar(IBaseModel baseModeldel);
        bool Excluir(IBaseModel baseModeldel);
        String Listar(IBaseModel baseModel);
    }
}
