//IUnidadeDeTrabalho

namespace Biblioteca.Dominio;

/// <summary>
/// Representa uma unidade de trabalho, que encapsula o controle transacional
/// sobre múltiplas operações de escrita em repositórios.
/// </summary>
/// <remarks>
/// Essa interface permite que todas as mudanças feitas nos repositórios dentro de um mesmo
/// contexto sejam confirmadas ou revertidas em conjunto. É um padrão comum em aplicações
/// que usam repositórios, especialmente com ORMs como Entity Framework ou Dapper.
/// </remarks>
public interface IUnidadeDeTrabalho
{
    /// <summary>
    /// Inicia uma nova transação, caso o mecanismo subjacente dê suporte.
    /// </summary>
    void Iniciar();

    /// <summary>
    /// Efetiva todas as operações realizadas desde o início da transação.
    /// </summary>
    void Commitar();

    /// <summary>
    /// Desfaz todas as alterações realizadas desde o início da transação.
    /// </summary>
    void Reverter();


    /// <summary>
    /// Executa uma função no contexto de uma transação.
    /// ExecutarComTransacao é um método genérico que aceita uma função como parâmetro e vai ter como tipo de retorno o mesmo retornado pela função.
    /// O parâmetro genérico T é o tipo de retorno da função passada como parâmetro.
    /// Cada vez que o método for chamado, ele irá retornar o mesmo tipo de retorno da função passada como parâmetro.
    /// </summary>
    public T ExecutarComTransacao<T>(Func<T> func);
}
