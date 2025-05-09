// AppServiceLivros

using Biblioteca.Dominio;
using Biblioteca.Dominio.Modelos;
using Biblioteca.Dominio.Repositorios;
using Biblioteca.Dominio.Servicos;
using GestaoBlibioteca.dominio.modelos;

namespace Biblioteca.Aplicacao.Servicos;

/// <summary>
/// Application Service responsável por coordenar casos de uso relacionados a <see cref="Livro"/>.
/// </summary>
/// <remarks>
/// Este serviço atua como orquestrador de operações envolvendo livros, intermediando
/// chamadas entre a camada de apresentação e os serviços e repositórios de domínio.
/// </remarks>
public class AppServiceLivros
{
    private readonly IRepositorioLivros _repositorioLivros;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
    private readonly CadastroDeLivros _cadastroDeLivros;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="AppServiceLivros"/>.
    /// </summary>
    public AppServiceLivros(
        IRepositorioLivros repositorioLivros,
        IUnidadeDeTrabalho unidadeDeTrabalho,
        CadastroDeLivros cadastroDeLivros)
    {
        _repositorioLivros = repositorioLivros;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _cadastroDeLivros = cadastroDeLivros;
    }

    /// <summary>
    /// Cadastra um novo livro no sistema.
    /// </summary>
    /// <returns>Instância do <see cref="Livro"/> cadastrado.</returns>
    public Livro Cadastrar(string titulo, string autor, string isbn, int anoPublicacao)
    {
        return _unidadeDeTrabalho.ExecutarComTransacao(() =>
        {
            var livro = _cadastroDeLivros.Criar(titulo, autor, isbn, anoPublicacao);
           return _repositorioLivros.Adicionar(livro);
        });
    }

    /// <summary>
    /// Lista todos os livros cadastrados, respeitando um limite.
    /// </summary>
    public List<Disponibilidade> ListarTodos(int limite = 10)
    {
        //return _repositorioLivros.ListarTodos(limite);
        return _repositorioLivros.ListarDisponibilidade(limite);
    }

    /// <summary>
    /// Busca livros que contenham parte ou totalidade do título especificado.
    /// </summary>
    public List<Livro> BuscarPorTitulo(string titulo, int limite = 10)
    {
        return _repositorioLivros.BuscarPorTitulo(titulo, limite);
    }

    /// <summary>
    /// Busca livros por nome do autor.
    /// </summary>
    public List<Livro> BuscarPorAutor(string autor, int limite = 10)
    {
        return _repositorioLivros.BuscarPorAutor(autor, limite);
    }

    /// <summary>
    /// Busca um livro pelo seu código ISBN.
    /// </summary>
    public Livro? BuscarPorISBN(string isbn)
    {
        return _repositorioLivros.ObterPorISBN(isbn);
    }

    /// <summary>
    /// Busca um livro por ID.
    /// </summary>
//    public Livro? ObterPorId(int id)
//    {
//        return _repositorioLivros.ObterPorId(id);
//    }
}
