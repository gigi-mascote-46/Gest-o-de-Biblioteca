//RegistradorDeTypeHandlers

using Dapper;
using Biblioteca.Dominio.Modelos;
using Biblioteca.Infraestrutura.Persistencia.TypeHandlers;

namespace Biblioteca.Infraestrutura.Persistencia
{
    /// <summary>
    /// Classe utilitária responsável por registrar os TypeHandlers personalizados do Dapper.
    ///
    /// Essa configuração garante que tipos do domínio como bool, DateTime (UTC) e enums
    /// como Role sejam corretamente convertidos para os tipos compatíveis do SQLite e vice-versa.
    ///
    /// Deve ser chamada uma única vez no início da aplicação.
    /// </summary>
    public static class RegistradorDeTypeHandlers
    {
        private static bool _registrado = false;

        /// <summary>
        /// Registra todos os TypeHandlers necessários.
        /// Garante que a operação seja executada apenas uma vez (idempotente).
        /// </summary>
        public static void RegistrarTodos()
        {
            if (_registrado) return;

            _registrado = true;

            SqlMapper.AddTypeHandler(new BooleanTypeHandler());
            SqlMapper.AddTypeHandler(new DateTimeUtcTypeHandler());
            SqlMapper.AddTypeHandler(new RoleTypeHandler());

        }
    }
}
