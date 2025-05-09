//AppServiceExemplares

using Biblioteca.Dominio.Modelos;
using Biblioteca.Dominio.Repositorios;
using Biblioteca.Dominio.Servicos;
using Biblioteca.Dominio;
using GestaoBlibioteca.Dominio.Excecoes;


namespace Biblioteca.Aplicacao.Servicos;

/// <summary>
/// Application Service responsável por coordenar casos de uso relacionados a <see cref="Exemplar"/>.
/// </summary>
/// <remarks>
/// Este serviço atua como orquestrador de operações envolvendo exemplares de livros,
/// delegando a criação e atualização para os serviços de domínio apropriados.
/// </remarks>
public class AppServiceExemplares
{
    private readonly IRepositorioExemplares _repositorioExemplares;
    private readonly IRepositorioLivros _repositorioLivros;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
    private readonly CadastroDeExemplares _cadastroDeExemplares;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="AppServiceExemplares"/>.
    /// </summary>
    public AppServiceExemplares(
        IRepositorioExemplares repositorioExemplares,
        IRepositorioLivros repositorioLivros,
        IUnidadeDeTrabalho unidadeDeTrabalho,
        CadastroDeExemplares cadastroDeExemplares)
    {
        _repositorioExemplares = repositorioExemplares;
        _repositorioLivros = repositorioLivros;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _cadastroDeExemplares = cadastroDeExemplares;
    }

    /// <summary>
    /// Cadastra um novo exemplar de um livro.
    /// </summary>
    /// <param name="livroId">ID do livro ao qual o exemplar pertence.</param>
    /// <param name="somenteLeituraLocal">Indica se o exemplar pode ser emprestado ou apenas lido localmente.</param>
    /// <returns>Instância do <see cref="Exemplar"/> cadastrado.</returns>
    /// <exception cref="LivroNaoEncontradoException">Lançada quando o livro informado não for encontrado.</exception>
    public Exemplar Cadastrar(long livroId, bool somenteLeituraLocal)
    {
        return _unidadeDeTrabalho.ExecutarComTransacao(() =>
        {

            var livro = _repositorioLivros.ObterPorId(livroId);
        if (livro is null)
        {
            throw new LivroNaoEncontradoException();
        }

        var exemplar = _cadastroDeExemplares.Criar(livro, somenteLeituraLocal);
        return _repositorioExemplares.Adicionar(exemplar);
        });

    }

    /// <summary>
    /// Atualiza o modo de leitura de um exemplar.
    /// </summary>
    /// <param name="exemplarId">ID do exemplar a ser atualizado.</param>
    /// <param name="somenteLeituraLocal">Novo valor para o modo de leitura.</param>
    /// <returns>Instância atualizada do <see cref="Exemplar"/>.</returns>
    /// <exception cref="ExemplarNaoEncontradoException">Lançada quando o exemplar informado não for encontrado.</exception>
    public Exemplar AtualizarModoDeLeitura(long exemplarId, bool somenteLeituraLocal)
    {
        return _unidadeDeTrabalho.ExecutarComTransacao(() =>
        {

            var exemplar = _repositorioExemplares.ObterPorId(exemplarId);
        if (exemplar is null)
        {
            throw new ExemplarNaoEncontradoException();
        }

        var exemplarAtualizado = _cadastroDeExemplares.AtualizarModoDeLeitura(exemplar, somenteLeituraLocal);
        _repositorioExemplares.AtualizarModoDeLeitura(exemplarId, somenteLeituraLocal);

        return exemplarAtualizado;
    });
    }

    /// <summary>
    /// Retorna o exemplar disponível para um livro específico.
    /// </summary>
    /// <param name="livroId">ID do livro desejado.</param>
    /// <returns>Um exemplar disponível, se existir.</returns>
//    public Exemplar? ObterDisponivelParaLivro(int livroId)
//    {
//        return _repositorioExemplares.ObterDisponivelParaLivro(livroId);
//    }

    /// <summary>
    /// Recupera um exemplar pelo seu ID.
    /// </summary>
//    public Exemplar? ObterPorId(int exemplarId)
//    {
//        return _repositorioExemplares.ObterPorId(exemplarId);
//    }
}
