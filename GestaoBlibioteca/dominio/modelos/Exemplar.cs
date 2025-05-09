//Domain Model: Exemplar

namespace Biblioteca.Dominio.Modelos;

/// <summary>
/// Representa um exemplar físico de um livro.
/// Pode ser destinado apenas à leitura local (não emprestável).
/// Contém referência ao livro correspondente.
/// </summary>
public sealed class Exemplar
{
    public long Id { get; }
    public Livro Livro { get; }
    public bool SomenteLeituraLocal { get; }

    public Exemplar(long id, Livro livro, bool somenteLeituraLocal)
    {
        Livro = livro ?? throw new ArgumentNullException(nameof(livro), "Livro não pode ser nulo.");
        Id = id;
        SomenteLeituraLocal = somenteLeituraLocal;
    }

//     public override string ToString()
//           => $"Exemplar {Id} - {Livro.Titulo} {(SomenteLeituraLocal ? "(Leitura local)" : "")}";

}
