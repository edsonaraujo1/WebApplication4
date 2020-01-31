using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebApplication4.Models;

namespace Calculo
{
    class Program
    {
    static void Main(string[] args)
    {
      RunAsync().Wait();
      Console.ReadKey();
    }
    static async Task RunAsync()
    {

      using (var client = new HttpClient())
      {
        client.BaseAddress = new System.Uri("http://localhost:53557/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage response = await client.GetAsync("api/produtos/3");
        if (response.IsSuccessStatusCode)
        {  //GET
          Pagamento pagamento = await response.Content.ReadAsAsync<Pagamento>();
          Console.WriteLine("{0}\tR${1}\t{2}", pagamento.IdCli, pagamento.Valor, pagamento.Total);
          Console.WriteLine("pagamento acessado e exibido.  Tecle algo para incluir um novo pagamento.");
          Console.ReadKey();
        }
        //POST
        var cha = new Pagamento() { IdCli = 2, Valor = 1.50M, Situacao = "Em Aberto" };
        response = await client.PostAsJsonAsync("api/produtos", cha);
        Console.WriteLine("Pagamento cliente 2 incluído. Tecle algo para atualizar Valor do Pagamento.");
        Console.ReadKey();

        if (response.IsSuccessStatusCode)
        {   //PUT
          Uri chaUrl = response.Headers.Location;
          cha.Valor = 2.55M;   // atualiza o preco do produto
          response = await client.PutAsJsonAsync(chaUrl, cha);
          Console.WriteLine("Pagamento valor atualizado. Tecle algo para excluir o Pagamento");
          Console.ReadKey();
          //DELETE
          response = await client.DeleteAsync(chaUrl);
          Console.WriteLine("Pagamento deletado");
          Console.ReadKey();
        }
      }
    }
  }
}
