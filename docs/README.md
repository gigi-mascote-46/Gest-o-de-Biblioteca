# 📚 Sistema de Biblioteca

Este é um sistema de biblioteca simples, modular e extensível, desenvolvido em C# com arquitetura baseada em **DDD (Domain-Driven Design)**, utilizando **SQLite** como banco de dados e **Dapper** como ORM de acesso a dados.

---

## 🧱 Organização do Projeto

A estrutura segue uma separação por camadas e responsabilidades. Os principais **namespaces** e seus papéis são:

### `Biblioteca.Dominio.*`
Contém o **core de negócio**:
- **Entidades (Modelos)**: Representam o domínio (`Livro`, `Usuario`, `Emprestimo`, `Exemplar`)
- **Serviços de Domínio**: Encapsulam regras de negócio (`CadastroDeLivros`, `GestorDeEmprestimos`, etc.)
- **Repositórios (interfaces)**: Contratos para acesso aos dados
- **Enums e Exceções**: Tipos e erros específicos de domínio

### `Biblioteca.Aplicacao.*`
Camada de orquestração:
- **Application Services**: Coordenam operações entre domínio e infraestrutura (`AppServiceUsuarios`, `AppServiceLivros`, etc.)
- **Menus auxiliares**: Algumas operações complementares

### `Biblioteca.Infraestrutura.*`
Contém a **infraestrutura**:
- **Persistência com SQLite**: Implementações de repositórios via Dapper
- **Unidade de Trabalho**: Suporte a transações
- **Type Handlers Dapper**: Conversão customizada de tipos (`bool`, `DateTime`, `enum`)
- **Fábrica de Conexões**: Centraliza criação de `IDbConnection`
- **IoC manual**: Composição explícita de dependências

### `Biblioteca.UI.*`
Responsável pela interface de usuário (console):
- **Telas por Entidade**: `LivroUI`, `UsuarioUI`, `EmprestimoUI`, etc.
- **Componentes genéricos**: `Menu`, `MenuItem`, `MenuUtils`
- **Autenticação**: `LoginUI`
- **Controlador principal**: `UIController`

---

## 🧭 Estrutura de Pastas

```plaintext
Biblioteca/
│
├── Aplicacao/
├── Dominio/
│   ├── Entidades/
│   ├── Enums/
│   ├── Excecoes/
│   ├── Repositorios/
│   └── Servicos/
│
├── Infraestrutura/
│   ├── Persistencia/
│   │   ├── TypeHandlers/
│   │   ├── Repositorios/
│   │   └── FabricaDeConexaoSQLite.cs
│   └── IoC/
│
├── UI/
│   ├── Telas/
│   └── Componentes/
│
├── Program.cs
└── README.md
```

---

## ⚙️ Como rodar o projeto

### Pré-requisitos

- [.NET SDK 7.0 ou superior](https://dotnet.microsoft.com/en-us/download)
- `dotnet` no PATH
- Não requer nenhum servidor de banco externo (SQLite local)

### Build

```bash
dotnet build
```

### Executar

```bash
dotnet run --project Biblioteca
```

> O banco será criado automaticamente no arquivo `biblioteca.db` (SQLite local).

### Primeira execução

- Na primeira execução, as tabelas serão criadas automaticamente via `DbInitializer`.
- O sistema começa na tela de login.
- O sistema já é iniciado com dois usuários: bibliotecario (papel **Bibliotecario**) e leitor (papel **Leitor**).
Você pode fazer login como um desses usuários para começar a usar o sistema.

---

## 👤 Acesso e Papéis

O sistema possui dois papéis:

- `Bibliotecario`: pode cadastrar livros, usuários, exemplares e realizar empréstimos.
- `Leitor`: pode visualizar livros e seus próprios empréstimos.

---

## 🧪 Testes

> ✅ Em desenvolvimento. Pode-se utilizar `xUnit` + SQLite em memória.

---

## 🛠️ Arquitetura e Padrões

- DDD: Separação clara entre domínio, aplicação e infraestrutura.
- Injeção de dependência manual.
- Banco de dados SQLite.
- Transações com Unidade de Trabalho.
- Conversão de tipos com Dapper (via `TypeHandler`).
- Imutabilidade nos Models de Domínio.

---

## 📌 Possíveis Melhorias Futuras

- Interface web (Blazor, ASP.NET, etc.)
- API REST
- Autenticação com senha
- Testes automatizados
- Configurações externas (`appsettings.json`)

---

## 📄 Licença

Este projeto tem copyright das autoras, todos os direitos reservados.

---

## ✍️ Autoras

- Angela Peixoto
- Amanda Brito
- Angélica Olivares
- Fabi Nascimento
