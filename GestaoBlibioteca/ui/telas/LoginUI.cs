//LoginUI

using Aplicacao;
using Biblioteca.Aplicacao.Servicos;


namespace UI
{
    /// <summary>
    /// Responsável por lidar com a interface de login do sistema.
    /// Permite ao usuário informar seu nome de usuário e iniciar uma sessão.
    /// </summary>
    public class LoginUI
    {
        private readonly AppServiceUsuarios _appServiceUsuarios;
        private readonly Sessao _sessao;

        /// <summary>
        /// Inicializa uma nova instância de <see cref="LoginUI"/>.
        /// </summary>
        /// <param name="appServiceUsuarios">Serviço de aplicação responsável por operações com usuários.</param>
        /// <param name="sessao">Gerenciador de sessão do sistema.</param>
        public LoginUI(AppServiceUsuarios appServiceUsuarios, Sessao sessao)
        {
            _appServiceUsuarios = appServiceUsuarios;
            _sessao = sessao;
        }

        /// <summary>
        /// Executa o processo de login do usuário no console.
        /// Solicita o nome de usuário, valida e inicia a sessão.
        /// </summary>
        /// <returns>Retorna verdadeiro se o login for bem-sucedido, falso caso contrário.</returns>
        public bool Login()
        {
            Console.Clear();
            Console.WriteLine("=== Login ===");
            Console.Write("Nome de usuário: ");
            var nomeUsuario = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nomeUsuario))
            {
                Console.WriteLine("Nome de usuário não pode ser vazio.");
                return false;
            }

            var usuario = _appServiceUsuarios.ObterPorNomeUsuario(nomeUsuario.Trim());

            if (usuario == null)
            {
                Console.WriteLine("Usuário não encontrado.");
                return false;
            }

            _sessao.Login(usuario);
            Console.WriteLine($"Bem-vindo, {usuario.Nome} ({usuario.Role})!");
            return true;
        }
    }
}
