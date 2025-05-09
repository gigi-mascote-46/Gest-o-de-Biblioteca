//UsuarioUI

using Aplicacao;
using Aplicacao.Menus;
using Biblioteca.Aplicacao.Servicos;
using Biblioteca.Dominio.Enums;
using Biblioteca.Dominio.Modelos;
using Biblioteca.UI.Componentes;

//TODO: Falta AtualizarContato.

namespace Biblioteca.UI.Telas
{
    /// <summary>
    /// Interface de usuário para operações relacionadas a usuários do sistema.
    /// Permite cadastro, listagem e alteração de papel (role) dos usuários.
    /// </summary>
    public class UsuarioUI
    {
        private readonly AppServiceUsuarios _appServiceUsuarios;

        /// <summary>
        /// Inicializa uma nova instância de <see cref="UsuarioUI"/>.
        /// </summary>
        /// <param name="appServiceUsuarios">Serviço de aplicação responsável por operações com usuários.</param>
        public UsuarioUI(AppServiceUsuarios appServiceUsuarios)
        {
            _appServiceUsuarios = appServiceUsuarios;
        }

        /// <summary>
        /// Exibe o menu de operações relacionadas a usuários.
        /// </summary>
        /// <param name="roleAtual">Permissão do usuário logado.</param>
        public void ExibirMenu(Role roleAtual)
        {
            var menu = new Menu("Menu de Usuários", new List<MenuItem>
            {
                new MenuItem("Cadastrar Usuário", CadastrarUsuario, new[] { Role.Bibliotecario }),
                new MenuItem("Listar Todos os Usuários", ListarUsuarios, new[] { Role.Bibliotecario }),
                // TODO: Reativar a alteração de role de usuario somente depois de adicionar regra de negócio
                // TODO: para evitar a alteração do role do usuário atualmente logado.
//                new MenuItem("Alterar Role de Usuário", AlterarRole, new[] { Role.Bibliotecario })
            }, incluirOpcaoVoltar: true);

            MenuUtils.ExecutarMenuCompleto(menu, roleAtual);
        }

        /// <summary>
        /// Solicita dados ao usuário e realiza o cadastro de um novo usuário do sistema.
        /// </summary>
        private void CadastrarUsuario()
        {
            Console.Clear();
            Console.WriteLine("=== Cadastrar Usuário ===");

            var nomeUsuario = ConsoleEx.LerTexto("Login: ");
            var nome = ConsoleEx.LerTexto("Nome: ");
            var telefone = ConsoleEx.LerTexto("Telefone: ");
            var email = ConsoleEx.LerTexto("Email: ", s => s.Contains("@"));

            Console.WriteLine("Roles disponíveis:");
            foreach (var role in Enum.GetValues<Role>())
            {
                Console.WriteLine($"- {role}");
            }

            var roleStr = ConsoleEx.LerTexto("Role: ", s => Enum.TryParse<Role>(s, true, out _));
            var roleSelecionado = Enum.Parse<Role>(roleStr, true);

            // TODO: O método Cadastrar está sendo usado com os parâmetros errados.
            var usuario = _appServiceUsuarios.Cadastrar(nomeUsuario, nome, telefone, email, roleSelecionado);
            Console.WriteLine($"Usuário cadastrado com sucesso! ID: {usuario.Id}");
        }

        /// <summary>
        /// Lista todos os usuários cadastrados no sistema.
        /// </summary>
        private void ListarUsuarios()
        {
            Console.Clear();
            Console.WriteLine("=== Lista de Usuários ===");

            var usuarios = _appServiceUsuarios.ListarTodos();
            if (!usuarios.Any())
            {
                Console.WriteLine("Nenhum usuário encontrado.");
                return;
            }

            foreach (var usuario in usuarios)
            {
                ExibirUsuario(usuario);
            }
        }

        /// <summary>
        /// Solicita dados ao usuário e altera a role de um usuário existente.
        /// </summary>
//        private void AlterarRole()
//        {
//            Console.Clear();
//            Console.WriteLine("=== Alterar Role de Usuário ===");
//
//            int usuarioId = ConsoleEx.LeInt("ID do Usuário: ");
//
//            Console.WriteLine("Roles disponíveis:");
//            foreach (var role in Enum.GetValues<Role>())
//            {
//                Console.WriteLine($"- {role}");
//            }
//
//            var roleStr = ConsoleEx.LeString("Nova Role: ", s => Enum.TryParse<Role>(s, true, out _));
//            var novaRole = Enum.Parse<Role>(roleStr, true);
//
//            _appServiceUsuarios.AlterarRole(usuarioId, novaRole);
//            Console.WriteLine("Role atualizada com sucesso.");
//        }

        /// <summary>
        /// Exibe informações detalhadas de um usuário.
        /// </summary>
        /// <param name="usuario">Usuário a ser exibido.</param>
        private void ExibirUsuario(Usuario usuario)
        {
            Console.WriteLine($"ID: {usuario.Id}");
            Console.WriteLine($"Nome: {usuario.NomeUsuario}");
            Console.WriteLine($"Role: {usuario.Role}");
            Console.WriteLine("--------------------------");
        }
    }
}
