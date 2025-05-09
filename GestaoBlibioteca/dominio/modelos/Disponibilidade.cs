using Biblioteca.Dominio.Modelos;

namespace GestaoBlibioteca.dominio.modelos
{
    /// <summary>
    /// Contém informação sobre a disponibilidade de um livro.
    /// </summary>
    public sealed class Disponibilidade
    {
        public Livro Livro { get; }
        public long NumExemplares { get; }
        public long NumExemplaresDisponiveis { get; }

    public Disponibilidade(Livro livro, long numExemplares, long numExemplaresDisponiveis)
        {
            Livro = livro;
            NumExemplares = numExemplares;
            NumExemplaresDisponiveis = numExemplaresDisponiveis;
        }
    }
}