# WebApplication4
Aplicativo Teste Técnico Grupo Aval

Tecnologias Envolvidas para o Projeto:<br>
Sistema Windows 10 Home Single Language<br> 
SQL Server 2017 Communit Express<br> 
Visual Studio 2019 Communit<br>  
Bootstrap.Datepicker  Version= 1.8.0.1<br> 
DinkToPdf  Version= 1.0.8<br> 
Microsoft.AspNetCore.App<br> 
Microsoft.AspNetCore.Mvc.Core Version= 2.2.5<br> 
Microsoft.AspNetCore.Razor.Design Version= 2.2.0<br>  
Microsoft.VisualStudio.Web.CodeGen.Design Version= 2.2.4<br> 
Rotativa.AspNetCore Version= 1.1.1<br> 
Vercionamento GitHub na Azure<br> 
Servidor IIS Versão 10 Host .Net Core 3.1.0<br>  
Versao .Net Core 3.0.100<br> 
Infra Desktop Core i7 4.80Ghz 16Gb Ram SSD 480Gb<br> 
<br> 
<b>Banco de Dados:</b><br><br> 
CREATE TABLE [dbo].[Cliente](<br>
[IdCli] [int] IDENTITY(1,1) NOT NULL, <br>
[Nome] [nvarchar](max) NULL,<br>
[Cpf] [nvarchar](max) NULL,<br>
[Contrato] [nvarchar](max) NULL,<br>
[Data] [datetime2](7) NOT NULL, <br>
[Valor_principal] [decimal](18, 2) NOT NULL, <br>
[Valor_atualizado] [decimal](18, 2) NOT NULL, <br>
CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED <br>
(  [IdCli] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] 
<br>
<br>
CREATE TABLE [dbo].[Debito]( <br>
[IdDeb] [int] IDENTITY(1,1) NOT NULL,<br>
[Vencimento] [datetime2](7) NOT NULL,<br>
[Valor_inicial] [decimal](18, 2) NOT NULL,<br>
[Valor_final] [decimal](18, 2) NOT NULL,<br>
[Juros] [decimal](18, 2) NOT NULL,<br>
[Multa] [decimal](18, 2) NOT NULL,<br>
[Dias] [int] NOT NULL,<br>
CONSTRAINT [PK_Debito] PRIMARY KEY CLUSTERED  (<br>
[IdDeb] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] 
<br>
<br>
CREATE TABLE [dbo].[Pagamento](<br>
[IdPagto] [int] IDENTITY(1,1) NOT NULL,<br>
[IdCli] [int] NOT NULL,<br>
[Cpf] [nvarchar](max) NULL,<br>
[Contrato] [nvarchar](max) NULL,<br>
[Parcela] [int] NOT NULL,<br>
[Data] [datetime2](7) NOT NULL,<br>
[Valor] [decimal](18, 2) NOT NULL,<br>
[Total] [varchar](50) NULL,<br>
[Situacao] [nvarchar](max) NULL,<br>
CONSTRAINT [PK_Pagamento] PRIMARY KEY CLUSTERED  (<br>
[IdPagto] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] 
<br> 
<br>
<br>

<b>Link para Baixar Projeto: </b> https://github.com/edsonaraujo1/WebApplication4/archive/master.zip 
