//MenuPrincipalUI

using System;
using System.Collections.Generic;
using Aplicacao.Menus;
using Biblioteca.Dominio.Enums;
using Biblioteca.UI.Componentes;
using Biblioteca.UI.Telas;
using UI;

namespace Biblioteca.UI
{
    /// <summary>
    /// Interface de usuário responsável por exibir o menu principal e redirecionar para os submenus da aplicação.
    /// </summary>
    public class MenuPrincipalUI
    {
        private readonly LivroUI _livroUI;
        private readonly UsuarioUI _usuarioUI;
        private readonly ExemplarUI _exemplarUI;
        private readonly EmprestimoUI _emprestimoUI;

        /// <summary>
        /// Inicializa uma nova instância de <see cref="MenuPrincipalUI"/>.
        /// </summary>
        /// <param name="livroUI">Interface de usuário para livros.</param>
        /// <param name="usuarioUI">Interface de usuário para usuários.</param>
        /// <param name="exemplarUI">Interface de usuário para exemplares.</param>
        /// <param name="emprestimoUI">Interface de usuário para empréstimos.</param>
        public MenuPrincipalUI(
            LivroUI livroUI,
            UsuarioUI usuarioUI,
            ExemplarUI exemplarUI,
            EmprestimoUI emprestimoUI)
        {
            _livroUI = livroUI;
            _usuarioUI = usuarioUI;
            _exemplarUI = exemplarUI;
            _emprestimoUI = emprestimoUI;
        }

        /// <summary>
        /// Exibe o menu principal com base no role do usuário e redireciona para os submenus.
        /// </summary>
        /// <param name="roleAtual">Perfil de acesso do usuário logado.</param>
        public void ExibirMenu(Role roleAtual)
        {
            var menu = new Menu("Menu Principal", new List<MenuItem>
            {
                new MenuItem("Gerenciar Livros",      () => _livroUI.ExibirMenu(roleAtual)),
                new MenuItem("Gerenciar Usuários",    () => _usuarioUI.ExibirMenu(roleAtual), new[] { Role.Bibliotecario }),
                new MenuItem("Gerenciar Exemplares",  () => _exemplarUI.ExibirMenu(roleAtual), new[] { Role.Bibliotecario }),
                new MenuItem("Gerenciar Empréstimos", () => _emprestimoUI.ExibirMenu(roleAtual))
            }, incluirOpcaoVoltar: true); // "Voltar" aqui será interpretado como logout

            MenuUtils.ExecutarMenuCompleto(menu, roleAtual);
        }
    }
}
