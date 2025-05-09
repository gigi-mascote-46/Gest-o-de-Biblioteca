//RepositorioLivrosSQLite

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Biblioteca.Dominio.Modelos;
using Biblioteca.Dominio.Repositorios;
using Dapper;
using GestaoBlibioteca.dominio.modelos;
using GestaoBlibioteca.infra.persistencia.modelos;

namespace Biblioteca.Infra.Persistencia
{
    /// <summary>
    /// Implementação concreta de <see cref="IRepositorioLivros"/> usando SQLite e Dapper.
    /// </summary>
    public class RepositorioLivrosSQLite : IRepositorioLivros
    {
        private readonly IDbConnection _conexao;

        /// <summary>
        /// Inicializa uma nova instância de <see cref="RepositorioLivrosSQLite"/>.
        /// </summary>
        /// <param name="conexao">Conexão SQLite já aberta.</param>
        public RepositorioLivrosSQLite(IDbConnection conexao)
        {
            _conexao = conexao ?? throw new ArgumentNullException(nameof(conexao));
        }

        // TODO: Se for necessário retornar o id gerado no banco de dados nesta função, alterar:
        // TODO: Tipo de retorno de void para int.
        // TODO: Terminar a query SQL com: "returning id" (adicionar isso no final, antes do ponto e vírgula),
        // TODO: pode quebrar uma linha para ficar mais fácil de ler a query.
        // TODO: Mudar a linha que execura a query para começar com : return _conexao.ExecuteScalar<long>
        // TODO: Mudar na interface. Depois pode mudar nos usos deste método nas outras classes.
        // TODO: Depois pode fazer o mesmo nos outros métodos desta classe e nos outros repositórios
        // TODO: (interfaces e implementações concretas).
        public Livro Adicionar(Livro livro)
        {
            const string sql = @"
                INSERT INTO livros (titulo, autor, isbn, ano_publicacao)
                VALUES (@Titulo, @Autor, @ISBN, @AnoPublicacao)
                RETURNING id, titulo, autor, isbn, ano_publicacao as anoPublicacao;";

            return _conexao.QueryFirstOrDefault<Livro>(sql, new
            {
                livro.Titulo,
                livro.Autor,
                livro.ISBN,
                AnoPublicacao = livro.AnoPublicacao
            });
        }

        public Livro? ObterPorId(long id)
        {
            const string sql = @"
                SELECT id, titulo, autor, isbn, ano_publicacao as anoPublicacao
                FROM livros
                WHERE id = @Id;";

            return _conexao.QueryFirstOrDefault<Livro>(sql, new { Id = id });
        }

        public Livro? ObterPorISBN(string isbn)
        {
            const string sql = @"
                SELECT id, titulo, autor, isbn, ano_publicacao as anoPublicacao
                FROM livros
                WHERE isbn = @ISBN;";

            return _conexao.QueryFirstOrDefault<Livro>(sql, new { ISBN = isbn });
        }

        public List<Livro> BuscarPorTitulo(string titulo, int limite)
        {
            const string sql = @"
                SELECT id, titulo, autor, isbn, ano_publicacao as anoPublicacao
                FROM livros
                WHERE titulo LIKE @Titulo
                LIMIT @Limite;";

            return _conexao.Query<Livro>(sql, new
            {
                Titulo = $"%{titulo}%",
                Limite = limite
            }).ToList();
        }

        public List<Livro> BuscarPorAutor(string autor, int limite)
        {
            const string sql = @"
                SELECT id, titulo, autor, isbn, ano_publicacao as anoPublicacao
                FROM livros
                WHERE autor LIKE @Autor
                LIMIT @Limite;";

            return _conexao.Query<Livro>(sql, new
            {
                Autor = $"%{autor}%",
                Limite = limite
            }).ToList();
        }

        public List<Livro> ListarTodos(int limite)
        {
            const string sql = @"
                SELECT id, titulo, autor, isbn, ano_publicacao as anoPublicacao
                FROM livros
                LIMIT @Limite;";

            return _conexao.Query<Livro>(sql, new { Limite = limite }).ToList();
        }

        public List<Disponibilidade> ListarDisponibilidade(int limite)
        {
            const string sql = @"
           SELECT l.id, l.titulo, l.autor, l.isbn, l.ano_publicacao as anoPublicacao,
            COUNT(ex.id) AS numExemplares,
            SUM(
                CASE
                    WHEN ex.somente_leitura_local = 0 AND (
                        NOT EXISTS (
                            SELECT 1
                            FROM emprestimos em
                            WHERE em.exemplar_id = ex.id
                            AND em.data_devolucao IS NULL
                        )
                    ) THEN 1
                    ELSE 0
                END
            ) AS numExemplaresDisponiveis
            FROM livros l
            LEFT JOIN exemplares ex ON ex.livro_id = l.id
            GROUP BY l.id
            order by l.id
            limit @Limite
        ";

            return _conexao.Query<LivroDb, long, long, Disponibilidade>(
                sql,
                (livroDb, numExemplares, numExemplaresDisponiveis) =>
                    new Disponibilidade(
                        new Livro(
                            livroDb.Id,
                            livroDb.Titulo,
                            livroDb.Autor,
                            livroDb.ISBN,
                            livroDb.AnoPublicacao
                        ),
                        numExemplares,
                        numExemplaresDisponiveis
                ),
                new { Limite = limite },
                splitOn: "numExemplares,numExemplaresDisponiveis"
            ).ToList();
        }
    }
}
