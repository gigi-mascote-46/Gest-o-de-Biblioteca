//FabricaDeConexaoSQLite

using System;
using System.Data;
using Microsoft.Data.Sqlite;
using SQLitePCL;

namespace Biblioteca.Infraestrutura.Persistencia
{
    /// <summary>
    /// Fabrica responsável por criar conexões SQLite.
    /// Encapsula a configuração da connection string e facilita testes e injeção de dependência.
    /// </summary>
    public class FabricaDeConexaoSQLite
    {
        private readonly string _connectionString;

        /// <summary>
        /// Cria uma nova instância da fábrica com base em um caminho para o arquivo de banco de dados.
        /// </summary>
        /// <param name="caminhoArquivo">Caminho para o arquivo .sqlite.</param>
        public FabricaDeConexaoSQLite(string caminhoArquivo)
        {
            if (string.IsNullOrWhiteSpace(caminhoArquivo))
                throw new ArgumentException("Caminho para o arquivo do banco de dados não pode ser nulo ou vazio.", nameof(caminhoArquivo));

        // TODO: enable foreign key constraints
        // https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/connection-strings
            _connectionString = $"Data Source={caminhoArquivo};Cache=Shared;Mode=ReadWriteCreate;";
        }

        /// <summary>
        /// Cria uma instância da fábrica com banco em memória.
        /// Útil para testes ou cenários temporários.
        /// </summary>
        /// <returns>Instância da fábrica conectada a um banco em memória e aberto.</returns>
        public static FabricaDeConexaoSQLite CriarEmMemoria()
        {
            return new FabricaDeConexaoSQLite(":memory:");
        }

        /// <summary>
        /// Cria uma nova conexão SQLite.
        /// </summary>
        /// <returns>IDbConnection aberta e pronta para uso.</returns>
        public IDbConnection CriarConexao()
        {
            Batteries.Init();
            var conexao = new SqliteConnection(_connectionString);
            conexao.Open();
            return conexao;
        }
    }
}
