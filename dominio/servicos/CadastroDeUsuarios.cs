//CadastroDeUsuarios

using Biblioteca.Dominio.Modelos;
using Biblioteca.Dominio.Enums;

namespace Biblioteca.Dominio.Servicos;

/// <summary>
/// Serviço de domínio responsável pelas operações relacionadas ao ciclo de vida do <see cref="Usuario"/>,
/// como cadastro e atualização de informações.
/// </summary>
public class CadastroDeUsuarios
{
    /// <summary>
    /// Cria uma nova instância válida de <see cref="Usuario"/>, com base nos dados fornecidos.
    /// </summary>
    /// <param name="nomeUsuario">Nome de usuário (login) único.</param>
    /// <param name="nome">Nome completo do usuário.</param>
    /// <param name="telefone">Número de telefone para contato.</param>
    /// <param name="email">Endereço de e-mail.</param>
    /// <param name="role">Perfil de acesso (papel) do usuário (por exemplo, Leitor ou Bibliotecário).</param>
    /// <returns>Nova instância de <see cref="Usuario"/>.</returns>
    /// <exception cref="ArgumentException">Lançada quando algum dos campos obrigatórios estiver vazio ou nulo.</exception>
    /// <remarks>
    /// Este método é responsável por encapsular a lógica de criação da entidade
    /// <see cref="Usuario"/>. Ele delega à própria entidade a validação da integridade
    /// dos dados.
    ///
    /// O id do usuário é atribuído posteriormente pela infraestrutura de persistência.
    /// </remarks>
    public Usuario Criar(string nomeUsuario, string nome, string telefone, string email, Role role)
    {
        if (string.IsNullOrWhiteSpace(nomeUsuario))
            throw new ArgumentException("O nome de usuário é obrigatório.", nameof(nomeUsuario));

        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome é obrigatório.", nameof(nome));

        // Email e telefone podem ter validações futuras específicas

        return new Usuario(
            id: 0, // Atribuído posteriormente pela camada de persistência
            nomeUsuario: nomeUsuario,
            nome: nome,
            telefone: telefone,
            email: email,
            role: role
        );
    }

    /// <summary>
    /// Atualiza os dados de contato de um <see cref="Usuario"/>, retornando uma nova instância (se houver alteração).
    /// </summary>
    /// <param name="usuario">Instância atual do usuário.</param>
    /// <param name="novoTelefone">Novo número de telefone de contato.</param>
    /// <param name="novoEmail">Novo endereço de e-mail.</param>
    /// <returns>
    /// Nova instância de <see cref="Usuario"/> com os dados de contato atualizados,
    /// ou a instância original se não houver mudanças.
    /// </returns>
    /// <remarks>
    /// Como a entidade <see cref="Usuario"/> é imutável, a atualização de dados
    /// requer a criação de uma nova instância refletindo as alterações.
    /// </remarks>
    /// <exception cref="ArgumentNullException">Lançada caso o usuário fornecido for nulo.</exception>
    public Usuario AtualizarContato(Usuario usuario, string novoTelefone, string novoEmail)
    {
        if (usuario == null)
            throw new ArgumentNullException(nameof(usuario), "Usuário não pode ser nulo.");

        // Se nada mudou, evita criação desnecessária
        if (usuario.Telefone == novoTelefone && usuario.Email == novoEmail)
            return usuario;

        return new Usuario(
            id: usuario.Id,
            nomeUsuario: usuario.NomeUsuario,
            nome: usuario.Nome,
            telefone: novoTelefone,
            email: novoEmail,
            role: usuario.Role
        );
    }
}
