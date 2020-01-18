using System;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
  public class PagamentoController : Controller
  {
    private readonly AplicationDbContext _context;

    public PagamentoController(AplicationDbContext context)
    {
      _context = context;
    }

    public Pagamento Pagamento { get; set; }

    // GET: Pagamento
    public async Task<IActionResult> Index(string sortOrder, string searchString)
    {

      ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
      ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
      ViewData["CurrentFilter"] = searchString;

      var pagamentos = from s in _context.Pagamento
                     select s;

      if (!String.IsNullOrEmpty(searchString))
      {
        pagamentos = pagamentos.Where(s => s.Cpf.Contains(searchString)
                               || s.Cpf.Contains(searchString));
      }
      switch (sortOrder)
      {
        case "name_desc":
          pagamentos = pagamentos.OrderByDescending(s => s.Contrato);
          break;
        case "Date":
          pagamentos = pagamentos.OrderBy(s => s.Data);
          break;
        case "date_desc":
          pagamentos = pagamentos.OrderByDescending(s => s.Cpf);
          break;
        default:
          pagamentos = pagamentos.OrderBy(s => s.Cpf);
          break;
      }

      //Cliente = await clientes.ToListAsync();


      return View(await pagamentos.ToListAsync());

    }

    // GET: Pagamento/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var pagamento = await _context.Pagamento
          .Include(p => p.Cliente)
          .FirstOrDefaultAsync(m => m.IdPagto == id);
      if (pagamento == null)
      {
        return NotFound();
      }

      return View(pagamento);
    }

    // GET: Pagamento/Create
    public IActionResult Create(int? id)
    {

      string id_CPF = "";
      string id_Contrato = "";

      if (id == null)
      {
        ViewData["IdCli"] = new SelectList(_context.Cliente, "IdCli", "IdCli");
      }
      else
      {
        ViewData["IdCli"] = id;

        string strQuery_select3 = string.Format("SELECT * FROM Cliente WHERE idCli = {0}", id);

        using (var contexto = new Contexto())
        {

          using (SqlDataReader Select_dados3 = contexto.ExecutaComandoComRetorno(strQuery_select3))
          {
            while (Select_dados3.Read())
            {
              id_CPF = Select_dados3["Cpf"].ToString();
              id_Contrato = Select_dados3["Contrato"].ToString();
            }
          }
        }


        ViewData["idCPF"] = id_CPF;
        ViewData["idContrato"] = id_Contrato;

      }

      //return Page();
      return View();

    //return View();
  }

  // POST: Pagamento/Create
  // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
  // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create([Bind("IdPagto,IdCli,Cpf,Contrato,Parcela,Data,Valor,Desconto,Situacao")] Pagamento pagamento)
  {
    if (ModelState.IsValid)
    {
      _context.Add(pagamento);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }
    ViewData["IdCli"] = new SelectList(_context.Cliente, "IdCli", "IdCli", pagamento.IdCli);
    return View(pagamento);
  }

  // GET: Pagamento/Edit/5
  public async Task<IActionResult> Edit(int? id)
  {
    if (id == null)
    {
      return NotFound();
    }

    var pagamento = await _context.Pagamento.FindAsync(id);
    if (pagamento == null)
    {
      return NotFound();
    }
    ViewData["IdCli"] = new SelectList(_context.Cliente, "IdCli", "IdCli", pagamento.IdCli);
    return View(pagamento);
  }

  // POST: Pagamento/Edit/5
  // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
  // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit(int id, [Bind("IdPagto,IdCli,Cpf,Contrato,Parcela,Data,Valor,Desconto,Situacao")] Pagamento pagamento)
  {
    if (id != pagamento.IdPagto)
    {
      //return NotFound();
    }

      if (ModelState.IsValid)
      {
      try
      {
        _context.Update(pagamento);
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!PagamentoExists(pagamento.IdPagto))
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
    ViewData["IdCli"] = new SelectList(_context.Cliente, "IdCli", "IdCli", pagamento.IdCli);
      //return RedirectToAction("Details/" + pagamento.IdCli + "", "Cliente");
      return View(pagamento);
  }

    // GET: Pagamento/Delete/5
    public async Task<IActionResult> Delete(int? id)
  {
    if (id == null)
    {
      return NotFound();
    }

    var pagamento = await _context.Pagamento
        .Include(p => p.Cliente)
        .FirstOrDefaultAsync(m => m.IdPagto == id);
    if (pagamento == null)
    {
      return NotFound();
    }

    return View(pagamento);
  }

  // POST: Pagamento/Delete/5
  [HttpPost, ActionName("Delete")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> DeleteConfirmed(int id)
  {
    var pagamento = await _context.Pagamento.FindAsync(id);
    _context.Pagamento.Remove(pagamento);
    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
  }

  private bool PagamentoExists(int id)
  {
    return _context.Pagamento.Any(e => e.IdPagto == id);
  }

    public async Task<IActionResult> PagamentoPDF()
    {
      // Define la URL de la Cabecera 
      string _headerUrl = Url.Action("PagamentoHeaderPDF", "Pagamento", null, "http");
      // Define la URL del Pie de página
      string _footerUrl = Url.Action("PagamentoFooterPDF", "Pagamento", null, "http");

      return new ViewAsPdf("PagamentoPDF", await _context.Pagamento.ToListAsync())
      {
        // Establece la Cabecera y el Pie de página
        CustomSwitches = "--header-html " + _headerUrl + " --header-spacing 13 " +
                         "--footer-html " + _footerUrl + " --footer-spacing 0",
        PageMargins = new Margins(50, 10, 12, 10),
        FileName = "Rel_Pagamento.pdf",
        PageSize = Size.A4,
        MinimumFontSize = 19,
        PageOrientation = Orientation.Portrait,
        IsGrayScale = false

      };
    }

    public IActionResult PagamentoHeaderPDF()
    {
      return View("PagamentoHeaderPDF");
    }

    public IActionResult PagamentoFooterPDF()
    {
      return View("PagamentoFooterPDF");
    }

  }
}
