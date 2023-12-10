using ControleDeVendas.Api.Model;

namespace ControleDeVendas.Api.DAO
{
    public class BaseDao : IBaseDao
    {
        private static string ConnectionString { get; set; } 
            = $@"Data Source=.\SQLEXPRESS; Initial Catalog=Vendas; Integrated Security=True;";

        private IBaseDao Dao { get; set; }

        public static string getConnectionString() => ConnectionString;

        public BaseDao( IBaseDao dao ) 
        { 
            Dao = dao;
        }
        public bool Alterar(IBaseModel baseModeldel)
        {
            return Dao.Alterar(baseModeldel);
        }

        public bool Excluir(IBaseModel baseModeldel)
        {
            return Dao.Excluir(baseModeldel);
        }

        public bool Inserir(IBaseModel baseModeldel)
        {
            return Dao.Inserir(baseModeldel);
        }

        public String Listar(IBaseModel baseModel)
        {
            return Dao.Listar(baseModel);
        }
    }
}
