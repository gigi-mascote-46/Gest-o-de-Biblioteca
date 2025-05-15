using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GestaoBlibioteca.Dominio.Excecoes
{
    /// <summary>
    /// Exceção lançada quando um exemplar está disponível apenas para leitura local e não pode ser emprestado.
    /// </summary>
    public class ExemplarSomenteLeituraLocalException : Exception
    {
      
    }

}
