# 📚 Sistema de Gestão de Biblioteca

## ✅ Visão Geral

Este projeto é um sistema de gestão de biblioteca desenvolvido em **C#**, com persistência via **SQLite3** e arquitetura pensada para manutenção, testes e expansão.  
Aplica os princípios de:
- Programação orientada a objetos (POO)
- Imutabilidade inspirada na programação funcional
- Separação clara de responsabilidades
- Estrutura modular com camadas bem definidas

## 🎯 Objetivo

Permitir a gestão de livros, usuários (leitores e bibliotecários) e empréstimos, respeitando regras de negócio específicas, com foco em qualidade, consistência e clareza de código.

---

## 🔐 Regras de Negócio

| # | Regra |
|--|-------|
| **1** | Cada exemplar pode ter um campo `SomenteLeituraLocal`. Esses exemplares **não podem ser emprestados**. |
| **2** | Os empréstimos têm um prazo fixo de **3 dias** a partir da data de empréstimo. |
| **3** | A penalidade por atraso é apenas **verbal** (sem multa ou bloqueio). |
| **4** | Usuários possuem papéis (`leitor` ou `bibliotecário`) que controlam o que podem fazer no sistema. |
| **5** | O sistema permite múltiplos exemplares por livro. A disponibilidade de empréstimo é calculada com base em exemplares emprestáveis. |
| **6** | Um exemplar só é considerado devolvido após a **data de devolução ser registrada** explicitamente. |

---

## 🏛️ Estrutura do Sistema

### 🧱 Domínio (Modelos Imutáveis)

- `Livro`: título, autor, ano, ISBN
- `Exemplar`: ID local, ISBN, `SomenteLeituraLocal`
- `Usuario`: nome, telefone, endereço, role (leitor/bibliotecário)
- `Emprestimo`: ID, exemplar, usuário, data empréstimo, **data limite**, **data de devolução** (null se não devolvido)

> Os **domain models são imutáveis** e suas instâncias são validadas rigorosamente para evitar estados inválidos.

---

### 🧠 Serviços (Aplicação das Regras de Negócio)

- `LivroService`: Cadastro, listagem e consultas por ISBN
- `UsuarioService`: Cadastro, login, atribuição de roles
- `EmprestimoService`: Gerencia empréstimos e devoluções, valida regras como disponibilidade e leitura local

---

### 🖥️ Interface (UI)

- Módulo console com menus organizados por entidade
- `UIController` principal e subcontroladores: `LivroUI`, `UsuarioUI`, `EmprestimoUI`
- Entrada via console adaptada conforme role do usuário autenticado
- Loop principal está dentro do módulo UI
- Usuário faz login apenas com nome (sem senha)

---

### 💾 Infraestrutura (Persistência)

- Repositórios que implementam acesso SQLite
- Migrations automáticas para criar e popular o banco com dados padrão:
    - Usuário "bibliotecario" com role `bibliotecario`
    - Usuário "leitor" com role `leitor`
    - Livros e exemplares de exemplo
- **Banco criado no primeiro uso do sistema**

---

## 🗃️ Estrutura do Banco de Dados (Resumida)

### Tabela `Livros`
- `ISBN` (PK)
- `Titulo`
- `Autor`
- `AnoPublicacao`

### Tabela `Exemplares`
- `Id` (PK)
- `ISBN` (FK para Livros)
- `SomenteLeituraLocal` (bool)

### Tabela `Usuarios`
- `Id` (PK)
- `Nome`
- `Endereco`
- `Telefone`
- `Role` (leitor ou bibliotecario)

### Tabela `Emprestimos`
- `Id` (PK)
- `ExemplarId` (FK)
- `UsuarioId` (FK)
- `DataEmprestimo`
- `DataLimiteDevolucao`
- `DataDevolucao` (nullable)

---

## 🧱 Decisões Arquiteturais

- **Imutabilidade no domínio**: Nenhuma entidade de domínio pode ter seu estado alterado depois de criada.
- **Regras de negócio isoladas**: Toda lógica que altera o sistema reside em serviços (não no modelo).
- **Dados sempre consistentes**: A modelagem evita dados duplicados ou inconsistentes (ex: disponibilidade é derivada, não armazenada).
- **Interface desacoplada**: O sistema pode futuramente adotar GUI ou Web sem alterar o núcleo.
- **Banco relacional bem normalizado**, sem redundâncias perigosas.

---

## 🔍 Observações Importantes

- A regra de leitura local é definida exemplar por exemplar com `SomenteLeituraLocal = true`.
- A interface de console será mantida simples, mas modular.
- No futuro, podemos explorar testes unitários com xUnit e logging com Serilog.
- Autenticação será apenas por nome de usuário (sem senhas).
- Cada empréstimo é registrado com uma data limite e uma data real de devolução, que pode ser null.

---

## 🧪 Futuros Recursos

| Recurso | Situação |
|---------|----------|
| Testes Unitários | Planejado |
| Logs estruturados | Planejado |
| GUI ou Web | Arquitetura preparada |
| Autenticação com senha | Fora do escopo |
| Persistência alternativa (JSON, XML) | Fora do escopo |

---

## 📝 Comentários no Código

Todos os pontos que envolvem:
- Lógicas de negócio específicas
- Restrições como leitura local
- Decisões arquiteturais (ex: imutabilidade)

Serão explicados com **comentários no código** no momento da implementação.

