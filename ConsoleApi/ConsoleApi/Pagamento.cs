using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApi
{
    class Pagamento
    {
        public int IdPagto { get; set; }
        public int IdCli { get; set; }
        public string Cpf { get; set; }
        public string Contrato { get; set; }
        public int Parcela { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public string Total { get; set; }
        public string Desconto { get; set; }
        public string Situacao { get; set; }
    }
}
