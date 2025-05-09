//IRepositorioUsuarios

using Biblioteca.Dominio.Modelos;

namespace Biblioteca.Dominio.Repositorios;

/// <summary>
/// Interface para operações de persistência e consulta da entidade <see cref="Usuario"/>.
/// </summary>
/// <remarks>
/// Esta interface define os métodos necessários para lidar com usuários do sistema,
/// incluindo seu cadastro, atualização de contato, e mecanismos de busca.
/// </remarks>
public interface IRepositorioUsuarios
{
    /// <summary>
    /// Adiciona um novo usuário ao repositório.
    /// </summary>
    /// <param name="usuario">Instância de <see cref="Usuario"/> a ser persistida.</param>
    Usuario Adicionar(Usuario usuario);

    /// <summary>
    /// Lista todos os usuários cadastrados, até um limite opcional.
    /// </summary>
    /// <param name="limite">Número máximo de usuários a serem retornados.</param>
    /// <returns>Lista de usuários.</returns>
    List<Usuario> ListarTodos(int limite);

    /// <summary>
    /// Obtém um usuário pelo nome de usuário.
    /// </summary>
    /// <param name="nomeUsuario">Nome de usuário a ser buscado.</param>
    /// <returns><see cref="Usuario"/> correspondente ou null se não encontrado.</returns>
    Usuario? ObterPorNomeUsuario(string nomeUsuario);

    /// <summary>
    /// Obtém um usuário pelo seu identificador único.
    /// </summary>
    /// <param name="id">ID do usuário.</param>
    /// <returns><see cref="Usuario"/> correspondente ou null se não encontrado.</returns>
    Usuario? ObterPorId(long id);

    /// <summary>
    /// Atualiza os dados de contato de um usuário.
    /// </summary>
    /// <param name="usuarioId">ID do usuário.</param>
    /// <param name="telefone">Novo telefone.</param>
    /// <param name="email">Novo email.</param>
    void AtualizarContato(long usuarioId, string telefone, string email);
}
