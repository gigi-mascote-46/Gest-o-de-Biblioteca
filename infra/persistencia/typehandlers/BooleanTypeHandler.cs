//BooleanTypeHandler

using System;
using System.Data;
using Dapper;

namespace Biblioteca.Infraestrutura.Persistencia.TypeHandlers
{
    /// <summary>
    /// Manipulador de tipo para conversão entre INTEGER do SQLite e bool do C#.
    ///
    /// O SQLite não possui tipo BOOLEAN nativo. Valores booleanos são representados como:
    /// 0 (false) e 1 (true).
    ///
    /// Este TypeHandler garante conversão automática e segura entre os valores armazenados
    /// como INTEGER no banco e bool no domínio.
    /// </summary>
    public class BooleanTypeHandler : SqlMapper.TypeHandler<bool>
    {
        /// <summary>
        /// Converte valor do banco de dados (INTEGER) para bool.
        /// </summary>
        public override bool Parse(object value)
        {
            return Convert.ToInt32(value) != 0;
        }

        /// <summary>
        /// Converte valor bool do domínio para INTEGER no banco de dados.
        /// </summary>
        public override void SetValue(IDbDataParameter parameter, bool value)
        {
            parameter.Value = value ? 1 : 0;
            parameter.DbType = DbType.Int32;
        }
    }
}
