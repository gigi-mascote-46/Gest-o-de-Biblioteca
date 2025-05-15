using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GestaoBlibioteca.Dominio.Excecoes
{
    /// <summary>
    /// Exceção lançada quando um usuário atinge o limite máximo permitido de empréstimos simultâneos.
    /// </summary>
    public class UsuarioAtingiuLimiteEmprestimosException : Exception
    {
     
    }
}
