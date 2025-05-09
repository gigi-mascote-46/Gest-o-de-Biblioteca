using Biblioteca.Dominio.Modelos;

namespace GestaoBlibioteca.infra.persistencia.modelos
{
    /// <summary>
    /// Representa um Emprestimo proveniente da camada de persistência.
    /// Instâncias desta classe são criadas exclusivamente pelo Dapper.
    /// </summary>
    internal sealed class EmprestimoDb
    {
        public long Id { get; }
        public long UsuarioId { get; }
        public long ExemplarId { get; }
        public DateTime DataEmprestimo { get; }
        public DateTime DataLimiteDevolucao { get; }
        public DateTime? DataDevolucao { get; }

        public EmprestimoDb(long id, long usuarioId, long exemplarId, string dataEmprestimo, string dataLimiteDevolucao, string? dataDevolucao)
        {
            Id = id;
            UsuarioId = usuarioId;
            ExemplarId = exemplarId;
            DataEmprestimo = DateTime.SpecifyKind(DateTime.Parse(dataEmprestimo), DateTimeKind.Utc);
            DataLimiteDevolucao = DateTime.SpecifyKind(DateTime.Parse(dataLimiteDevolucao), DateTimeKind.Utc);

            if (dataDevolucao != null ) {
            DataDevolucao = DateTime.SpecifyKind(DateTime.Parse(dataDevolucao), DateTimeKind.Utc);
            }
        }
    }
}
