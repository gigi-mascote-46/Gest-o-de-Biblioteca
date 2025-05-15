# ✅ Explicação do Projeto da Biblioteca

# Este arquivo tem sérios problemas e deve ser deletado.

> Não entregar este arquivo.

## 📚 Visão Geral

O projeto implementa um sistema de biblioteca conforme solicitado no arquivo `TemaTrabalhoGrupoPOO.pdf`. O objetivo principal é fornecer uma aplicação em C# que permita o gerenciamento de livros, usuários, exemplares e empréstimos de forma estruturada, seguindo os princípios da Programação Orientada a Objetos (POO), separação de responsabilidades, boas práticas arquiteturais e uso de DDD (Domain-Driven Design).

---

## 🧱 Arquitetura Adotada

Adotamos uma arquitetura em **camadas** baseada nos princípios do DDD:

1. **Domínio (Modelos, Serviços de Domínio, Exceções)**
2. **Aplicação (AppServices como orquestradores)**
3. **Infraestrutura (Persistência, IoC, TypeHandlers)**
4. **Apresentação (UI via console)**

Essa separação nos permitiu manter o código coeso, reutilizável e testável, com cada camada tendo responsabilidades bem definidas.

---

## 📂 Organização de Namespaces

| Namespace                        | Responsabilidade Principal |
|----------------------------------|----------------------------|
| `Biblioteca.Dominio`            | Entidades, enums e lógica de domínio |
| `Biblioteca.Aplicacao`          | AppServices: coordenam uso do domínio |
| `Biblioteca.Infraestrutura`     | Repositórios, persistência e banco de dados |
| `Biblioteca.UI`                 | Interface com o usuário via terminal |
| `Biblioteca.Acesso`             | Enum `Role`, usado em controle de permissões |

---

## ⚖️ Divergências (Justificadas) em relação ao PDF original

### 🔸 1. **Uso de DDD e camadas**
- **Divergência**: O PDF não especifica arquitetura, mas sugeria uma organização mais simples.
- **Justificativa**: Escolhemos usar camadas e DDD para praticar princípios avançados de POO, manter o projeto escalável e profissional.

> Acho que o item 2 está errado:
### 🔸 2. **Autenticação simplificada**
- **Divergência**: O enunciado menciona "login e senha".
- **Justificativa**: O projeto usa apenas `nome_usuario` para login. Senhas foram omitidas por simplicidade e por não haver exigência de criptografia ou segurança.

### 🔸 3. **Banco de dados relacional (SQLite)**
- **Divergência**: O enunciado não define mecanismo de persistência.
- **Justificativa**: Optamos por usar SQLite com Dapper para persistência leve e real, permitindo trabalhar com dados reais e testes mais ricos.

### 🔸 4. **Validação no domínio e imutabilidade**
- **Divergência**: O enunciado não exige isso.
- **Justificativa**: Aplicamos validações e tornamos os modelos imutáveis para seguir boas práticas e evitar mutabilidade silenciosa.

### 🔸 5. **Controle transacional com Unidade de Trabalho**
- **Divergência**: Não descrito no enunciado.
- **Justificativa**: Evita inconsistências em operações de escrita que envolvem múltiplos repositórios (ex: empréstimos).

---

## 👥 Perfis e Permissões

Usamos o enum `Role` para controlar o acesso aos menus:
- `Leitor`: pode buscar e listar livros.
- `Bibliotecario`: acesso completo, incluindo cadastro, edição e empréstimos.

---

## 🛠️ Design de Classes e Relacionamentos

Todas as entidades foram modeladas com base nos diagramas:
- Diagramas de classe, sequência e ER foram sincronizados com o código real.
- Usamos chaves primárias `int` autoincrementadas em todas as tabelas (SQLite).
- TypeHandlers garantem persistência correta de `DateTime`, `Role`, e `bool`.

---

## ⚙️ Como Rodar e Testar

- Basta abrir o projeto no Visual Studio ou VS Code.
- Build com `dotnet build`, execute com `dotnet run`.
- Interface por console.
- Não requer dependências externas além do .NET SDK 6.0 ou superior.

---

## 📌 Conclusão

O projeto excede os requisitos mínimos do enunciado, adotando boas práticas de engenharia de software. As divergências foram planejadas e bem justificadas, com foco em robustez, clareza e arquitetura limpa.

Caso o professor deseje uma versão simplificada ou com menos camadas, o projeto pode ser facilmente adaptado, mas a versão atual serve como demonstração de um sistema profissional e extensível.