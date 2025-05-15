//Role

namespace Biblioteca.Dominio.Enums;

/// <summary>
/// Define os perfis de acesso disponíveis no sistema.
/// </summary>
public enum Role
{
    /// <summary>Usuário com permissões completas (administração, empréstimos, etc.).</summary>
    Bibliotecario,

    /// <summary>Usuário comum com acesso limitado.</summary>
    Leitor
}
