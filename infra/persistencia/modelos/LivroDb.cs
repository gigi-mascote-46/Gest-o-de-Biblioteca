namespace GestaoBlibioteca.infra.persistencia.modelos
{
    /// <summary>
    /// Representa um Livro proveniente da camada de persistência.
    /// Instâncias desta classe são criadas exclusivamente pelo Dapper.
    /// </summary>
    internal class LivroDb
    {
        public long Id { get; }
        public string Titulo { get; }
        public string Autor { get; }
        public string ISBN { get; }
        public long AnoPublicacao { get; }

        public LivroDb(long id, string titulo, string autor, string isbn, long anoPublicacao)
        {
            Id = id;
            Titulo = titulo;
            Autor = autor;
            ISBN = isbn;
            AnoPublicacao = anoPublicacao;
        }
    }
}
