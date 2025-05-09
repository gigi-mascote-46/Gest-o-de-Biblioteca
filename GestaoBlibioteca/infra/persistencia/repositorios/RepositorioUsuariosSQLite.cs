//RepositorioUsuariosSQLite

using System.Data;
using Biblioteca.Dominio.Modelos;
using Biblioteca.Dominio.Repositorios;
using Dapper;

namespace Biblioteca.Infra.Persistencia;

/// <summary>
/// Implementação do repositório de usuários usando SQLite e Dapper.
/// Responsável por persistir e recuperar dados de usuários, bem como atualizar suas informações de contato.
///
/// O campo "role" é armazenado como string no banco de dados e convertido para o enum <see cref="Role"/> no domínio.
/// </summary>
public class RepositorioUsuariosSQLite : IRepositorioUsuarios
{
    private readonly IDbConnection _conexao;

    public RepositorioUsuariosSQLite(IDbConnection conexao)
    {
        _conexao = conexao;
    }

    public Usuario? ObterPorId(long id)
    {
        const string sql = @"
            SELECT id, nome_usuario AS NomeUsuario, nome, telefone, email, role
            FROM usuarios
            WHERE id = @Id
        ";

        return _conexao.QueryFirstOrDefault<Usuario>(sql, new { Id = id });
    }

    public Usuario? ObterPorNomeUsuario(string nome)
    {
        const string sql = @"
            SELECT id, nome_usuario AS NomeUsuario, nome, telefone, email, role
            FROM usuarios
            WHERE nome_usuario = @Nome
        ";

        return _conexao.QueryFirstOrDefault<Usuario>(sql, new { Nome = nome });
    }

    public Usuario Adicionar(Usuario usuario)
    {
        const string sql = @"
            INSERT INTO usuarios (nome_usuario, nome, telefone, email, role)
            VALUES (@NomeUsuario, @Nome, @Telefone, @Email, @Role)
            RETURNING id, nome_usuario AS NomeUsuario, nome, telefone, email, role
        ";

        return _conexao.QueryFirstOrDefault<Usuario>(sql, new
        {
            usuario.NomeUsuario,
            usuario.Nome,
            usuario.Telefone,
            usuario.Email,
            Role = usuario.Role.ToString()
        });
    }

    public void AtualizarContato(long usuarioId, string novoTelefone, string novoEmail)
    {
        const string sql = @"
            UPDATE usuarios
            SET telefone = @Telefone, email = @Email
            WHERE id = @UsuarioId
        ";

        _conexao.Execute(sql, new
        {
            Telefone = novoTelefone,
            Email = novoEmail,
            UsuarioId = usuarioId
        });
    }

    public List<Usuario> ListarTodos(int limite)
    {
        const string sql = @"
            SELECT id, nome_usuario AS NomeUsuario, nome, telefone, email, role
            FROM usuarios
            LIMIT @Limite
        ";

        return _conexao.Query<Usuario>(sql, new { Limite = limite }).ToList();
    }
}
