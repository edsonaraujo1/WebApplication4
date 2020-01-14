using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
  public class Pagamento
  {
    [Key]
    public int IdPagto { get; set; }
    public int IdCli { get; set; }
    public string Cpf { get; set; }
    public string Contrato { get; set; }
    public int Parcela { get; set; }

    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    [DataType(DataType.DateTime)]
    public DateTime Data { get; set; }
    [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
    public decimal Valor { get; set; }
    [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
    public string Total { get; set; }
    public string Situacao { get; set; }

    public virtual Cliente Cliente { get; set; }

  }
}
