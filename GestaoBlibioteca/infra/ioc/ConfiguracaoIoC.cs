//ConfiguracaoIoC

using System.Data;
using Biblioteca.Aplicacao.Servicos;
using Biblioteca.Dominio.Servicos;
using Biblioteca.Dominio.Repositorios;
using Biblioteca.Dominio;
using Biblioteca.Infraestrutura.Persistencia;
using Biblioteca.UI.Telas;
using Biblioteca.UI;
using Infra;
using Biblioteca.Infra.Persistencia;
using Biblioteca.Infraestrutura.Persistencia.Repositorios;
using UI;

namespace Biblioteca.Infraestrutura.IoC;

/// <summary>
/// Responsável por configurar manualmente as dependências da aplicação.
/// Fornece instâncias prontas de todos os serviços, repositórios e interfaces.
/// </summary>
public static class ConfiguracaoIoC
{
    /// <summary>
    /// Inicializa todas as dependências e retorna os controladores de UI principais.
    /// </summary>
    public static UIController Inicializar()
    {
        // Registro de type handlers do Dapper
        RegistradorDeTypeHandlers.RegistrarTodos();

        // Conexão única compartilhada (já aberta)
        var fabricaConexao = new FabricaDeConexaoSQLite("biblioteca.db");
        IDbConnection conexao = fabricaConexao.CriarConexao();

        // Inicialização das tabelas do banco
        DbInitializer.ExecutarMigracoes(conexao);

        // Unidade de trabalho
        var unidadeDeTrabalho = new UnidadeDeTrabalhoSQLite(conexao);

        // Repositórios
        var repoUsuarios = new RepositorioUsuariosSQLite(conexao);
        var repoLivros = new RepositorioLivrosSQLite(conexao);
        var repoExemplares = new RepositorioExemplaresSQLite(conexao);
        var repoEmprestimos = new RepositorioEmprestimosSQLite(conexao);

        // Serviços de domínio
        var cadastroDeUsuarios = new CadastroDeUsuarios();
        var cadastroDeLivros = new CadastroDeLivros();
        var cadastroDeExemplares = new CadastroDeExemplares();
        var gestorDeEmprestimos = new GestorDeEmprestimos();

        // Application Services
        var appServiceUsuarios = new AppServiceUsuarios(repoUsuarios, unidadeDeTrabalho, cadastroDeUsuarios);
        var appServiceLivros = new AppServiceLivros(repoLivros, unidadeDeTrabalho, cadastroDeLivros);
        var appServiceExemplares = new AppServiceExemplares(repoExemplares, repoLivros, unidadeDeTrabalho, cadastroDeExemplares);
        var appServiceEmprestimos = new AppServiceEmprestimos(repoLivros, repoUsuarios, repoExemplares, repoEmprestimos, unidadeDeTrabalho, gestorDeEmprestimos);

        // Sessão
        var sessao = new Sessao();

        // Interfaces de usuário (UI)
        var loginUI = new LoginUI(appServiceUsuarios, sessao);
        var usuarioUI = new UsuarioUI(appServiceUsuarios);
        var livroUI = new LivroUI(appServiceLivros);
        var exemplarUI = new ExemplarUI(appServiceExemplares);
        var emprestimoUI = new EmprestimoUI(appServiceEmprestimos, sessao);

        // Menu principal
        var menuPrincipalUI = new MenuPrincipalUI( livroUI, usuarioUI, exemplarUI, emprestimoUI);

        // Controlador principal
        var uiController = new UIController(loginUI, menuPrincipalUI, sessao);

        // Devolve o ponto de entrada para Program.cs
        return uiController;
    }
}
