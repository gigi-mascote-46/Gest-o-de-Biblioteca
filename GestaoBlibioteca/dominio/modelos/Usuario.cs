//Domain Model: Usuario
using Biblioteca.Dominio.Enums;

namespace Biblioteca.Dominio.Modelos;

/// <summary>
/// Representa um usuário do sistema, que pode ser um leitor ou bibliotecário.
/// O campo Username é obrigatório e usado no login.
/// </summary>
public sealed class Usuario
{
    public long Id { get; }
    public string NomeUsuario { get; }
    public string Nome { get; }
    public string Telefone { get; }
    public string Email { get; }
    public Role Role { get; }

    public Usuario(long id, string nomeUsuario, string nome, string telefone, string email, Role role)
    {
        if (string.IsNullOrWhiteSpace(nomeUsuario))
            throw new ArgumentException("Nome de usuário é obrigatório.", nameof(nomeUsuario));

        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome é obrigatório.", nameof(nome));

        if (string.IsNullOrWhiteSpace(telefone))
            throw new ArgumentException("Telefone é obrigatório.", nameof(telefone));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email é obrigatório.", nameof(email));

        Id = id;
        NomeUsuario = nomeUsuario;
        Nome = nome;
        Telefone = telefone;
        Email = email;
        Role = role;
    }

//    public override string ToString() => $"{Nome} ({Username}) - {Role}";
}
