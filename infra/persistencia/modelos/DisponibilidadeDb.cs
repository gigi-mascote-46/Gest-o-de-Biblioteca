using Biblioteca.Dominio.Modelos;

namespace GestaoBlibioteca.infra.persistencia.modelos
{
    /// <summary>
    /// Representa um Exemplar proveniente da camada de persistência.
    /// Instâncias desta classe são criadas exclusivamente pelo Dapper.
    /// </summary>
    internal sealed class DisponibilidadeDb
    {
        public long LivroId { get; }
        public int NumExemplares { get; }
        public int NumExemplaresDisponiveis { get; }

        public DisponibilidadeDb(long livroId, int numExemplares, int numExemplaresDisponiveis)
        {
            LivroId = livroId;
            NumExemplares = numExemplares;
            NumExemplaresDisponiveis = numExemplaresDisponiveis;
        }
    }
}
