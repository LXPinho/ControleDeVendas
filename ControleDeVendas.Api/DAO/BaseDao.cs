using ControleDeVendas.Api.Model;

namespace ControleDeVendas.Api.DAO
{
    public class BaseDao : IBaseDao
    {
        private static string ConnectionString { get; set; } =
            new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build().GetConnectionString("DefaultConnection") ?? string.Empty;

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
