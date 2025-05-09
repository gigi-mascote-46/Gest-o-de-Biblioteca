using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;



namespace GestaoBlibioteca.Dominio.Excecoes
{
    /// <summary>
    /// Exceção lançada quando um empréstimo não é encontrado no repositório.
    /// </summary>
    public class EmprestimoNaoEncontradoException : Exception
    {
     
    }
}
