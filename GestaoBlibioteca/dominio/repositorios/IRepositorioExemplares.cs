//IRepositorioExemplares

using Biblioteca.Dominio.Modelos;

namespace Biblioteca.Dominio.Repositorios;

/// <summary>
/// Interface para operações de persistência e consulta da entidade <see cref="Exemplar"/>.
/// </summary>
/// <remarks>
/// Responsável por interações com o armazenamento de exemplares, como criação, atualização e consultas.
/// Esta interface é utilizada principalmente por serviços de aplicação que gerenciam a alocação de exemplares
/// e seu modo de leitura.
/// </remarks>
public interface IRepositorioExemplares
{
    /// <summary>
    /// Adiciona um novo exemplar ao repositório.
    /// </summary>
    /// <param name="exemplar">Instância de <see cref="Exemplar"/> a ser persistida.</param>
    Exemplar Adicionar(Exemplar exemplar);

    /// <summary>
    /// Retorna um exemplar disponível para empréstimo de um determinado livro.
    /// </summary>
    /// <param name="livroId">ID do livro do qual se deseja um exemplar disponível.</param>
    /// <returns>Exemplar disponível ou null se nenhum estiver disponível.</returns>
    Exemplar? ObterDisponivelParaLivro(long livroId);

    /// <summary>
    /// Retorna o exemplar com o ID fornecido.
    /// </summary>
    /// <param name="id">Identificador do exemplar.</param>
    /// <returns>Exemplar correspondente ou null se não encontrado.</returns>
    Exemplar? ObterPorId(long id);

    /// <summary>
    /// Atualiza o modo de leitura de um exemplar.
    /// </summary>
    /// <param name="exemplarId">ID do exemplar a ser atualizado.</param>
    /// <param name="leituraLocal">Novo valor do modo de leitura.</param>
    void AtualizarModoDeLeitura(long exemplarId, bool leituraLocal);
}
