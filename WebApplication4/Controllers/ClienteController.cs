using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
  public class ClienteController : Controller
  {
    private readonly AplicationDbContext _context;

    public ClienteController(AplicationDbContext context)
    {
      _context = context;
    }

    public Cliente Cliente { get; set; }

    // GET: Cliente
    public async Task<IActionResult> Index(string sortOrder, string searchString)
    {

      ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Cpf" : "";
      ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
      ViewData["CurrentFilter"] = searchString;

      var clientes = from s in _context.Cliente
                     select s;

      if (!String.IsNullOrEmpty(searchString))
      {
        clientes = clientes.Where(s => s.Nome.Contains(searchString)
                               || s.Cpf.Contains(searchString));
      }
      switch (sortOrder)
      {
        case "Nome":
          clientes = clientes.OrderByDescending(s => s.Nome);
          break;
        case "Date":
          clientes = clientes.OrderBy(s => s.Data);
          break;
        case "Cpf":
          clientes = clientes.OrderByDescending(s => s.Cpf);
          break;
        default:
          clientes = clientes.OrderBy(s => s.Nome);
          break;
      }

      return View(await clientes.ToListAsync());
    }

    // GET: Cliente/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      Cliente = await _context.Cliente.FirstOrDefaultAsync(m => m.IdCli == id);

      if (Cliente == null)
      {
        return NotFound();
      }

      StringBuilder formCorpo1t = new StringBuilder();
      StringBuilder formCorpo21t = new StringBuilder();
      StringBuilder sb = new StringBuilder();

      formCorpo21t.AppendLine("<a href='../Edit/" + id + "' class='btn btn-primary btn-sm'>Editar Cliente</a> | ");
      formCorpo21t.AppendLine("<a href='../Index' class='btn btn-outline-dark btn-sm'>Valtar</a> | ");
      formCorpo21t.AppendLine("<a href='../../Pagamento/Create/" + id + "' class='btn btn-outline-dark btn-sm'>Incluir Cobrança</a> | ");
      formCorpo21t.AppendLine("<a href='Report/" + id + "' class='btn btn-success btn-sm'>Relatório</a>");


      formCorpo1t.AppendLine("               <div class='table-responsive'>");
      formCorpo1t.AppendLine("                 <table class='table table-striped jambo_table bulk_action'>");
      formCorpo1t.AppendLine("                     <tr class='headings'>");
      formCorpo1t.AppendLine("                       <th class='column-title'>Order</th>");
      formCorpo1t.AppendLine("                       <th class='column-title'>Codigo</th>");
      formCorpo1t.AppendLine("                       <th class='column-title'>CPF</th>");
      formCorpo1t.AppendLine("                       <th class='column-title'>Contrato</th>");
      formCorpo1t.AppendLine("                       <th class='column-title'>Parcela</th>");
      formCorpo1t.AppendLine("                       <th class='column-title'>Valor</th>");
      formCorpo1t.AppendLine("                       <th class='column-title'>Atualizado</th>");
      formCorpo1t.AppendLine("                       <th class='column-title'>Desconto</th>");
      formCorpo1t.AppendLine("                       <th class='column-title'>Situacao</th>");
      formCorpo1t.AppendLine("                       <th class='column-title'></th>");
      formCorpo1t.AppendLine("                     </tr>");

      string strQuery_select3 = string.Format("SELECT * FROM Pagamento WHERE idCli = {0}", id);
      int ordem = 1;

      int id_Pag = 0;
      int id_Cli = 0;
      string id_Cpf = "";
      string id_Contrato = "";
      string id_Parcela = "";
      string id_Valor = "";
      string id_Situacao = "";
      string id_Desconto = "";

      using (var contexto = new Contexto())
      {

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
            id_Desconto = Select_dados3["Desconto"].ToString();

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

            formCorpo1t.AppendLine("        <tr class='even pointer labelrt'>");
            formCorpo1t.AppendLine("          <td class='a-center'>" + ordem + "</td>");
            formCorpo1t.AppendLine("          <td>" + id_Cli + "</td>");
            formCorpo1t.AppendLine("          <td class=' '>" + id_Cpf + "</td>");
            formCorpo1t.AppendLine("          <td class=' '>" + string.Format("{0:dd/MM/yyyy}", Data.ToString("d")) + "</td>");
            formCorpo1t.AppendLine("          <td class=' '>" + id_Parcela + "</td>");
            formCorpo1t.AppendLine("          <td class=' '>" + string.Format("{0:N}", id_Valor, 11) + "</td>");
            formCorpo1t.AppendLine("          <td class=' '>" + string.Format("{0:N}", valor5, 11) + "</td>");
            formCorpo1t.AppendLine("          <td class=' '>" + id_Desconto + "</td>");
            formCorpo1t.AppendLine("          <td class=' '>" + id_Situacao + "</td>");

            formCorpo1t.AppendLine("          <td><a href='../../Pagamento/Edit/?id=" + id_Pag + "' class='btn btn-primary btn-sm' >Alterar</a> |");
            formCorpo1t.AppendLine("          <a href='../../Pagamento/Delete/?id=" + id_Pag + "' class='btn btn-danger btn-sm' >Excluir</a></td>");
            ordem++;
          }

          formCorpo1t.AppendLine("          </tr>");
          formCorpo1t.AppendLine("   </table>");

          formCorpo1t.AppendLine("   <table>");
          formCorpo1t.AppendLine("          <tr>");
          formCorpo1t.AppendLine("          <td>");

          formCorpo1t.AppendLine("<h4>Valor Total do Debito: " + string.Format("{0:N}", somavalor2, 11) + "</h4>");

          formCorpo1t.AppendLine("          </td>");
          formCorpo1t.AppendLine("          </tr>");
          formCorpo1t.AppendLine("   </table>");

          formCorpo1t.AppendLine("</div>");

          string MeuNumero = string.Format("{0:N}", somavalor2, 12);
          string d = Math.Round(Convert.ToDecimal(MeuNumero), 2).ToString();
          string vl = d.Replace(",", ".");
          string strQueryUpdat2 = "UPDATE Cliente SET Valor_atualizado = '" + vl + "' WHERE IdCli = " + id + "";
          contexto.ExecutaComando(strQueryUpdat2);


        }
      }

      sb.AppendLine(formCorpo1t.ToString());

      ViewData["Clie_Ente"] = sb.ToString();
      ViewData["Clie_Botao"] = formCorpo21t.ToString();

      return View(Cliente);
    }

    // GET: Cliente/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: Cliente/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("IdCli,Nome,Cpf,Contrato,Data,Valor_principal,Valor_atualizado")] Cliente cliente)
    {
      if (ModelState.IsValid)
      {
        _context.Add(cliente);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(cliente);
    }

    // GET: Cliente/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var cliente = await _context.Cliente.FindAsync(id);
      if (cliente == null)
      {
        return NotFound();
      }
      ViewBag.IdCli = id;
      return View(cliente);
    }

    // POST: Cliente/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("IdCli,Nome,Cpf,Contrato,Data,Valor_principal,Valor_atualizado")] Cliente cliente)
    {
      if (id != cliente.IdCli)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(cliente);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!ClienteExists(cliente.IdCli))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction(nameof(Index));
      }
      return View(cliente);
    }

    // GET: Cliente/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var cliente = await _context.Cliente.FindAsync(id);

      if (cliente == null)
      {
        return NotFound();
      }

      return View(cliente);
    }

    // POST: Cliente/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var cliente = await _context.Cliente.FindAsync(id);
      _context.Cliente.Remove(cliente);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool ClienteExists(int id)
    {
      return _context.Cliente.Any(e => e.IdCli == id);
    }

    public async Task<IActionResult> ClientesPDF()
    {
      // Define la URL de la Cabecera 
      string _headerUrl = Url.Action("ClienteHeaderPDF", "Cliente", null, "http");
      // Define la URL del Pie de página
      string _footerUrl = Url.Action("ClienteFooterPDF", "Cliente", null, "http");

      return new ViewAsPdf("ClientesPDF", await _context.Cliente.ToListAsync())
      {
        // Establece la Cabecera y el Pie de página
        CustomSwitches = "--header-html " + _headerUrl + " --header-spacing 13 " +
                         "--footer-html " + _footerUrl + " --footer-spacing 0",
        PageMargins = new Margins(50, 10, 12, 10),
        FileName = "Rel_Cliente.pdf",
        PageSize = Size.A4,
        MinimumFontSize = 19,
        PageOrientation = Orientation.Portrait,
        IsGrayScale = false

      };
    }

    public IActionResult ClienteHeaderPDF()
    {
      return View("ClienteHeaderPDF");
    }

    public IActionResult ClienteFooterPDF()
    {
      return View("ClienteFooterPDF");
    }

  }
}
