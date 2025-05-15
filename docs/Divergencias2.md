# ✅ Explicação do Projeto: Sistema de Gestão de Biblioteca

## 🎯 Objetivo

Este projeto implementa um sistema de gestão de biblioteca conforme proposto no documento original “TemaTrabalhoGrupoPOO.pdf”, utilizando **C#** e os princípios da **Programação Orientada a Objetos (POO)**. O sistema é executado em modo console e permite gerenciar livros, usuários e empréstimos.

---

## 🧱 Arquitetura e Organização

O projeto segue uma arquitetura em **camadas** fortemente inspirada pelos conceitos do **Domain-Driven Design (DDD)**. Cada camada tem responsabilidades bem definidas:

- **UI (Interface de Usuário)**: Interação com o usuário via console.
- **Aplicação**: Coordenação dos casos de uso (Application Services).
- **Domínio**: Entidades, regras de negócio, serviços de domínio, exceções.
- **Infraestrutura**: Acesso ao banco de dados, persistência com SQLite e Dapper.

---

## ✅ Implementação das Funcionalidades Requeridas

### 📚 Livros
- Cadastro com **título, autor, ISBN e ano de publicação**.
- Consulta por **título, autor e ISBN**.
- Armazenamento persistente em SQLite.

### 👤 Usuários
- Cadastro com **nome de usuário, nome completo, telefone e email**.
- Atribuição de **roles**: `Bibliotecario` e `Leitor`.
- Listagem e atualização da role.

### 📕 Exemplares
- Cada livro pode ter **vários exemplares**.
- Indicação de se o exemplar é **restrito à leitura local**.

### 🔄 Empréstimos
- Empréstimos realizados apenas com exemplares **disponíveis** e **não restritos à leitura local**.
- Período fixo de **3 dias**, calculado automaticamente.
- Registro da **data de devolução**.
- Listagem de empréstimos por usuário.

---

## 🔄 Divergências em relação ao enunciado original e justificativas

| Item no Enunciado | Implementação | Justificativa |
|-------------------|---------------|---------------|
| "Número de exemplares disponíveis" | Modelado como entidade `Exemplar`, com um registro por cópia | Melhor modelagem orientada a objetos, com suporte direto a empréstimos por cópia |
| "Endereço" do usuário | Omitido | Substituído por telefone e email, que são mais úteis e práticos |
| "Usuários se registram" | Apenas o bibliotecário pode cadastrar usuários | Segurança e controle, além de simplificação |
| "Livros disponíveis" | Sistema calcula disponibilidade com base em empréstimos ativos | Abordagem mais robusta e baseada em estados reais |
| Armazenamento opcional em arquivo | Usamos SQLite com Dapper | Escolha por robustez e clareza na persistência |
| Interface simples via console | Console interativo com menus, validações e mensagens claras | Interface intuitiva e robusta conforme solicitado |

---

## 💡 Melhorias implementadas

- ✅ Persistência com **SQLite**.
- ✅ Camada de domínio com **entidades imutáveis**.
- ✅ Uso de **TypeHandlers personalizados** para integração com Dapper.
- ✅ **Injeção de dependências manual** para simplificação e controle.
- ✅ Sistema inicializa com **dados de exemplo** (usuário admin, livros, etc).
- ✅ Diagrama de Classes, Sequência e Banco em **D2**, atualizado para refletir o sistema real.

---

## 🧪 Validação de entradas

O sistema implementa:
- Entradas protegidas com `ConsoleEx` para leitura segura de dados.
- Mensagens de erro informativas.
- Menus com permissões baseadas em `Role`.

---

## 📝 Conclusão

O projeto foi desenvolvido com rigor e clareza, mantendo conformidade com os objetivos propostos e aplicando os princípios de POO na prática. As melhorias e divergências foram cuidadosamente pensadas para **melhorar a coesão, flexibilidade, legibilidade e realismo** da solução.

