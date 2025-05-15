using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace GestaoBlibioteca.Dominio.Excecoes
{
    /// <summary>
    /// Exceção lançada quando se tenta registrar a devolução de um empréstimo que já foi devolvido anteriormente.
    /// </summary>
    /// 

    public class EmprestimoJaDevolvidoException : Exception
    {
      
    }
}
