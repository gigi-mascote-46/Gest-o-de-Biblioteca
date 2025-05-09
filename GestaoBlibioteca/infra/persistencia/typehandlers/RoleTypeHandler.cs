//RoleTypeHandler

using System;
using System.Data;
using Dapper;
using Biblioteca.Dominio.Modelos;
using Biblioteca.Dominio.Enums;

// TODO: Desnecessário, remover.

namespace Biblioteca.Infraestrutura.Persistencia.TypeHandlers
{
    /// <summary>
    /// Manipulador de tipo para a enumeração Role.
    ///
    /// Este handler converte entre strings armazenadas no banco ("Leitor", "Bibliotecario")
    /// e a enum Role no domínio.
    /// </summary>
    public class RoleTypeHandler : SqlMapper.TypeHandler<Role>
    {
        /// <summary>
        /// Converte string vinda do banco para enum Role.
        /// </summary>
        public override Role Parse(object value)
        {
            if (value is string str && Enum.TryParse<Role>(str, ignoreCase: true, out var role))
            {
                return role;
            }

            throw new DataException($"Valor inválido para Role: '{value}'");
        }

        /// <summary>
        /// Converte enum Role para string ao persistir no banco.
        /// </summary>
        public override void SetValue(IDbDataParameter parameter, Role value)
        {
            parameter.Value = value.ToString();
            parameter.DbType = DbType.String;
        }
    }
}
