//MenuUtils

using System;
using System.Collections.Generic;
using System.Linq;
using Aplicacao.Menus;
using Biblioteca.Dominio.Enums;

namespace Biblioteca.UI.Componentes
{
    /// <summary>
    /// Classe utilitária para exibição e controle de menus baseados em permissões.
    /// </summary>
    public static class MenuUtils
    {
        /// <summary>
        /// Executa o menu completo: exibe opções filtradas por role e trata entrada do usuário,
        /// voltando automaticamente ao menu até que o usuário selecione a opção de retornar (se houver).
        /// </summary>
        /// <param name="menu">Menu a ser exibido</param>
        /// <param name="roleAtual">Role do usuário atual</param>
        public static void ExecutarMenuCompleto(Menu menu, Role roleAtual)
        {
            while (true)
            {
                var mapa = ExibirMenu(menu, roleAtual);
                Console.Write("Selecione uma opção: ");
                var entrada = Console.ReadLine();

                if (int.TryParse(entrada, out int opcao) && mapa.TryGetValue(opcao, out var acao))
                {
                    // Verifica se é a ação de voltar
                    if (acao == Menu.AcaoVoltar)
                    {
                        Console.WriteLine(); // Espaço visual ao voltar
                        return; // Sai do loop e retorna ao menu anterior
                    }

                    try
                    {
                        Console.WriteLine(); // Espaço antes de executar
                        acao();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao executar a ação: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Opção inválida. Tente novamente.");
                }

                Console.WriteLine(); // Espaço após execução ou erro
            }
        }

        /// <summary>
        /// Exibe o menu filtrado por role e retorna um dicionário mapeando opções numéricas para ações.
        /// </summary>
        /// <param name="menu">Instância de menu com título e itens</param>
        /// <param name="roleAtual">Role do usuário atual</param>
        /// <returns>Dicionário com número sequencial como chave e a ação correspondente como valor</returns>
        private static Dictionary<int, Action> ExibirMenu(Menu menu, Role roleAtual)
        {
            Console.WriteLine();
            Console.WriteLine($"== {menu.Titulo} ==");

            var itensFiltrados = menu.Itens.Where(i => i.PodeSerExibido(roleAtual)).ToList();
            var mapa = new Dictionary<int, Action>();
            int contador = 1;

            foreach (var item in itensFiltrados)
            {
                Console.WriteLine($"{contador}. {item.Texto}");
                mapa[contador] = item.Acao;
                contador++;
            }

            return mapa;
        }
    }
}
