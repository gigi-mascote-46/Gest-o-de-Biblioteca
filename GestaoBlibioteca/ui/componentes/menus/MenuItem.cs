//MenuItem

using System;
using System.Collections.Generic;
using System.Linq;
using Biblioteca.Dominio.Enums; // Para o enum Role

namespace Biblioteca.UI.Componentes
{
    /// <summary>
    /// Representa um item de menu, com texto, ação a ser executada e roles permitidos.
    /// </summary>
    public class MenuItem
    {
        public string Texto { get; }
        public Action Acao { get; }
        public Role[] Roles { get; } // TODO: ver se precisa ser nullable: Role[]?

        /// <param name="texto">Texto exibido no menu</param>
        /// <param name="acao">Ação a ser executada quando selecionado</param>
        /// <param name="roles">Roles permitidos a visualizar/selecionar este item. Se null, todos podem ver.</param>
        public MenuItem(string texto, Action acao, Role[] roles = null)
        {
            Texto = texto;
            Acao = acao;
            Roles = roles;
        }

        /// <summary>
        /// Retorna se o item pode ser exibido para o role atual.
        /// </summary>
        public bool PodeSerExibido(Role roleAtual)
        {
            return Roles == null || Roles.Contains(roleAtual);
        }
    }
}
