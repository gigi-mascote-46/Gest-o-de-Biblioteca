# Decisões Arquiteturais e de Design do Sistema de Biblioteca

Este documento apresenta e justifica as principais escolhas arquiteturais, padrões de projeto, decisões de design e trade-offs adotados no desenvolvimento do sistema de biblioteca. O objetivo é proporcionar uma base sólida de entendimento técnico, didático e evolutiva, adequada para um projeto educacional de médio porte com foco em boas práticas de engenharia de software.

---

## 🏛️ Arquitetura em Camadas

O projeto adota uma **arquitetura em camadas**, separando responsabilidades em:

- **UI (Interface do Usuário)**: interação com o usuário (via terminal).
- **Application Services**: orquestração de operações e regras de negócio do sistema.
- **Domínio (Domain Model)**: lógica de negócio central, entidades e serviços de domínio.
- **Infraestrutura (Repositórios, Unidade de Trabalho)**: acesso ao banco de dados, persistência e transações.

### ✅ Benefícios:

- Clareza e separação de responsabilidades.
- Facilidade de testes unitários e de integração.
- Modularidade e substituição de implementações (ex: repositórios SQLite podem ser substituídos por mock ou outra base).
- Alinhamento com a **arquitetura limpa** e o **DDD (Domain-Driven Design)**.

---[Decisões Arquiteturais e de Design.md](Decis%F5es%20Arquiteturais%20e%20de%20Design.md)

## 🧱 Modelagem de Domínio

As entidades centrais do sistema são:

- `Livro`
- `Exemplar`
- `Usuario`
- `Emprestimo`

As entidades seguem o padrão **entidade imutável**, com valores definidos apenas no construtor. Isso melhora a previsibilidade e alinhamento com princípios da **programação funcional**.

Serviços de domínio (como `CadastroDeLivros` e `GestorDeEmprestimos`) encapsulam regras específicas e validações mais complexas.

Exceções específicas são usadas para representar falhas de negócio, por exemplo:

- `UsuarioAtingiuLimiteEmprestimosException`
- `EmprestimoJaDevolvidoException`

---

## 🛠️ Padrões de Projeto Utilizados

- **Service Layer**: separação entre lógica de domínio e orquestração.
- **Repository**: abstração do acesso a dados.
- **Unit of Work**: controle de transações.
- **Factory Method (implícito)**: criação de entidades com regras encapsuladas.
- **Dependency Injection (manual)**: injeção de dependências nas instâncias das camadas superiores.

---

## 💾 Acesso a Dados e Armazenamento

- Banco de dados: **SQLite** (leve, local, zero-config, ideal para fins educacionais).
- Acesso via: **Dapper** (micro-ORM).

### 📌 Justificativas para o Dapper

- Exposição explícita ao SQL (importante para fins didáticos).
- Maior controle e previsibilidade.
- Melhor performance em muitos casos.
- Integração simples com objetos imutáveis.

### 🌐 Armazenamento de Datas

Todos os campos `DateTime` são armazenados como `TEXT` no formato **ISO 8601 (UTC)**, garantindo portabilidade, legibilidade e consistência.

---

## 🔁 Diagramas de Sequência

Todos os principais fluxos do sistema foram descritos com **diagramas de sequência D2**, cobrindo:

- Login e controle de sessão
- Cadastro, atualização e listagem de usuários
- Operações com livros e exemplares
- Empréstimos e devoluções

Esses diagramas demonstram a interação clara entre camadas e entidades, promovendo entendimento técnico e validação do comportamento esperado.

---

## 🔄 Trade-offs Considerados

| Decisão | Justificativa | Consequência |
|--------|---------------|--------------|
| Uso de Dapper ao invés de EF Core | Maior controle e exposição ao SQL | Mais código manual, mas mais didático |
| Evitar DTOs | Redução de complexidade em um sistema pequeno | A UI lida diretamente com tipos primitivos |
| Imutabilidade nos domain models | Segurança, previsibilidade e alinhamento com boas práticas | Exige construtores completos, exige configuração especial no Dapper |
| Separação da camada de UI | Permite testes e expansão futura | UI precisa conhecer estrutura básica dos dados |

---

## ✅ Boas Práticas Atendidas

- **SRP (Princípio da Responsabilidade Única)**: cada classe tem uma função clara e coesa.
- **Encapsulamento**: entidades controlam seus próprios dados.
- **Inversão de dependência**: repositórios e serviços usam interfaces.
- **Testabilidade**: fácil mock de serviços e repositórios.
- **Transparência**: dados trafegam em tipos simples ou entidades do domínio, sem mágica oculta.

---

## 🧠 Alinhamento entre OO e Funcional

- **Orientação a Objetos**: modelagem rica, com encapsulamento e comportamentos nos próprios objetos.
- **Funcional**: uso de objetos imutáveis, tratamento explícito de exceções como resultado de falhas de negócio, e separação clara entre side-effects (infraestrutura) e lógica pura (domínio).

---

## 📚 Conclusão

O projeto é um exercício prático completo de design, arquitetura e boas práticas, construído com clareza pedagógica em mente. Ele serve como base sólida para evolução futura — seja para adicionar mais funcionalidades, mudar a interface (ex: web), ou alterar o mecanismo de persistência — mantendo separação de responsabilidades, flexibilidade e previsibilidade.

