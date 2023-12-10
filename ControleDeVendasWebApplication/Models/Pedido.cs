using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ControleDeVendasWebApplication.Models
{
    [Table("Pedido")]
    public class Pedido : IBaseModel
    {
        [Column("Id")]
        [Display(Name = "Código do Pedido")]
        public int Id { get; set; } = 0;

        [Column("Cliente")]
        [Display(Name = "Cliente")]
        public Cliente ClientePedido { get; set; } = new Cliente();

        [Column("DataDaVenda")]
        [Display(Name = "Data da Venda")]
        public DateTime DataDaVenda { get; set; } = DateTime.Now;

        [Display(Name = "Lista de Clientes")]
        public List<Cliente> ListaClientesPedido { get; set; } = new List<Cliente>();

/*      public class MyViewModel
        {
            public int EmployeeId { get; set; }
            public string Comments { get; set; }
            public List<Employee> EmployeesList { get; set; }
        }
        public class Employee
        {
            public int Id { get; set; }
            public string Nome { get; set; }
        }
        public MyViewModel myViewModel;
*/    }
}
