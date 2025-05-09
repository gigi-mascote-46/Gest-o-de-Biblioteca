using Biblioteca.Dominio.Enums;

namespace GestaoBlibioteca.infra.persistencia.modelos
{
    /// <summary>
    /// Representa um Usuario proveniente da camada de persistência.
    /// Instâncias desta classe são criadas exclusivamente pelo Dapper.
    /// </summary>
    internal sealed class UsuarioDb
    {
        public long Id { get; }
        public string NomeUsuario { get; }
        public string Nome { get; }
        public string Telefone { get; }
        public string Email { get; }
        public Role Role { get; }

        public UsuarioDb(long id, string nomeUsuario, string nome, string telefone, string email, string role) { 
            Id = id;
            NomeUsuario = nomeUsuario;
            Nome = nome;
            Telefone = telefone;
            Email = email;
            Role = Enum.Parse<Role>(role, true);
        }
    }
}
