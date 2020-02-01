using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ConsoleApi
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
                //client.BaseAddress = new System.Uri("http://localhost:49858/");
                client.BaseAddress = new System.Uri("http://www.utyum.somee.com/Grupo/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/Pagto/3");
                if (response.IsSuccessStatusCode)
                {  //GET
                    Pagamento pagamento = await response.Content.ReadAsAsync<Pagamento>();
                    Console.WriteLine("{0}\t{1}\t{2}\tR${3}\tR${4}\t{5}", pagamento.IdCli, pagamento.Cpf, pagamento.Data, pagamento.Valor, pagamento.Total, pagamento.Situacao);
                    Console.WriteLine("Pagamento acessado e exibido.  Tecle algo para incluir um novo Pagamento.");
                    Console.ReadKey();
                }
                //POST
                var cha = new Pagamento() { IdCli = 3, Cpf = "123.098.123-08", Contrato = "00003", Parcela = 2, Valor = 500.12M, Data = Convert.ToDateTime("2019-01-31"), Situacao = "Devedor" };
                response = await client.PostAsJsonAsync("api/Pagto", cha);
                Console.WriteLine("Pagamento incluído. Tecle algo para atualizar o preço do Pagamento.");
                Console.ReadKey();

                if (response.IsSuccessStatusCode)
                {   //PUT
                    Uri chaUrl = response.Headers.Location;
                    cha.Total = "771,15";   // atualiza o preco do produto
                    response = await client.PutAsJsonAsync(chaUrl, cha);
                    Console.WriteLine("Pagamento Valor Total do atualizado para. R$ " + cha.Total + " Tecle algo para excluir o Pagamento");
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
