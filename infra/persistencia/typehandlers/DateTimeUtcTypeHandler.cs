//DateTimeUtcTypeHandler

using System;
using System.Data;
using Dapper;

namespace Biblioteca.Infraestrutura.Persistencia.TypeHandlers
{
    /// <summary>
    /// Manipulador de tipo para conversão entre TEXT (formato ISO 8601 UTC) e DateTime.
    ///
    /// O SQLite armazena datas como TEXT. Este TypeHandler garante que os valores
    /// sejam convertidos corretamente entre DateTime (em UTC) e string ISO 8601.
    /// </summary>
    public class DateTimeUtcTypeHandler : SqlMapper.TypeHandler<DateTime>
    {
        private const string Iso8601Format = "yyyy-MM-ddTHH:mm:ssZ";

        /// <summary>
        /// Converte valor do banco de dados (string ISO 8601) para DateTime com Kind = Utc.
        /// </summary>
        public override DateTime Parse(object value)
        {
            if (value is string str)
            {
                return DateTime.SpecifyKind(DateTime.Parse(str), DateTimeKind.Utc);
            }

            throw new DataException($"Não foi possível converter '{value}' para DateTime (esperado string).");
        }

        /// <summary>
        /// Converte DateTime do domínio para string ISO 8601 (UTC) para persistência.
        /// </summary>
        public override void SetValue(IDbDataParameter parameter, DateTime value)
        {
            if (value.Kind != DateTimeKind.Utc)
            {
                // Garante que estamos persistindo sempre em UTC
                value = DateTime.SpecifyKind(value, DateTimeKind.Utc);
            }

            parameter.Value = value.ToString(Iso8601Format);
            parameter.DbType = DbType.String;
        }
    }
}
