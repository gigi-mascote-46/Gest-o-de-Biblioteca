//Sessao

using Biblioteca.Dominio.Enums;
using Biblioteca.Dominio.Modelos;


namespace UI
{
    /// <summary>
    /// Representa a sessão atual do sistema, armazenando o usuário autenticado e
    /// oferecendo métodos para login, logout e verificação de permissões.
    /// </summary>
    public class Sessao
    {
        /// <summary>
        /// Obtém o usuário atualmente autenticado na sessão.
        /// </summary>
        public Usuario? UsuarioAtual { get; private set; }

        /// <summary>
        /// Realiza o login de um usuário, registrando-o como o usuário atual da sessão.
        /// </summary>
        /// <param name="usuario">O usuário a ser autenticado.</param>
        public void Login(Usuario usuario)
        {
            UsuarioAtual = usuario;
        }

        /// <summary>
        /// Realiza o logout, encerrando a sessão atual.
        /// </summary>
        public void Logout()
        {
            UsuarioAtual = null;
        }

        /// <summary>
        /// Verifica se o usuário atualmente autenticado possui o papel de Bibliotecário.
        /// </summary>
        /// <returns><c>true</c> se o usuário for um bibliotecário; caso contrário, <c>false</c>.</returns>
        public bool EhBibliotecario()
        {
            return UsuarioAtual?.Role == Role.Bibliotecario;
        }

        /// <summary>
        /// Verifica se o usuário atualmente autenticado possui o papel de Leitor.
        /// </summary>
        /// <returns><c>true</c> se o usuário for um leitor; caso contrário, <c>false</c>.</returns>
        public bool EhLeitor()
        {
            return UsuarioAtual?.Role == Role.Leitor;
        }

        /// <summary>
        /// Verifica se há um usuário autenticado na sessão atual.
        /// </summary>
        /// <returns><c>true</c> se houver um usuário autenticado; caso contrário, <c>false</c>.</returns>
        public bool EstaAutenticado()
        {
            return UsuarioAtual != null;
        }
    }
}
