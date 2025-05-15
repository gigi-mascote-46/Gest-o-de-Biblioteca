//IRepositorioLivros

using Biblioteca.Dominio.Modelos;
using GestaoBlibioteca.dominio.modelos;

namespace Biblioteca.Dominio.Repositorios;

/// <summary>
/// Interface para operações de persistência e consulta da entidade <see cref="Livro"/>.
/// </summary>
/// <remarks>
/// Essa interface define métodos para adicionar livros ao sistema e consultá-los com base
/// em diferentes critérios. A implementação concreta pode usar qualquer mecanismo de persistência,
/// como um ORM (Entity Framework, Dapper) ou armazenamento em memória.
/// </remarks>
public interface IRepositorioLivros
{
    /// <summary>
    /// Adiciona um novo livro ao repositório.
    /// </summary>
    /// <param name="livro">Instância do livro a ser persistida.</param>
    Livro Adicionar(Livro livro);

    /// <summary>
    /// Retorna todos os livros cadastrados, com um limite opcional de resultados.
    /// </summary>
    /// <param name="limite">Número máximo de livros a retornar.</param>
    /// <returns>Lista de livros encontrados.</returns>
    List<Livro> ListarTodos(int limite);

    /// <summary>
    /// Busca livros cujo título contenha o texto fornecido.
    /// </summary>
    /// <param name="titulo">Texto parcial ou completo do título.</param>
    /// <param name="limite">Número máximo de livros a retornar.</param>
    /// <returns>Lista de livros com títulos correspondentes.</returns>
    List<Livro> BuscarPorTitulo(string titulo, int limite);

    /// <summary>
    /// Busca livros cujo nome do autor contenha o texto fornecido.
    /// </summary>
    /// <param name="autor">Texto parcial ou completo do nome do autor.</param>
    /// <param name="limite">Número máximo de livros a retornar.</param>
    /// <returns>Lista de livros com autores correspondentes.</returns>
    List<Livro> BuscarPorAutor(string autor, int limite);

    /// <summary>
    /// Busca um livro com o ISBN exato fornecido.
    /// </summary>
    /// <param name="isbn">ISBN do livro.</param>
    /// <returns>Livro correspondente ou null se não encontrado.</returns>
    Livro? ObterPorISBN(string isbn);

    /// <summary>
    /// Busca um livro pelo seu identificador único.
    /// </summary>
    /// <param name="id">Identificador do livro.</param>
    /// <returns>Livro correspondente ou null se não encontrado.</returns>
    Livro? ObterPorId(long id);

    /// <summary>
    /// Retorna informação sobre a disponibilidade de livros.
    /// </summary>
    List<Disponibilidade> ListarDisponibilidade(int limite);

}
