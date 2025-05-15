using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace GestaoBlibioteca.Dominio.Excecoes
{
    /// <summary>
    /// Exceção lançada quando um exemplar não é encontrado no repositório.
    /// </summary>
    internal class ExemplarNaoEncontradoException : Exception
    {
        
    }
}
