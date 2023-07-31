using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace API_DataBase.Models
{
    public class Situacao
    {
        [Key]
        public int CD_SITUACAO { get; set; }
        [StringLength(200)]
        public string DC_SITUACAO { get; set; }
        public DateTime DT_ALTERACAO { get; set; }
    }
}
