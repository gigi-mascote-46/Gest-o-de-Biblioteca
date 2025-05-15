//AppServiceEmprestimos

using Biblioteca.Dominio.Modelos;
using Biblioteca.Dominio.Repositorios;
using Biblioteca.Dominio.Servicos;
using Biblioteca.Dominio;
using Biblioteca.Infraestrutura.Persistencia.Repositorios;
using GestaoBlibioteca.Dominio.Excecoes;

namespace Biblioteca.Aplicacao.Servicos;

/// <summary>
/// Application Service responsável por coordenar os casos de uso relacionados a empréstimos de exemplares.
/// Atua como orquestrador entre repositórios, domain services e unidade de trabalho, sem conter lógica de negócio.
/// </summary>
public class AppServiceEmprestimos
{
    private readonly IRepositorioLivros _repositorioLivros;
    private readonly IRepositorioUsuarios _repositorioUsuarios;
    private readonly IRepositorioExemplares _repositorioExemplares;
    private readonly IRepositorioEmprestimos _repositorioEmprestimos;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
    private readonly GestorDeEmprestimos _gestorDeEmprestimos;

    public AppServiceEmprestimos(
        IRepositorioLivros repositorioLivros,
        IRepositorioUsuarios repositorioUsuarios,
        IRepositorioExemplares repositorioExemplares,
        IRepositorioEmprestimos repositorioEmprestimos,
        IUnidadeDeTrabalho unidadeDeTrabalho,
        GestorDeEmprestimos gestorDeEmprestimos)
    {
        _repositorioLivros = repositorioLivros;
        _repositorioUsuarios = repositorioUsuarios;
        _repositorioExemplares = repositorioExemplares;
        _repositorioEmprestimos = repositorioEmprestimos;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _gestorDeEmprestimos = gestorDeEmprestimos;
    }

    /// <summary>
    /// Realiza um novo empréstimo de um exemplar de um livro para um usuário.
    /// </summary>
    /// <param name="livroId">ID do livro desejado.</param>
    /// <param name="usuarioId">ID do usuário solicitante.</param>
    /// <returns>Instância de empréstimo criado.</returns>
    /// <exception cref="LivroNaoEncontradoException"></exception>
    /// <exception cref="UsuarioNaoEncontradoException"></exception>
    /// <exception cref="ExemplarIndisponivelException"></exception>
    /// <exception cref="UsuarioAtingiuLimiteEmprestimosException"></exception>
    /// <exception cref="ExemplarSomenteLeituraLocalException"></exception>
    public Emprestimo RealizarEmprestimo(long livroId, long usuarioId)
    {
        return _unidadeDeTrabalho.ExecutarComTransacao(() =>
        {

            var livro = _repositorioLivros.ObterPorId(livroId)
                    ?? throw new LivroNaoEncontradoException();

        var usuario = _repositorioUsuarios.ObterPorId(usuarioId)
                      ?? throw new UsuarioNaoEncontradoException();

        var exemplar = _repositorioExemplares.ObterDisponivelParaLivro(livroId)
                       ?? throw new ExemplarIndisponivelException();

        var dataEmprestimo = DateTime.UtcNow;

        var emprestimo = _gestorDeEmprestimos.Criar(exemplar, usuario, dataEmprestimo);

        return _repositorioEmprestimos.Adicionar(emprestimo);
        });

    }

    /// <summary>
    /// Registra a devolução de um empréstimo existente.
    /// </summary>
    /// <param name="emprestimoId">ID do empréstimo.</param>
    /// <returns>Instância de empréstimo com devolução registrada.</returns>
    /// <exception cref="EmprestimoNaoEncontradoException"></exception>
    /// <exception cref="EmprestimoJaDevolvidoException"></exception>
    public Emprestimo RegistrarDevolucao(long emprestimoId)
    {
        return _unidadeDeTrabalho.ExecutarComTransacao(() =>
        {

            var emprestimo = _repositorioEmprestimos.ObterPorId(emprestimoId)
                          ?? throw new EmprestimoNaoEncontradoException();

        var atualizado = _gestorDeEmprestimos.MarcarComoDevolvido(emprestimo);

        _repositorioEmprestimos.RegistrarDevolucao(atualizado.Id, (DateTime)atualizado.DataDevolucao);
            return atualizado;
        });
    }
    /// <summary>
    /// Lista os empréstimos associados a um usuário específico.
    /// </summary>
    /// <param name="usuarioId">ID do usuário.</param>
    /// <returns>Lista de empréstimos do usuário</returns>
    public List<Emprestimo> ListarPorUsuario(long usuarioId, int limite=10)
    {
       return _repositorioEmprestimos.ListarPorUsuario(usuarioId, limite);
    }

    /// <summary>
    /// Lista todos os empréstimos registrados no sistema.
    /// </summary>
    /// <returns>Lista de todos os empréstimos.</returns>
    public List<Emprestimo> ListarTodos(int limite = 10)
    {
        return _repositorioEmprestimos.ListarTodos(limite);
    }
}
