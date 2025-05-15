//Domain Model: Livro

namespace Biblioteca.Dominio.Modelos;

/// <summary>
/// Representa um livro na biblioteca. Essa entidade é imutável.
/// Utiliza ID inteiro como surrogate key. ISBN é um identificador natural.
/// </summary>
public sealed class Livro
  {
      public long Id { get; }
      public string Titulo { get; }
      public string Autor { get; }
      public string ISBN { get; }
      public long AnoPublicacao { get; }

      public Livro(long id, string titulo, string autor, string isbn, long anoPublicacao)
      {
          if (string.IsNullOrWhiteSpace(titulo))
              throw new ArgumentException("Título é obrigatório.", nameof(titulo));

          if (string.IsNullOrWhiteSpace(autor))
              throw new ArgumentException("Autor é obrigatório.", nameof(autor));

          if (string.IsNullOrWhiteSpace(isbn))
              throw new ArgumentException("ISBN é obrigatório.", nameof(isbn));

          if (anoPublicacao <= 0)
              throw new ArgumentOutOfRangeException(nameof(anoPublicacao), "Ano de publicação deve ser positivo.");

          Id = id;
          Titulo = titulo;
          Autor = autor;
          ISBN = isbn;
          AnoPublicacao = anoPublicacao;
      }

//    public override string ToString() => $"{Titulo} ({AnoPublicacao}) - ISBN: {ISBN}";
  }
