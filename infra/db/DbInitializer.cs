//DbInitializer.cs

using System;
using Microsoft.Data.Sqlite;
using System.Data;
using Dapper;

namespace Infra
{
    /// <summary>
    /// Responsável por aplicar migrações no banco de dados.
    /// Assegura que todas as tabelas estejam criadas conforme o modelo de dados do sistema.
    /// </summary>
    public static class DbInitializer
    {
        /// <summary>
        /// Executa as migrações de criação das tabelas no banco de dados SQLite.
        /// A conexão deve ser aberta e reutilizável ao longo do programa.
        /// </summary>
        /// <param name="conexao">Conexão ativa com o banco de dados.</param>
        public static void ExecutarMigracoes(IDbConnection conexao)
        {
            if (conexao == null) throw new ArgumentNullException(nameof(conexao));

            // Estrutura do banco de dados.

            // https://sqlite.org/lang_createtable.html
            // https://sqlite.org/datatype3.html
            conexao.Execute(@"
            CREATE TABLE IF NOT EXISTS usuarios (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                nome_usuario TEXT NOT NULL UNIQUE,
                nome TEXT NOT NULL,
                telefone TEXT NOT NULL,
                email TEXT NOT NULL,
                role TEXT NOT NULL
                );
            ");

            conexao.Execute(@"
            CREATE TABLE IF NOT EXISTS livros (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                titulo TEXT NOT NULL,
                autor TEXT NOT NULL,
                    isbn TEXT NOT NULL UNIQUE,
                    ano_publicacao INTEGER NOT NULL
                );
            ");

            conexao.Execute(@"
            CREATE TABLE IF NOT EXISTS exemplares (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                livro_id INTEGER NOT NULL,
                somente_leitura_local INTEGER NOT NULL,
                FOREIGN KEY (livro_id) REFERENCES livros(id)
                );
            ");

            conexao.Execute(@"
            CREATE TABLE IF NOT EXISTS emprestimos (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                usuario_id INTEGER NOT NULL,
                exemplar_id INTEGER NOT NULL,
                data_emprestimo TEXT NOT NULL,
                data_limite_devolucao TEXT NOT NULL,
                data_devolucao TEXT,
                FOREIGN KEY (usuario_id) REFERENCES usuarios(id),
                FOREIGN KEY (exemplar_id) REFERENCES exemplares(id)
                );
            ");

            // Dados iniciais para o sistema.

            // https://sqlite.org/lang_insert.html
            // https://sqlite.org/lang_conflict.html
            conexao.Execute(@"
                INSERT OR IGNORE INTO usuarios (id, nome_usuario, nome, telefone, email, role)
                VALUES
                    (1, 'bibliotecario', 'Bibliotecário da Biblioteca', '9999-0000', 'bibliotecario@example.com', 'Bibliotecario'),
                    (2, 'leitor', 'Primeiro Leitor', '8888-0001', 'leitor@example.com', 'Leitor');
            ");

            conexao.Execute(@"
                INSERT OR IGNORE INTO livros (id, titulo, autor, isbn, ano_publicacao)
                VALUES
                    (1, 'O Senhor dos Anéis', 'J.R.R. Tolkien', '9789897773921', 1954),
                    (2, '1984', 'George Orwell', ' 9789726081890', 1949),
                    (3, 'Dom Casmurro', 'Machado de Assis', '9789897876530', 1899);
            ");

            conexao.Execute(@"
                INSERT OR IGNORE INTO exemplares (id, livro_id, somente_leitura_local)
                VALUES
                    (1, 1, 0),
                    (2, 1, 1),
                    (3, 2, 0),
                    (4, 3, 1);
            ");

            Console.WriteLine("Migrações do banco concluídas.");
        }
    }
}
