//Domain Model: Emprestimo

namespace Biblioteca.Dominio.Modelos;

/// <summary>
/// Representa o empréstimo de um exemplar a um usuário.
/// O exemplar é indisponível até a efetiva devolução (DataDevolucao preenchida).
/// </summary>
public sealed class Emprestimo
{
    public long Id { get; }
    public Usuario Usuario { get; }
    public Exemplar Exemplar { get; }
    public DateTime DataEmprestimo { get; }
    public DateTime DataLimiteDevolucao { get; }
    public DateTime? DataDevolucao { get; }

    public Emprestimo(
        long id,
        Usuario usuario,
        Exemplar exemplar,
        DateTime dataEmprestimo,
        DateTime dataLimiteDevolucao,
        DateTime? dataDevolucao = null)
    {
        Usuario = usuario ?? throw new ArgumentNullException(nameof(usuario));
        Exemplar = exemplar ?? throw new ArgumentNullException(nameof(exemplar));

        // TODO: Adicionar a mesma validação para as outras datas.
        if (dataEmprestimo == default)
            throw new ArgumentException("Data de empréstimo inválida.");

        if (dataEmprestimo.Kind != DateTimeKind.Utc)
            throw new ArgumentException("A data de empréstimo deve estar em UTC.", nameof(dataEmprestimo));

        if (dataLimiteDevolucao.Kind != DateTimeKind.Utc)
            throw new ArgumentException("A data limite de devolução deve estar em UTC.", nameof(dataLimiteDevolucao));

        if (dataLimiteDevolucao < dataEmprestimo)
            throw new ArgumentException("A data limite de devolução deve ser posterior à data do empréstimo.");

        if (dataDevolucao.HasValue && dataDevolucao.Value.Kind != DateTimeKind.Utc)
            throw new ArgumentException("A data de devolução, se fornecida, deve estar em UTC.", nameof(dataDevolucao));

        Id = id;
        DataEmprestimo = dataEmprestimo;
        DataLimiteDevolucao = dataLimiteDevolucao;
        DataDevolucao = dataDevolucao;
    }

    public bool EstaDevolvido => DataDevolucao.HasValue;

    /// <summary>
    /// Verifica se o empréstimo está atrasado.
    /// </summary>
    /// <returns>True se o empréstimo está atrasado, false caso contrário.</returns>
    public bool EstaAtrasado => DataLimiteDevolucao < DateTime.UtcNow && !EstaDevolvido;

    /// <summary>
    /// Verifica se o empréstimo foi devolvido depois do prazo.
    /// </summary>
    /// <returns>True se o empréstimo foi devolvido depois do prazo, false caso contrário.</returns>
    public bool FoiDevolvidoAtrasado => EstaDevolvido && DataDevolucao > DataLimiteDevolucao;

    /// <summary>
    /// Retorna o estado atual do empréstimo.
    /// </summary>
    /// <returns>Uma string representando o estado do empréstimo: "Emprestado", "Atrasado" ou "Devolvido".</returns>
    public string Estado
    {
        get
        {
            if (DataDevolucao != null)
                return "Devolvido";
            if (EstaAtrasado)
                return "Atrasado";
            return "Emprestado";
        }
    }

    //    public override string ToString()
    //        => $"Empréstimo do exemplar {Exemplar.Id} para {Usuario.Username} em {DataEmprestimo:dd/MM/yyyy}" +
    //           (EstaDevolvido ? $" (Devolvido em {DataDevolucao:dd/MM/yyyy})" : $" (Limite: {DataLimiteDevolucao:dd/MM/yyyy})");
}
