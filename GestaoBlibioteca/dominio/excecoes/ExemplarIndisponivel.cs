using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;



namespace GestaoBlibioteca.Dominio.Excecoes
{
    /// <summary>
    /// Exceção lançada quando não há exemplares disponíveis para empréstimo.
    /// </summary>
    public class ExemplarIndisponivelException : Exception
    {
      
    }

}
