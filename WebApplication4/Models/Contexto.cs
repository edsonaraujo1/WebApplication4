using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace WebApplication4.Models
{
  public class Contexto : IDisposable
  {

    public readonly SqlConnection minhaConexao;

    public Contexto()
    {
      minhaConexao = new SqlConnection("Data Source=den1.mssql8.gear.host; Initial Catalog = grupoaval; Persist Security Info = True; Uid = grupoaval; Password = Yk8Pr9q_8Zs_; MultipleActiveResultSets = true; Min Pool Size=0; Max Pool Size=250; Connect Timeout=1800;");
      try
      {
        minhaConexao.Open();
      }
      catch (Exception ex)
      {
        throw ex;

      }
    }

    public void ExecutaComando(string strQuery)
    {
      var cmdComando = new SqlCommand
      {
        CommandText = strQuery,
        CommandType = CommandType.Text,
        Connection = minhaConexao
      };
      try
      {
        cmdComando.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        cmdComando.Dispose();
      }
    }

    public SqlDataReader ExecutaComandoComRetorno(string strQuery)
    {
      using (var cmdComando = new SqlCommand(strQuery, minhaConexao))
      {
        try
        {
          return cmdComando.ExecuteReader();
        }
        catch (Exception ex)
        {
          throw ex;
        }
        finally
        {
          cmdComando.Dispose();
        }

      }
    }

    // Método - Fechar Conexao
    public void FecharConexao()
    {
      minhaConexao.Close();
    }

    public void Dispose()
    {
      if (minhaConexao != null && minhaConexao.State == ConnectionState.Open)
      {
        minhaConexao.Close();
      }
      if (minhaConexao != null)
      {
        minhaConexao.Dispose();
      }
      GC.SuppressFinalize(this);
    }

    public void Open()
    {
      if (minhaConexao.State == ConnectionState.Closed)
      {
        minhaConexao.Open();
      }
    }

    public void Close()
    {
      if (minhaConexao.State == ConnectionState.Open)
      {
        minhaConexao.Close();
      }
    }

    public Exception UltimaException { get; set; }

    public string Msg { get; set; }

    public string EVENT_LOG_NAME { get; set; }

    [SerializableAttribute]
    [ComVisibleAttribute(true)]
    public class NullReferenceException : SystemException
    {

    }

  }
}
