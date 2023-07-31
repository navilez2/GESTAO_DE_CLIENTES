using System.ComponentModel.DataAnnotations;

namespace API_DataBase.Models;

public class Cliente
{
    [Key]
    public int ID { get; set; }
    [StringLength(200)]
    public string? Nome { get; set; }
    public long CPF { get; set; }
    [StringLength(3)]
    public string? Sexo { get; set; }
    public DateTime Nascimento { get; set; }
    public int ID_Situacao { get; set; }
    [StringLength(100)]
    public string? Situacao { get; set; }
    public DateTime Alteracao { get; set; }


}
