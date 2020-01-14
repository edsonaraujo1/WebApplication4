using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
  public class Cliente
  {
    [Key]
    public int IdCli { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public string Contrato { get; set; }
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime Data { get; set; }
    [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
    public decimal Valor_principal { get; set; }
    [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
    public decimal Valor_atualizado { get; set; }

    public virtual List<Pagamento> Pagamentos { get; set; }


  }
}
