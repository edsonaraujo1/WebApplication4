using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PagtoController : ControllerBase
  {
    private readonly AplicationDbContext _context;

    public PagtoController(AplicationDbContext context)
    {
      _context = context;
    }

    // GET: api/Pagto
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pagamento>>> GetPagamento()
    {

      int id_Pag = 0;
      int id_Cli = 0;
      string id_Cpf = "";
      string id_Contrato = "";
      string id_Parcela = "";
      string id_Valor = "";
      string id_Situacao = "";

      using (var contexto = new Contexto())
      {
        string strQuery_select3 = string.Format("SELECT * FROM Pagamento ORDER BY IdPagto ASC");

        using (SqlDataReader Select_dados3 = contexto.ExecutaComandoComRetorno(strQuery_select3))
        {
          double somavalor2 = 0;

          while (Select_dados3.Read())
          {
            id_Pag = int.Parse(Select_dados3["IdPagto"].ToString());
            id_Cli = int.Parse(Select_dados3["idCli"].ToString());
            id_Cpf = Select_dados3["Cpf"].ToString();
            id_Contrato = Select_dados3["Contrato"].ToString();
            id_Parcela = Select_dados3["Parcela"].ToString();
            id_Valor = Select_dados3["Valor"].ToString();
            id_Situacao = Select_dados3["Situacao"].ToString();

            DateTime Data = Convert.ToDateTime(Select_dados3["Data"].ToString().Substring(0, 10));
            double Valor = double.Parse(Select_dados3["Valor"].ToString());

            DateTime hj = DateTime.Now;
            TimeSpan diferenca = (Data - hj);
            int dias = diferenca.Days;

            double Multa = 4.80;
            double Juros = 2.00;
            double Honorario = 10.00;


            double Jurus3 = Juros / 100;
            double Jurus4 = Jurus3 * dias;

            double valor1 = (Valor + Multa);
            double valor2 = valor1 * (Jurus4 / 100);
            double valor3 = valor1 + valor2;
            double valor4 = valor3 * (Honorario / 100);
            double valor5 = valor4 + valor3;

            somavalor2 += valor5;

            string strQueryUpdat1 = "UPDATE Pagamento SET Total = '" + string.Format("{0:N}", valor5, 11) + "' WHERE IdPagto = " + id_Pag + "";
            contexto.ExecutaComando(strQueryUpdat1);

          }
        }
      }    

      return new JsonResult(await _context.Pagamento.ToListAsync()) { SerializerSettings = new JsonSerializerSettings() { Formatting = Formatting.Indented } };

    }

    // GET: api/Pagto/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Pagamento>> GetPagamento(int id)
    {
      var pagamento = await _context.Pagamento.FindAsync(id);

      int id_Pag = 0;
      int id_Cli = 0;
      string id_Cpf = "";
      string id_Contrato = "";
      string id_Parcela = "";
      string id_Valor = "";
      string id_Situacao = "";

      using (var contexto = new Contexto())
      {
        string strQuery_select3 = string.Format("SELECT * FROM Pagamento ORDER BY IdPagto ASC");
        //string strQuery_select3 = string.Format("SELECT * FROM Pagamento WHERE IdPagto = {0}", id);

        using (SqlDataReader Select_dados3 = contexto.ExecutaComandoComRetorno(strQuery_select3))
        {
          double somavalor2 = 0;

          while (Select_dados3.Read())
          {
            id_Pag = int.Parse(Select_dados3["IdPagto"].ToString());
            id_Cli = int.Parse(Select_dados3["idCli"].ToString());
            id_Cpf = Select_dados3["Cpf"].ToString();
            id_Contrato = Select_dados3["Contrato"].ToString();
            id_Parcela = Select_dados3["Parcela"].ToString();
            id_Valor = Select_dados3["Valor"].ToString();
            id_Situacao = Select_dados3["Situacao"].ToString();

            DateTime Data = Convert.ToDateTime(Select_dados3["Data"].ToString().Substring(0, 10));
            double Valor = double.Parse(Select_dados3["Valor"].ToString());

            DateTime hj = DateTime.Now;
            TimeSpan diferenca = (Data - hj);
            int dias = diferenca.Days;                  // Soma Numero de dias em atrazo
                                                        
            double Multa = 4.80;                        // Multa
            double Juros = 2.00;                        // Juros
            double Honorario = 10.00;                   // Honorario

            double Jurus3 = Juros / 100;                 // Calculo Juros
            double Jurus4 = Jurus3 * dias;               // Calculo dias

            double valor1 = (Valor + Multa);             // Valor Mais Multa
            double valor2 = valor1 * (Jurus4 / 100);     // Valor corrigido mais Juros
            double valor3 = valor1 + valor2;          
            double valor4 = valor3 * (Honorario / 100);  // Valor Atualizado
            double valor5 = valor4 + valor3;             // Valor final 

            somavalor2 += valor5;

            string strQueryUpdat1 = "UPDATE Pagamento SET Total = '" + string.Format("{0:N}", valor5, 11) + "' WHERE IdPagto = " + id_Pag + "";
            contexto.ExecutaComando(strQueryUpdat1);

          }
        }
      }
      if (pagamento == null)
      {
        return NotFound();
      }

      //return pagamento;

      return new JsonResult(pagamento) { SerializerSettings = new JsonSerializerSettings() { Formatting = Formatting.Indented } };
    }

    // PUT: api/Pagto/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPagamento(int id, Pagamento pagamento)
    {
      if (id != pagamento.IdPagto)
      {
        return BadRequest();
      }

      _context.Entry(pagamento).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!PagamentoExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/Pagto
    [HttpPost]
    public async Task<ActionResult<Pagamento>> PostPagamento(Pagamento pagamento)
    {
      _context.Pagamento.Add(pagamento);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetPagamento", new { id = pagamento.IdPagto }, pagamento);
    }

    // DELETE: api/Pagto/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Pagamento>> DeletePagamento(int id)
    {
      var pagamento = await _context.Pagamento.FindAsync(id);
      if (pagamento == null)
      {
        return NotFound();
      }

      _context.Pagamento.Remove(pagamento);
      await _context.SaveChangesAsync();

      return pagamento;
    }

    private bool PagamentoExists(int id)
    {
      return _context.Pagamento.Any(e => e.IdPagto == id);
    }
  }
}
