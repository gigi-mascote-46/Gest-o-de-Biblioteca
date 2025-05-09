// Program.cs

using System;
using Biblioteca.Infraestrutura.IoC;
using Biblioteca.Infraestrutura.Persistencia;
using Infra;
using UI;
using System;
using Aplicacao;
using Biblioteca.UI;
using Biblioteca.UI.Telas;

namespace Biblioteca
{
    /// <summary>
    /// Classe principal do sistema. Responsável por iniciar a aplicação.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Iniciando sistema da biblioteca...");

            // Inicializa manualmente todas as dependências da aplicação
            var uiController = ConfiguracaoIoC.Inicializar();

            // Inicia o fluxo principal da aplicação
            uiController.Iniciar();

            Console.WriteLine("Sistema finalizado.");
        }
    }
}

