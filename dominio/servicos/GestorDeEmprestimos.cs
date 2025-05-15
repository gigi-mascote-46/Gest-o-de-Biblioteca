//GestorDeEmprestimos

using Biblioteca.Dominio.Modelos;

namespace Biblioteca.Dominio.Servicos;

/// <summary>
/// Serviço de domínio responsável pela criação e controle de empréstimos de exemplares, gerenciamdp o seu ciclo de vida.
/// Aplica regras relacionadas à política de empréstimos, como a criação e a devolução de empréstimos,
/// sempre retornando instâncias imutáveis de <see cref="Emprestimo"/>.
/// </summary>
public class GestorDeEmprestimos
{
    /// <summary>
    /// Cria um novo empréstimo de um exemplar de um livro para um usuário.
    /// </summary>
    /// <param name="exemplar">Exemplar a ser emprestado.</param>
    /// <param name="usuario">Usuário que realizará o empréstimo.</param>
    /// <param name="dataEmprestimo">Data do empréstimo.</param>
    /// <returns>Nova instância de <see cref="Emprestimo"/> representando o empréstimo criado.</returns>
    /// <exception cref="InvalidOperationException">
    /// Lançada se o exemplar for de leitura local ou se já estiver emprestado.
    /// </exception>
    public Emprestimo Criar(Exemplar exemplar, Usuario usuario, DateTime dataEmprestimo)
    {
        if (exemplar.SomenteLeituraLocal)
            throw new InvalidOperationException("Este exemplar é apenas para leitura local e não pode ser emprestado.");

        // Aqui poderíamos incluir futuras verificações como:
        // - Limite de empréstimos ativos por usuário
        // - Restrições de horário ou perfil

        return new Emprestimo(
            id: 0, // Será atribuído pela camada de persistência
            usuario: usuario,
            exemplar: exemplar,
            dataEmprestimo: dataEmprestimo,
            dataLimiteDevolucao: dataEmprestimo.AddDays(3), // Exemplo: 7 dias para devolução
            dataDevolucao: null
        );
    }

    /// <summary>
    /// Marca um empréstimo como devolvido, retornando uma nova instância com a data de devolução registrada com a data atual.
    /// </summary>
    /// <param name="emprestimo">Empréstimo original.</param>
    /// <returns>Nova instância de <see cref="Emprestimo"/> com a data de devolução preenchida.</returns>
    /// <exception cref="InvalidOperationException">
    /// Lançada se o empréstimo já estiver marcado como devolvido.
    /// </exception>
    public Emprestimo MarcarComoDevolvido(Emprestimo emprestimo)
    {
        if (emprestimo == null)
            throw new ArgumentNullException(nameof(emprestimo));

        if (emprestimo.DataDevolucao.HasValue)
            throw new InvalidOperationException("Este empréstimo já foi devolvido.");

        var dataDevolucao = DateTime.UtcNow;

        return new Emprestimo(
         id: emprestimo.Id,
         usuario: emprestimo.Usuario, // Passa o objeto Usuario
         exemplar: emprestimo.Exemplar, // Passa o objeto Exemplar
         dataEmprestimo: emprestimo.DataEmprestimo,
         dataLimiteDevolucao: emprestimo.DataLimiteDevolucao, // Adiciona o campo obrigatório
         dataDevolucao: dataDevolucao
  );
    }
}
