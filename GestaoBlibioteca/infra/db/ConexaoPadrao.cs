// TODO: Esta classe não é mais usada e pode ser removida.

//ConexaoPadrao.cs

using Microsoft.Data.Sqlite;

namespace Infra
{
    /// <summary>
    /// Fornece um ponto único para criar conexões SQLite com o banco de dados padrão da aplicação.
    /// </summary>
    public static class ConexaoPadrao
    {
        // Nome do arquivo de banco de dados SQLite.
        // Será criado automaticamente na pasta onde o executável for iniciado.
        private const string CaminhoBanco = "biblioteca.db";

        /// <summary>
        /// Cria e abre uma conexão SQLite com o banco de dados local da aplicação.
        /// </summary>
        /// <returns>Instância aberta de <see cref="SqliteConnection"/>.</returns>
        public static SqliteConnection CriarConexao()
        {
            var conexao = new SqliteConnection($"Data Source={CaminhoBanco};Version=3;");
            conexao.Open();
            return conexao;
        }
    }
}
