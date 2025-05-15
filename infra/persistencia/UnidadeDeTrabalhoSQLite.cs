//UnidadeDeTrabalhoSQLite

using Dapper;
using System;
using System.Data;
using Microsoft.Data.Sqlite;
using Biblioteca.Dominio;
using Biblioteca.Dominio.Repositorios;
using Biblioteca.Dominio.Servicos;

namespace Biblioteca.Infraestrutura.Persistencia
{
    /// <summary>
    /// Implementação concreta de IUnidadeDeTrabalho utilizando SQLite e Dapper.
    /// Essa classe gerencia o ciclo de vida de uma transação, garantindo que todas as operações sejam atômicas.
    /// </summary>
    /// <remarks>
    /// Esta classe não é thread-safe. Cada instância deve ser usada por uma única thread.
    /// Idealmente, deve ter ciclo de vida Scoped ou Transient.
    /// </remarks>
    public class UnidadeDeTrabalhoSQLite : IUnidadeDeTrabalho
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;

        /// <summary>
        /// Construtor que recebe uma conexão do tipo SqliteConnection.
        /// </summary>
        /// <param name="connection">Conexão com o banco de dados SQLite.</param>
        public UnidadeDeTrabalhoSQLite(IDbConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        /// <summary>
        /// Inicia uma nova transação no banco de dados.
        /// </summary>
        public void Iniciar()
        {
            if (_transaction != null)
            {
                throw new InvalidOperationException("A transação já foi iniciada.");
            }

            _transaction = _connection.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// Efetua o commit da transação, confirmando todas as alterações no banco de dados.
        /// </summary>
        public void Commitar()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Nenhuma transação foi iniciada.");
            }

            _transaction.Commit();
            _transaction = null;
        }

        /// <summary>
        /// Efetua o rollback da transação, revertendo todas as alterações realizadas.
        /// </summary>
        public void Reverter()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Nenhuma transação foi iniciada.");
            }

            _transaction.Rollback();
            _transaction = null;
        }

        public T ExecutarComTransacao<T>(Func<T> func)
        {
            try
            {
                Iniciar();
                var resultado = func();
                Commitar();

                return resultado;
            }
            catch (Exception ex)
            {
                Reverter();
                throw ex;
            }
        }
    }
}
