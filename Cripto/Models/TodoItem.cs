using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    public class TodoItem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Nombre { get; set; }
        public decimal Valor { get; set; }
        public decimal Valor_Maximo { get; set; }
    }
}