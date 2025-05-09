//RepositorioEmprestimosSQLite

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using Biblioteca.Dominio.Modelos;
using Biblioteca.Dominio.Repositorios;
using Biblioteca.UI.Telas;
using Dapper;
using GestaoBlibioteca.dominio.modelos;
using GestaoBlibioteca.infra.persistencia.modelos;
using UI;

namespace Biblioteca.Infraestrutura.Persistencia.Repositorios;

/// <summary>
/// Implementação do repositório de empréstimos usando SQLite e Dapper.
/// Responsável por armazenar, recuperar e atualizar empréstimos de exemplares.
///
/// Datas são persistidas em formato ISO 8601 UTC (string) conforme decisão arquitetural do projeto.
/// </summary>
public class RepositorioEmprestimosSQLite : IRepositorioEmprestimos
{
    private readonly IDbConnection _conexao;

    public RepositorioEmprestimosSQLite(IDbConnection conexao)
    {
        _conexao = conexao;
    }

        public Emprestimo? ObterPorId(long id)
    {
        const string sql = @"
                SELECT
                    e.id,
                    e.usuario_id as usuarioId,
                    e.exemplar_id as exemplarId,
                    e.data_emprestimo as dataEmprestimo,
                    e.data_limite_devolucao as dataLimiteDevolucao,
                    e.data_devolucao as dataDevolucao,

                    u.id, u.nome_usuario as nomeUsuario, u.nome, u.telefone, u.email, u.role,

                    ex.id, ex.livro_id as livroId, ex.somente_leitura_local as somenteLeituraLocal,

                    l.id, l.titulo, l.autor, l.isbn, l.ano_publicacao as anoPublicacao

                FROM emprestimos e
                JOIN usuarios u ON e.usuario_id = u.id
                JOIN exemplares ex ON e.exemplar_id = ex.id
                JOIN livros l ON ex.livro_id = l.id
                WHERE e.id = @id
        ";

            return _conexao.Query<EmprestimoDb, UsuarioDb, ExemplarDb, LivroDb, Emprestimo>(
                sql,
                (emprestimoDb, usuarioDb, exemplarDb, livroDb) =>
                {
                    return new Emprestimo(
                        emprestimoDb.Id,
                        new Usuario(
                            usuarioDb.Id,
                            usuarioDb.NomeUsuario,
                            usuarioDb.Nome,
                            usuarioDb.Telefone,
                            usuarioDb.Email,
                            usuarioDb.Role
                            ),
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
                        emprestimoDb.DataEmprestimo,
                        emprestimoDb.DataLimiteDevolucao,
                        emprestimoDb.DataDevolucao
                    );
                },
                new { id },
                splitOn: "id,id"
            ).FirstOrDefault();
    }

    public List<Emprestimo> ListarPorUsuario(long usuarioId, int limite)
    {
        const string sql = @"
                SELECT
                    e.id,
                    e.usuario_id as usuarioId,
                    e.exemplar_id as exemplarId,
                    e.data_emprestimo as dataEmprestimo,
                    e.data_limite_devolucao as dataLimiteDevolucao,
                    e.data_devolucao as dataDevolucao,

                    u.id, u.nome_usuario as nomeUsuario, u.nome, u.telefone, u.email, u.role,

                    ex.id, ex.livro_id as livroId, ex.somente_leitura_local as somenteLeituraLocal,

                    l.id, l.titulo, l.autor, l.isbn, l.ano_publicacao as anoPublicacao

                FROM emprestimos e
                JOIN usuarios u ON e.usuario_id = u.id
                JOIN exemplares ex ON e.exemplar_id = ex.id
                JOIN livros l ON ex.livro_id = l.id
                WHERE u.id = @usuarioId
                ORDER BY e.data_emprestimo DESC
                LIMIT @limite
        ";

        return _conexao.Query<EmprestimoDb, UsuarioDb, ExemplarDb, LivroDb, Emprestimo>(
                sql,
                (emprestimoDb, usuarioDb, exemplarDb, livroDb) =>
                {
                    return new Emprestimo(
                        emprestimoDb.Id,
                        new Usuario(
                            usuarioDb.Id,
                            usuarioDb.NomeUsuario,
                            usuarioDb.Nome,
                            usuarioDb.Telefone,
                            usuarioDb.Email,
                            usuarioDb.Role
                            ),
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
                        emprestimoDb.DataEmprestimo,
                        emprestimoDb.DataLimiteDevolucao,
                        emprestimoDb.DataDevolucao
                    );
                },
                new { usuarioId, limite },
                splitOn: "id,id,id"
            ).ToList();
    }

