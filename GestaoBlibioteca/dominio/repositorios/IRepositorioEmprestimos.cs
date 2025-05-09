//IRepositorioEmprestimos

using Biblioteca.Dominio.Modelos;
using GestaoBlibioteca.dominio.modelos;

namespace Biblioteca.Dominio.Repositorios;

/// <summary>
/// Interface para operações de persistência e consulta da entidade <see cref="Emprestimo"/>.
/// </summary>
/// <remarks>
/// Define métodos para lidar com empréstimos de exemplares,
/// incluindo registro, consulta e devolução de livros.
/// </remarks>
public interface IRepositorioEmprestimos
{
    /// <summary>
    /// Adiciona um novo empréstimo ao repositório.
    /// </summary>
    /// <param name="emprestimo">Instância de <see cref="Emprestimo"/> a ser persistida.</param>
    Emprestimo Adicionar(Emprestimo emprestimo);

    /// <summary>
    /// Obtém um empréstimo pelo seu identificador único.
    /// </summary>
    /// <param name="emprestimoId">ID do empréstimo.</param>
    /// <returns><see cref="Emprestimo"/> correspondente ou null se não encontrado.</returns>
    Emprestimo? ObterPorId(long emprestimoId);

    /// <summary>
    /// Lista os empréstimos associados a um usuário, com um limite opcional.
    /// </summary>
    /// <param name="usuarioId">ID do usuário.</param>
    /// <param name="limite">Número máximo de empréstimos a serem retornados.</param>
    /// <returns>Lista de empréstimos do usuário.</returns>
    List<Emprestimo> ListarPorUsuario(long usuarioId, int limite);

    /// <summary>
    /// Registra a devolução de um empréstimo.
    /// </summary>
    /// <param name="emprestimoId">ID do empréstimo a ser marcado como devolvido.</param>
    /// <param name="dataDevolucao">Data e hora da devolução.</param>
    void RegistrarDevolucao(long emprestimoId, DateTime dataDevolucao);

    /// <summary>
    /// Lista todos os empréstimos registrados, com um limite opcional.
    /// </summary>
    /// <param name="limite">Número máximo de empréstimos a serem retornados.</param>
    /// <returns>Lista de todos os empréstimos.</returns>
    List<Emprestimo> ListarTodos(int limite);
}
