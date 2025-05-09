//LivroUI

using Biblioteca.UI.Componentes;
using Aplicacao;
using Biblioteca.Dominio.Enums;
using Aplicacao.Menus;
using Biblioteca.Dominio.Modelos;
using Biblioteca.Aplicacao.Servicos;
using GestaoBlibioteca.dominio.modelos;

namespace UI
{
    /// <summary>
    /// Interface de usuário para operações relacionadas a livros.
    /// Permite cadastro, listagem e busca por título, autor e ISBN.
    /// </summary>
    public class LivroUI
    {
        private readonly AppServiceLivros _appServiceLivros;
        private readonly AppServiceExemplares _appServiceExemplares;

        /// <summary>
        /// Inicializa uma nova instância de <see cref="LivroUI"/>.
        /// </summary>
        /// <param name="appServiceLivros">Serviço de aplicação responsável por operações com livros.</param>
        /// <param name="appServiceExemplares">Serviço de aplicação responsável por operações com exemplares.</param>
        public LivroUI(AppServiceLivros appServiceLivros)//, AppServiceExemplares appServiceExemplares)
        {
            _appServiceLivros = appServiceLivros;
            //_appServiceExemplares = appServiceExemplares;
        }

        /// <summary>
        /// Exibe o menu de operações relacionadas a livros.
        /// </summary>
        public void ExibirMenu(Role roleAtual)
        {
            var menu = new Menu("Menu de Livros", new List<MenuItem>
            {
                new MenuItem("Cadastrar Livro", CadastrarLivro, new[] { Role.Bibliotecario }),
                new MenuItem("Listar Todos os Livros", ListarLivros),
                new MenuItem("Buscar Livro por Título", BuscarPorTitulo),
                new MenuItem("Buscar Livro por Autor", BuscarPorAutor),
                new MenuItem("Buscar Livro por ISBN", BuscarPorISBN)
            }, incluirOpcaoVoltar:true);

            MenuUtils.ExecutarMenuCompleto(menu, roleAtual);
        }

        /// <summary>
        /// Solicita dados do livro ao usuário e realiza o cadastro.
        /// </summary>
        private void CadastrarLivro()
        {
            Console.Clear();
            Console.WriteLine("=== Cadastrar Livro ===");

            Console.Write("Título: ");
            var titulo = Console.ReadLine();

            Console.Write("Autor: ");
            var autor = Console.ReadLine();

            Console.Write("ISBN: ");
            var isbn = Console.ReadLine();

            Console.Write("Ano de publicação: ");
            if (!int.TryParse(Console.ReadLine(), out int anoPublicacao))
            {
                Console.WriteLine("Ano inválido.");
                return;
            }

            var livro = _appServiceLivros.Cadastrar(titulo, autor, isbn, anoPublicacao);
            Console.WriteLine($"Livro cadastrado com sucesso! ID: {livro.Id}");
        }

        /// <summary>
        /// Lista todos os livros cadastrados no sistema.
        /// </summary>
        private void ListarLivros()
        {
            Console.Clear();
            Console.WriteLine("=== Lista de Livros ===");

            var disponibilidade = _appServiceLivros.ListarTodos();
            if (!disponibilidade.Any())
            {
                Console.WriteLine("Nenhum livro encontrado.");
                return;
            }

            foreach (var item in disponibilidade)
            {
                ExibirDisponibilidade(item);
            }
        }

        /// <summary>
        /// Realiza busca por livros com base no título informado.
        /// </summary>
        private void BuscarPorTitulo()
        {
            Console.Clear();
            Console.WriteLine("=== Buscar Livro por Título ===");

            Console.Write("Título: ");
            var titulo = Console.ReadLine();

            var livros = _appServiceLivros.BuscarPorTitulo(titulo ?? "");

            if (!livros.Any())
            {
                Console.WriteLine("Nenhum livro encontrado com esse título.");
                return;
            }

            foreach (var livro in livros)
            {
                ExibirLivro(livro);
            }
        }

        /// <summary>
        /// Realiza busca por livros com base no autor informado.
        /// </summary>
        private void BuscarPorAutor()
        {
            Console.Clear();
            Console.WriteLine("=== Buscar Livro por Autor ===");

            Console.Write("Autor: ");
            var autor = Console.ReadLine();

            var livros = _appServiceLivros.BuscarPorAutor(autor ?? "");

            if (!livros.Any())
            {
                Console.WriteLine("Nenhum livro encontrado para esse autor.");
                return;
            }

            foreach (var livro in livros)
            {
                ExibirLivro(livro);
            }
        }

        /// <summary>
        /// Realiza busca por livro com base no ISBN informado.
        /// </summary>
        private void BuscarPorISBN()
        {
            Console.Clear();
            Console.WriteLine("=== Buscar Livro por ISBN ===");

            Console.Write("ISBN: ");
            var isbn = Console.ReadLine();

            var livro = _appServiceLivros.BuscarPorISBN(isbn ?? "");

            if (livro == null)
            {
                Console.WriteLine("Livro não encontrado.");
                return;
            }

            ExibirLivro(livro);
        }

        /// <summary>
        /// Exibe informações detalhadas de um livro.
        /// </summary>
        /// <param name="livro">Livro a ser exibido.</param>
        private void ExibirLivro(Livro livro, bool incluirSeparador = true)
        {
            Console.WriteLine($"ID: {livro.Id}");
            Console.WriteLine($"Título: {livro.Titulo}");
            Console.WriteLine($"Autor: {livro.Autor}");
            Console.WriteLine($"ISBN: {livro.ISBN}");
            Console.WriteLine($"Ano: {livro.AnoPublicacao}");
            
            if (incluirSeparador)
            {
                Console.WriteLine("--------------------------");
            }
        }

        /// <summary>
        /// Exibe informações detalhadas de um livro com a sua disponibilidade.
        /// </summary>
        private void ExibirDisponibilidade(Disponibilidade disponibilidade)
        {
            ExibirLivro(disponibilidade.Livro, false);

            Console.WriteLine($"Número de exemplares: {disponibilidade.NumExemplares}");
            Console.WriteLine($"Número de exemplares disponíveis para empréstimo: {disponibilidade.NumExemplaresDisponiveis}");
            Console.WriteLine("--------------------------");
        }
    }
}
