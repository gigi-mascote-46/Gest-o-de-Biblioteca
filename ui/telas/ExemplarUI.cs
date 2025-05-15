//ExemplarUI

using System;
using System.Collections.Generic;
using Aplicacao.Menus;
using Aplicacao;
using Biblioteca.UI.Componentes;
using Biblioteca.Aplicacao.Servicos;
using Biblioteca.Dominio.Enums;
using System.ComponentModel;

namespace Biblioteca.UI.Telas
{
    /// <summary>
    /// Interface de usuário para operações relacionadas a exemplares de livros.
    /// Permite cadastrar novos exemplares e atualizar seu modo de leitura.
    /// </summary>
    public class ExemplarUI
    {
        private readonly AppServiceExemplares _appServiceExemplares;

        /// <summary>
        /// Inicializa uma nova instância de <see cref="ExemplarUI"/>.
        /// </summary>
        /// <param name="appServiceExemplares">Serviço de aplicação responsável por operações com exemplares.</param>
        public ExemplarUI(AppServiceExemplares appServiceExemplares)
        {
            _appServiceExemplares = appServiceExemplares;
        }

        /// <summary>
        /// Exibe o menu de operações relacionadas a exemplares.
        /// </summary>
        /// <param name="roleAtual">Role do usuário logado.</param>
        public void ExibirMenu(Role roleAtual)
        {
            var menu = new Menu("Menu de Exemplares", new List<MenuItem>
            {
                new MenuItem("Cadastrar Exemplar", CadastrarExemplar, new[] { Role.Bibliotecario }),
                new MenuItem("Atualizar Modo de Leitura", AtualizarModoDeLeitura, new[] { Role.Bibliotecario })
            }, incluirOpcaoVoltar: true);

            MenuUtils.ExecutarMenuCompleto(menu, roleAtual);
        }

        /// <summary>
        /// Solicita dados ao usuário e cadastra um novo exemplar vinculado a um livro.
        /// </summary>
        private void CadastrarExemplar()
        {
            Console.Clear();
            Console.WriteLine("=== Cadastrar Exemplar ===");

            var livroId = ConsoleEx.LerLong("Digite o ID do livro:");
           

            Console.Write("Somente leitura local (s/n): ");
            var leituraLocal = Console.ReadLine()?.Trim().ToLower() == "s";

            var exemplar = _appServiceExemplares.Cadastrar(livroId, leituraLocal);
            Console.WriteLine($"Exemplar cadastrado com sucesso! ID: {exemplar.Id}");
        }

        /// <summary>
        /// Solicita dados ao usuário e atualiza o modo de leitura de um exemplar.
        /// </summary>
        private void AtualizarModoDeLeitura()
        {
            Console.Clear();
            Console.WriteLine("=== Atualizar Modo de Leitura ===");

           var exemplarId = ConsoleEx.LerLong("Digite o ID do exemplar:");

            Console.Write("Novo modo: somente leitura local? (s/n): ");
            bool leituraLocal = Console.ReadLine()?.Trim().ToLower() == "s";

            var atualizado = _appServiceExemplares.AtualizarModoDeLeitura(exemplarId, leituraLocal);
            Console.WriteLine("Modo de leitura atualizado com sucesso.");
        }
    }
}
