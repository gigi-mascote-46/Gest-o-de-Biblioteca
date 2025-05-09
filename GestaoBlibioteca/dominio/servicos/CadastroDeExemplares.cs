//CadastroDeExemplares

using Biblioteca.Dominio.Modelos;

namespace Biblioteca.Dominio.Servicos;

/// <summary>
/// Serviço de domínio responsável por operações relacionadas à criação e atualização de exemplares de livros.
/// Garante a consistência e as regras de negócio específicas da entidade <see cref="Exemplar"/>.
/// </summary>
public class CadastroDeExemplares
{
    /// <summary>
    /// Cria um novo exemplar vinculado a um livro existente.
    /// </summary>
    /// <param name="livro">Livro ao qual o exemplar estará vinculado. Não pode ser nulo.</param>
    /// <param name="somenteLeituraLocal">Define se o exemplar será restrito à leitura local.</param>
    /// <returns>Uma nova instância de <see cref="Exemplar"/>.</returns>
    /// <exception cref="ArgumentNullException">Lançada caso o livro seja nulo.</exception>
    public Exemplar Criar(Livro livro, bool somenteLeituraLocal)
    {
        if (livro == null)
            throw new ArgumentNullException(nameof(livro), "Livro não pode ser nulo ao criar um exemplar.");

        var exemplar = new Exemplar(
            id: 0, // O ID será atribuído posteriormente pela camada de persistência
            livro: livro,
            somenteLeituraLocal: somenteLeituraLocal
        );

        return exemplar;
    }

    /// <summary>
    /// Atualiza o modo de leitura de um exemplar, criando uma nova instância com os dados atualizados, caso haja mudança no valor atual.
    /// </summary>
    /// <param name="exemplar">O exemplar a ser atualizado.</param>
    /// <param name="leituraLocal">Novo valor para o modo de leitura local.</param>
    /// <returns>Uma nova instância de <see cref="Exemplar"/> com o modo de leitura atualizado.</returns>
    /// <exception cref="ArgumentNullException">Lançada caso o exemplar seja nulo.</exception>
    public Exemplar AtualizarModoDeLeitura(Exemplar exemplar, bool leituraLocal)
    {
        if (exemplar == null)
            throw new ArgumentNullException(nameof(exemplar), "Exemplar não pode ser nulo.");

        // Se o modo de leitura não mudou, retorna o mesmo exemplar (não há mudança de estado)
        if (exemplar.SomenteLeituraLocal== leituraLocal)
            return exemplar;

        // Cria uma nova instância com o mesmo ID e Livro, mas novo valor para leituraLocal
        return new Exemplar(
            id: exemplar.Id,
            livro: exemplar.Livro,
            somenteLeituraLocal: leituraLocal
        );
    }
}