    public Emprestimo Adicionar(Emprestimo emprestimo)
    {
        const string sql = @"
            INSERT INTO emprestimos (usuario_id, exemplar_id, data_emprestimo, data_limite_devolucao, data_devolucao)
            VALUES (@UsuarioId, @ExemplarId, @DataEmprestimo, @DataLimiteDevolucao, NULL)
            RETURNING id
        ";

        var emprestimoId = _conexao.ExecuteScalar<long>(
                sql,
                new
                {
                    UsuarioId = emprestimo.Usuario.Id,
                    ExemplarId = emprestimo.Exemplar.Id,
                    DataEmprestimo = emprestimo.DataEmprestimo.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                    DataLimiteDevolucao = emprestimo.DataLimiteDevolucao.ToString("yyyy-MM-ddTHH:mm:ssZ")
                }
            );

            return new Emprestimo(
                emprestimoId,
                emprestimo.Usuario,
                emprestimo.Exemplar,
                emprestimo.DataEmprestimo,
                emprestimo.DataLimiteDevolucao,
                emprestimo.DataDevolucao
             );
    }

    public void RegistrarDevolucao(long emprestimoId, DateTime dataDevolucao)
    {
        const string sql = @"
            UPDATE emprestimos
                SET data_devolucao = @DataDevolucao
            WHERE id = @Id
        ";

        _conexao.Execute(sql, new
        {
            Id = emprestimoId,
                DataDevolucao = dataDevolucao.ToString("yyyy-MM-ddTHH:mm:ssZ")
        });
    }

    public List<Emprestimo> ListarTodos(int limite)
    {
        const string sql = @"
                SELECT em.id, em.usuario_id as usuarioId, em.exemplar_id as exemplarId,
                    em.data_emprestimo as dataEmprestimo,
                    em.data_limite_devolucao as dataLimiteDevolucao,
                    em.data_devolucao as dataDevolucao,
                    u.id, u.nome_usuario as nomeUsuario, u.nome, u.telefone, u.email, u.role,
                    ex.id, ex.livro_id as livroId, ex.somente_leitura_local as somenteLeituraLocal,
                    l.id, l.titulo, l.autor, l.isbn, l.ano_publicacao as anoPublicacao
                FROM emprestimos em
                JOIN usuarios u ON em.usuario_id = u.id
                JOIN exemplares ex ON em.exemplar_id = ex.id
                JOIN livros l ON ex.livro_id = l.id
                ORDER BY em.id DESC
                LIMIT @limite
        ";
        return _conexao.Query<EmprestimoDb, UsuarioDb, ExemplarDb, LivroDb, Emprestimo>(
            sql,
            (emprestimoDb, usuarioDb, exemplarDb, livroDb) =>
            {
                return new Emprestimo(
                    emprestimoDb.Id,
                    new Usuario(
                        usuarioDb.Id,
                        usuarioDb.NomeUsuario,
                        usuarioDb.Nome,
                        usuarioDb.Telefone,
                        usuarioDb.Email,
                        usuarioDb.Role
                        ),
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
                    emprestimoDb.DataEmprestimo,
                    emprestimoDb.DataLimiteDevolucao,
                    emprestimoDb.DataDevolucao
                );
            },
            new { limite },
            splitOn: "id,id,id"
        ).ToList();
    }
}
