//UIController

using System;
using Biblioteca.UI.Telas;
using UI;

namespace Biblioteca.UI
{
    /// <summary>
    /// Controlador principal da interface de usuário. Gerencia o ciclo de login e menus principais.
    /// </summary>
    public class UIController
    {
        private readonly LoginUI _loginUI;
        private readonly MenuPrincipalUI _menuPrincipalUI;
        private readonly Sessao _sessao;

        /// <summary>
        /// Inicializa uma nova instância de <see cref="UIController"/>.
        /// </summary>
        /// <param name="loginUI">Interface de usuário para autenticação.</param>
        /// <param name="menuPrincipalUI">Interface de usuário do menu principal.</param>
        public UIController(LoginUI loginUI, MenuPrincipalUI menuPrincipalUI, Sessao sessao)
        {
            _loginUI = loginUI;
            _menuPrincipalUI = menuPrincipalUI;
            _sessao = sessao;
        }

        /// <summary>
        /// Inicia a interface de usuário, exibindo o fluxo principal de login e navegação.
        /// </summary>
        public void Iniciar()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Sistema de Biblioteca ===");

                var logado = _loginUI.Login();
                if (logado == false)
                {
                    continue;
                }

                _menuPrincipalUI.ExibirMenu(_sessao.UsuarioAtual.Role);

                // TODO: Limpar a sessão (fazer logout).
                Console.WriteLine("Você saiu da sessão. Pressione uma tecla para voltar ao login...");
                Console.ReadKey();
            }
        }
    }
}
