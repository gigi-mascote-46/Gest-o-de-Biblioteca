//Menu

using System;
using System.Collections.Generic;
using Biblioteca.UI.Componentes;

namespace Aplicacao.Menus
{
    /// <summary>
    /// Representa um menu contendo um título e uma lista de itens de menu.
    /// </summary>
    public class Menu
    {
        /// <summary>
        /// Título do menu, exibido ao usuário.
        /// </summary>
        public string Titulo { get; }

        /// <summary>
        /// Lista de itens de menu disponíveis neste menu.
        /// </summary>
        public List<MenuItem> Itens { get; }

        /// <summary>
        /// Delegate especial (sentinel) para representar a ação de retornar ao menu anterior.
        /// </summary>
        public static readonly Action AcaoVoltar = () => { };

        /// <summary>
        /// Cria uma nova instância da classe <see cref="Menu"/>.
        /// </summary>
        /// <param name="titulo">O título do menu.</param>
        /// <param name="itens">A lista de itens de menu.</param>
        /// <param name="incluirOpcaoVoltar">Se verdadeiro, adiciona uma opção "Voltar" ao final da lista.</param>
        public Menu(string titulo, List<MenuItem> itens, bool incluirOpcaoVoltar = false)
        {
            Titulo = titulo;
            Itens = itens ?? new List<MenuItem>();

            if (incluirOpcaoVoltar)
            {
                Itens.Add(new MenuItem("Voltar ao menu anterior", AcaoVoltar));
            }
        }
    }
}
