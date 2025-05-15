using Biblioteca.Dominio.Modelos;

namespace GestaoBlibioteca.infra.persistencia.modelos
{
    /// <summary>
    /// Representa um Exemplar proveniente da camada de persistência.
    /// Instâncias desta classe são criadas exclusivamente pelo Dapper.
    /// </summary>
    internal sealed class ExemplarDb
    {
        public long Id { get; }
        public long LivroId{ get; }
        public bool SomenteLeituraLocal { get; }

        public ExemplarDb(long id, long livroId, bool somenteLeituraLocal)
        {
            Id = id;
            LivroId = livroId;  
            SomenteLeituraLocal = somenteLeituraLocal;
        }
    }
}
