//ConsoleEx

using System;

namespace Biblioteca.UI.Componentes
{
    /// <summary>
    /// Classe utilitária para facilitar entrada de dados no console com validação.
    /// </summary>
    public static class ConsoleEx
    {
        /// <summary>
        /// Solicita ao usuário a entrada de uma string, exibindo um prompt personalizado.
        /// A entrada é considerada inválida se estiver vazia ou contiver apenas espaços em branco.
        /// Pode ser fornecida uma função de validação extra para aplicar regras adicionais.
        /// </summary>
        /// <param name="prompt">Mensagem exibida ao usuário solicitando a entrada.</param>
        /// <param name="validacaoExtra">
        /// (Opcional) Função que recebe a string digitada e retorna true se for válida.
        /// Caso contrário, a entrada será rejeitada e solicitada novamente.
        /// </param>
        /// <returns>Uma string não vazia e que passou pelas validações especificadas.</returns>
        public static string LerTexto(string prompt, Func<string, bool>? validacaoExtra = null)
        {
            while (true)
            {
                Console.Write(prompt);
                var entrada = Console.ReadLine() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(entrada))
                {
                    Console.WriteLine("Entrada não pode estar vazia.");
                    continue;
                }

                if (validacaoExtra != null && !validacaoExtra(entrada))
                {
                    Console.WriteLine("Valor inválido.");
                    continue;
                }

                return entrada;
            }
        }

        /// <summary>
        /// Solicita ao usuário a entrada de um número inteiro (int), exibindo um prompt personalizado.
        /// Caso a entrada não seja um inteiro válido, o usuário será avisado e solicitado novamente.
        /// Permite validação adicional através de função opcional.
        /// </summary>
        /// <param name="prompt">Mensagem exibida ao usuário solicitando a entrada.</param>
        /// <param name="validacaoExtra">
        /// (Opcional) Função que recebe o inteiro digitado e retorna true se for válido.
        /// Se a validação falhar, o usuário será solicitado a inserir novamente.
        /// </param>
        /// <returns>Um número inteiro que passou pelas validações.</returns>
        public static int LerInt(string prompt, Func<int, bool>? validacaoExtra = null)
        {
            while (true)
            {
                Console.Write(prompt);
                var entrada = Console.ReadLine();

                if (!int.TryParse(entrada, out var valor))
                {
                    Console.WriteLine("Número inválido.");
                    continue;
                }

                if (validacaoExtra != null && !validacaoExtra(valor))
                {
                    Console.WriteLine("Valor inválido.");
                    continue;
                }

                return valor;
            }
        }


        /// <summary>
        /// Solicita ao usuário a entrada de um número inteiro (long), exibindo um prompt personalizado.
        /// Caso a entrada não seja um inteiro válido, o usuário será avisado e solicitado novamente.
        /// Permite validação adicional através de função opcional.
        /// </summary>
        /// <param name="prompt">Mensagem exibida ao usuário solicitando a entrada.</param>
        /// <param name="validacaoExtra">
        /// (Opcional) Função que recebe o inteiro digitado e retorna true se for válido.
        /// Se a validação falhar, o usuário será solicitado a inserir novamente.
        /// </param>
        /// <returns>Um número inteiro que passou pelas validações.</returns>
        public static long LerLong(string prompt, Func<long, bool>? validacaoExtra = null)
        {
            while (true)
            {
                Console.Write(prompt);
                var entrada = Console.ReadLine();

                if (!long.TryParse(entrada, out var valor))
                {
                    Console.WriteLine("Número inválido.");
                    continue;
                }

                if (validacaoExtra != null && !validacaoExtra(valor))
                {
                    Console.WriteLine("Valor inválido.");
                    continue;
                }

                return valor;
            }
        }

        /// <summary>
        /// Solicita ao usuário a entrada de uma data (DateTime), exibindo um prompt personalizado.
        /// Aceita qualquer formato de data válido segundo a cultura atual do sistema.
        /// Caso a entrada não possa ser convertida para DateTime, será solicitada novamente.
        /// Uma função de validação personalizada pode ser utilizada para restringir datas válidas.
        /// </summary>
        /// <param name="prompt">Mensagem exibida ao usuário solicitando a entrada.</param>
        /// <param name="validacaoExtra">
        /// (Opcional) Função que recebe a data digitada e retorna true se for válida.
        /// A entrada será solicitada novamente até que atenda aos critérios.
        /// </param>
        /// <returns>Um objeto DateTime válido.</returns>
        public static DateTime LerDateTime(string prompt, Func<DateTime, bool>? validacaoExtra = null)
        {
            while (true)
            {
                Console.Write(prompt);
                var entrada = Console.ReadLine();

                if (!DateTime.TryParse(entrada, out var valor))
                {
                    Console.WriteLine("Data inválida.");
                    continue;
                }

                if (validacaoExtra != null && !validacaoExtra(valor))
                {
                    Console.WriteLine("Valor inválido.");
                    continue;
                }

                return valor;
            }
        }

        /// <summary>
        /// Solicita ao usuário a entrada de um valor booleano (sim/não), com validação e prompt personalizado.
        /// Aceita múltiplas variações de entrada: "s", "n", "sim", "não", "nao", "true", "false".
        /// A entrada será convertida para true ou false, e pode ser validada com uma função adicional.
        /// </summary>
        /// <param name="prompt">Mensagem exibida ao usuário solicitando a entrada (será complementada com "(s/n)").</param>
        /// <param name="validacaoExtra">
        /// (Opcional) Função que recebe o valor booleano e retorna true se for válido.
        /// Caso contrário, o usuário será solicitado a inserir novamente.
        /// </param>
        /// <returns>Um valor booleano válido (true para "sim", false para "não").</returns>
        public static bool LerBool(string prompt, Func<bool, bool>? validacaoExtra = null)
        {
            while (true)
            {
                Console.Write(prompt + " (s/n): ");
                var entrada = (Console.ReadLine() ?? string.Empty).Trim().ToLower();

                bool? valor = entrada switch
                {
                    //"s" or "sim" or "true" => true,
                    //"n" or "nao" or "não" or "false" => false,
                    "s" => true,
                    "n" => false,
                    _ => null
                };

                if (valor == null)
                {
                    Console.WriteLine("Entrada inválida. Use 's' para sim ou 'n' para não.");
                    continue;
                }

                if (validacaoExtra != null && !validacaoExtra(valor.Value))
                {
                    Console.WriteLine("Valor inválido.");
                    continue;
                }

                return valor.Value;
            }
        }
    }
}
