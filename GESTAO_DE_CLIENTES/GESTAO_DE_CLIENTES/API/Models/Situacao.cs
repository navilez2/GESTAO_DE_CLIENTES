using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GESTAO_DE_CLIENTES.API.Models
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