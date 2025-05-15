//RepositorioExemplaresSQLite

using System.Data;
using Biblioteca.Dominio.Modelos;
using Biblioteca.Dominio.Repositorios;
using Biblioteca.UI.Telas;
using Dapper;
using GestaoBlibioteca.infra.persistencia.modelos;
using UI;
namespace Biblioteca.Infra.Persistencia;

/// <summary>
/// Implementação de <see cref="IRepositorioExemplares"/> utilizando SQLite e Dapper.
/// Responsável por persistir e recuperar informações sobre exemplares de livros.
///
/// Assumimos que a verificação de disponibilidade do exemplar (se está emprestado ou não)
/// é feita em nível de domínio, não na camada de persistência.
/// </summary>
public class RepositorioExemplaresSQLite : IRepositorioExemplares
{
    private readonly IDbConnection _conexao;

    public RepositorioExemplaresSQLite(IDbConnection conexao)
    {
        _conexao = conexao;
    }

    // Código já corrigido.
    public Exemplar? ObterPorId(long id)
    {
        const string sql = @"
            SELECT
                e.id, e.livro_id as livroId, e.somente_leitura_local as somenteLeituraLocal,
                l.id, l.titulo, l.autor, l.isbn, l.ano_publicacao as anoPublicacao
            FROM exemplares e
            JOIN livros l ON l.id = e.livro_id
            WHERE e.id = @Id
        ";

        return _conexao.Query<ExemplarDb, LivroDb, Exemplar>(
            sql,
            (exemplarDb, livroDb) =>
                new Exemplar(
                    exemplarDb.Id,
                    new Livro(livroDb.Id, livroDb.Titulo, livroDb.Autor, livroDb.ISBN, livroDb.AnoPublicacao),
                    exemplarDb.SomenteLeituraLocal
                ),
            new { Id = id },
            splitOn: "id"
        ).FirstOrDefault();
    }

    public Exemplar? ObterDisponivelParaLivro(long livroId)
    {
        const string sql = @"
            SELECT
                e.id, e.livro_id as livroId, e.somente_leitura_local as SomenteLeituraLocal,
                l.id, l.titulo, l.autor, l.isbn, l.ano_publicacao as anoPublicacao
            FROM exemplares e
            JOIN livros l ON l.id = e.livro_id
            LEFT JOIN emprestimos em ON em.exemplar_id = e.id AND em.data_devolucao IS NULL
            WHERE e.livro_id = @LivroId
                AND em.id IS NULL
                AND e.somente_leitura_local = 0
            LIMIT 1
        ";

        return _conexao.Query<ExemplarDb, LivroDb, Exemplar>(
            sql,
            (exemplarDb, livroDb) => 
                new Exemplar(
                    exemplarDb.Id,
                    new Livro(
                        livroDb.Id,
                        livroDb.Titulo,
                        livroDb.Autor,
                        livroDb.ISBN,
                        livroDb.AnoPublicacao
                    ),
                    exemplarDb.SomenteLeituraLocal
                    ),
            new { LivroId = livroId },
            splitOn: "id"
        ).FirstOrDefault();
    }

    public Exemplar Adicionar(Exemplar exemplar)
    {
        const string sql = @"
            INSERT INTO exemplares (livro_id, somente_leitura_local)
            VALUES (@Id, @SomenteLeituraLocal)
            RETURNING id
        ";


        var exemplarId = _conexao.ExecuteScalar<long>(
            sql,
            new {
                exemplar.Livro.Id,
                exemplar.SomenteLeituraLocal
            }
        );

        return new Exemplar(
            exemplarId,
            exemplar.Livro,
            exemplar.SomenteLeituraLocal
         );
    }

    public void AtualizarModoDeLeitura(long exemplarId, bool leituraLocal)
    {
        const string sql = @"
            UPDATE exemplares
            SET somente_leitura_local = @LeituraLocal
            WHERE id = @ExemplarId
        ";

        _conexao.Execute(sql, new
        {
            LeituraLocal = leituraLocal,
            ExemplarId = exemplarId
        });
    }
}
