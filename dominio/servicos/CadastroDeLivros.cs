//CadastroDeLivros

using Biblioteca.Dominio.Modelos;

namespace Biblioteca.Dominio.Servicos;

/// <summary>
/// Serviço de domínio responsável por encapsular a lógica de criação de um novo livro.
/// Essa classe garante que nenhum livro seja criado com dados inválidos, mantendo a integridade do domínio.
/// </summary>
public class CadastroDeLivros
{
    /// <summary>
    /// Cria uma nova instância de <see cref="Livro"/>, validando os parâmetros de entrada conforme regras de negócio.
    /// </summary>
    /// <param name="titulo">Título do livro. Não pode ser nulo ou vazio.</param>
    /// <param name="autor">Nome do autor. Não pode ser nulo ou vazio.</param>
    /// <param name="isbn">Código ISBN do livro. Não pode ser nulo ou vazio. Deve ter 10 ou 13 dígitos.</param>
    /// <param name="anoPublicacao">Ano de publicação do livro. Deve estar entre 1500 e o ano atual.</param>
    /// <returns>Uma nova instância de <see cref="Livro"/> com estado válido.</returns>
    /// <exception cref="ArgumentException">Lançada caso qualquer parâmetro esteja inválido.</exception>
    public Livro Criar(string titulo, string autor, string isbn, int anoPublicacao)
    {
        // As validações são aplicadas no próprio construtor de Livro.
        // O serviço garante apenas a organização e aplicação das regras de negócio
        // em um ponto único, favorecendo reuso e consistência.

        var livro = new Livro(
            id: 0, // O ID será atribuído posteriormente pelo repositório ou pela camada de persistência
            titulo: titulo,
            autor: autor,
            isbn: isbn,
            anoPublicacao: anoPublicacao
        );

        return livro;
    }
}
