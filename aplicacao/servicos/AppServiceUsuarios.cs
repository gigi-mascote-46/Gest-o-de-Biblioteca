// AppServiceUsuarios

using Biblioteca.Dominio.Servicos;
using Biblioteca.Dominio.Repositorios;
using Biblioteca.Dominio.Modelos;
using Biblioteca.Dominio;
using Biblioteca.Dominio.Enums;
using GestaoBlibioteca.Dominio.Excecoes;

namespace Biblioteca.Aplicacao.Servicos;

/// <summary>
/// Application Service responsável por coordenar os casos de uso relacionados ao cadastro, consulta e atualização de usuários.
/// Segue o padrão de Application Service da arquitetura DDD, orquestrando repositórios, domain services e unidade de trabalho,
/// mas sem conter lógica de domínio.
/// </summary>
public class AppServiceUsuarios
{
    private readonly IRepositorioUsuarios _repositorioUsuarios;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
    private readonly CadastroDeUsuarios _cadastroDeUsuarios;

    public AppServiceUsuarios(
        IRepositorioUsuarios repositorioUsuarios,
        IUnidadeDeTrabalho unidadeDeTrabalho,
        CadastroDeUsuarios cadastroDeUsuarios)
    {
        _repositorioUsuarios = repositorioUsuarios;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _cadastroDeUsuarios = cadastroDeUsuarios;
    }

    /// <summary>
    /// Realiza o cadastro de um novo usuário.
    /// </summary>
    /// <param name="nomeUsuario">Nome de usuário único.</param>
    /// <param name="nome">Nome completo do usuário.</param>
    /// <param name="telefone">Telefone de contato.</param>
    /// <param name="email">Endereço de e-mail.</param>
    /// <param name="role">Papel do usuário no sistema (por exemplo, Leitor ou Bibliotecario).</param>
    /// <returns>Usuário cadastrado.</returns>
    public Usuario Cadastrar(string nomeUsuario, string nome, string telefone, string email, Role role)
    {
        return _unidadeDeTrabalho.ExecutarComTransacao(() =>
        {

            var usuario = _cadastroDeUsuarios.Criar(nomeUsuario, nome, telefone, email, role);

        return _repositorioUsuarios.Adicionar(usuario);
        }); 
    }

    /// <summary>
    /// Atualiza o telefone e e-mail de contato de um usuário existente.
    /// </summary>
    /// <param name="usuarioId">ID do usuário a ser atualizado.</param>
    /// <param name="novoTelefone">Novo telefone de contato.</param>
    /// <param name="novoEmail">Novo e-mail de contato.</param>
    /// <returns>Usuário atualizado.</returns>
    /// <exception cref="UsuarioNaoEncontradoException">
    /// Lançada caso não seja encontrado um usuário com o ID informado.
    /// </exception>
    public Usuario AtualizarContato(int usuarioId, string novoTelefone, string novoEmail)
    {
        return _unidadeDeTrabalho.ExecutarComTransacao(() =>
        {

            var usuario = _repositorioUsuarios.ObterPorId(usuarioId);
        if (usuario is null)
        {
            throw new UsuarioNaoEncontradoException();
        }

        var atualizado = _cadastroDeUsuarios.AtualizarContato(usuario, novoTelefone, novoEmail);

        _repositorioUsuarios.AtualizarContato(usuarioId, novoTelefone, novoEmail);
        return atualizado;

        });
    }

    /// <summary>
    /// Lista todos os usuários, com um limite máximo de resultados.
    /// </summary>
    /// <param name="limite">Número máximo de usuários a retornar (valor padrão: 10).</param>
    /// <returns>Lista de usuários.</returns>
    public List<Usuario> ListarTodos(int limite = 10)
    {
        return _repositorioUsuarios.ListarTodos(limite);
    }

    /// <summary>
    /// Obtém um usuário a partir do seu nome de usuário.
    /// </summary>
    /// <param name="nomeUsuario">Nome de usuário a buscar.</param>
    /// <returns>Usuário correspondente, ou null se não encontrado.</returns>
    public Usuario? ObterPorNomeUsuario(string nomeUsuario)
    {
        return _repositorioUsuarios.ObterPorNomeUsuario(nomeUsuario);
    }
}
