//EmprestimoUI

using Aplicacao;
using Aplicacao.Menus;
using Biblioteca.Aplicacao.Servicos;
using Biblioteca.Dominio.Enums;
using Biblioteca.Dominio.Modelos;
using Biblioteca.UI.Componentes;
using UI;


namespace Biblioteca.UI.Telas
{
    /// <summary>
    /// Interface de usuário para operações de empréstimo de livros.
    /// Permite realizar empréstimos, registrar devoluções e listar empréstimos por usuário.
    /// </summary>
    public class EmprestimoUI
    {
        private readonly AppServiceEmprestimos _appServiceEmprestimos;
        private readonly Sessao _sessao;

        /// <summary>
        /// Inicializa uma nova instância de <see cref="EmprestimoUI"/>.
        /// </summary>
        /// <param name="appServiceEmprestimos">Serviço de aplicação responsável por empréstimos.</param>
        public EmprestimoUI(AppServiceEmprestimos appServiceEmprestimos, Sessao sessao)
        {
            _appServiceEmprestimos = appServiceEmprestimos;
            _sessao = sessao;
        }

        /// <summary>
        /// Exibe o menu de operações de empréstimo.
        /// </summary>
        public void ExibirMenu(Role roleAtual)
        {
            var menu = new Menu("Menu de Empréstimos", new List<MenuItem>
            {
                new MenuItem("Realizar Empréstimo", RealizarEmprestimo, new[] { Role.Bibliotecario }),
                new MenuItem("Registrar Devolução", RegistrarDevolucao, new[] { Role.Bibliotecario }),
                new MenuItem("Listar Empréstimos por Usuário", ListarPorUsuario, new[] { Role.Bibliotecario }), 
                new MenuItem("Listar Meus Empréstimos", ListarMeusEmprestimos),
                new MenuItem("Listar Todos Empréstimos", ListarTodosEmprestimos)
            }, incluirOpcaoVoltar: true);

            MenuUtils.ExecutarMenuCompleto(menu, roleAtual);
        }

        /// <summary>
        /// Solicita os dados e realiza o empréstimo de um livro.
        /// </summary>
        private void RealizarEmprestimo()
        {
            Console.Clear();
            Console.WriteLine("=== Realizar Empréstimo ===");

            var livroId = ConsoleEx.LerLong("ID do Livro: ");
            var usuarioId = ConsoleEx.LerLong("ID do Usuário: ");

            try
            {
                var emprestimo = _appServiceEmprestimos.RealizarEmprestimo(livroId, usuarioId);
                Console.WriteLine($"Empréstimo realizado com sucesso! ID: {emprestimo.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao realizar empréstimo: {ex.Message}");
            }
        }

        /// <summary>
        /// Solicita o ID do empréstimo e registra a devolução.
        /// </summary>
        private void RegistrarDevolucao()
        {
            Console.Clear();
            Console.WriteLine("=== Registrar Devolução ===");

            var emprestimoId = ConsoleEx.LerLong("ID do Empréstimo: ");

            try
            {
                var emprestimo = _appServiceEmprestimos.RegistrarDevolucao(emprestimoId);
                
                if (emprestimo.FoiDevolvidoAtrasado)
                {
                    Console.WriteLine("A devolução foi realizada com atraso.");
                }

                Console.WriteLine("Devolução registrada com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao registrar devolução: {ex.Message}");
            }
        }

        /// <summary>
        /// Lista todos os empréstimos associados a um usuário.
        /// </summary>
        private void ListarPorUsuario()
        {
            Console.Clear();
            Console.WriteLine("=== Empréstimos por Usuário ===");

            var usuarioId = ConsoleEx.LerLong("ID do Usuário: ");

            try
            {
                var emprestimos = _appServiceEmprestimos.ListarPorUsuario(usuarioId);

                if (!emprestimos.Any())
                {
                    Console.WriteLine("Nenhum empréstimo encontrado para este usuário.");
                    return;
                }

                foreach (var item in emprestimos)
                {
                    ExibirEmprestimo(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar empréstimos: {ex.Message}");
            }
        }
        /// <summary>
        /// Lista todos os empréstimos associados ao usuário atual.
        /// </summary>
        private void ListarMeusEmprestimos()
        {
            Console.Clear();
            Console.WriteLine("=== Meus Empréstimos ===");

            var usuarioId = _sessao.UsuarioAtual.Id;

            try
            {
                var emprestimos = _appServiceEmprestimos.ListarPorUsuario(usuarioId);

                if (!emprestimos.Any())
                {
                    Console.WriteLine("Nenhum empréstimo encontrado para este usuário.");
                    return;
                }

                foreach (var item in emprestimos)
                {
                    ExibirEmprestimo(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar empréstimos: {ex.Message}");
            }
        }

        private void ListarTodosEmprestimos()
        {
            Console.Clear();
            Console.WriteLine("=== Todos os Empréstimos ===");
            try
            {
                var emprestimos = _appServiceEmprestimos.ListarTodos();
                if (!emprestimos.Any())
                {
                    Console.WriteLine("Nenhum empréstimo encontrado.");
                    return;
                }
                foreach (var item in emprestimos)
                {
                    ExibirEmprestimo(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar empréstimos: {ex.Message}");
            }
        }

        /// <summary>
        /// Exibe os detalhes de um empréstimo.
        /// </summary>
        /// <param name="emprestimo">Objeto de empréstimo a ser exibido.</param>
        private void ExibirEmprestimo(Emprestimo emprestimo)
        {
            Console.WriteLine($"ID: {emprestimo.Id}");
            Console.WriteLine($"ID do Exemplar: {emprestimo.Exemplar.Id}");
            Console.WriteLine($"Título do Livro: {emprestimo.Exemplar.Livro.Titulo}");
            Console.WriteLine($"ID do Usuário: {emprestimo.Usuario.Id}");
            Console.WriteLine($"Nome do Usuário: {emprestimo.Usuario.Nome}");
            Console.WriteLine($"Data do Empréstimo: {emprestimo.DataEmprestimo:dd/MM/yyyy}");
            Console.WriteLine($"Data Limite de Devolução: {emprestimo.DataLimiteDevolucao:dd/MM/yyyy}");
            Console.WriteLine($"Data de Devolução: {(emprestimo.DataDevolucao.HasValue ? emprestimo.DataDevolucao.Value.ToString("dd/MM/yyyy") : "Ainda não devolvido")}");
            Console.WriteLine($"Estado: {emprestimo.Estado}");
            Console.WriteLine("-----------------------------------");
        }
    }
}
