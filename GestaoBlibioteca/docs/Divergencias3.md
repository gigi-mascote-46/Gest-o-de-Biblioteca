# 🧭 Justificativa das Diferenças em Relação ao Enunciado Original

## 📄 Referência: Documento “TemaTrabalhoGrupoPOO.pdf”

O enunciado propõe o desenvolvimento de um sistema de gestão de biblioteca com as seguintes funcionalidades principais:

- Cadastro de livros, com informações como título, autor, ano e número de exemplares.
- Cadastro de usuários, com nome, endereço e telefone.
- Empréstimo e devolução de livros com prazos.
- Consulta de livros disponíveis.
- Relatórios de empréstimos.

A linguagem definida é **C#**, com forte ênfase em **POO (Programação Orientada a Objetos)** e modularidade. O sistema deve rodar no **modo console**, e o uso de arquivos ou persistência em banco é opcional.

---

## ✅ Funcionalidades Atendidas

Todas as funcionalidades principais mencionadas no enunciado estão implementadas no nosso projeto:

- Cadastro e consulta de **livros**.
- Cadastro e consulta de **usuários**.
- Registro e devolução de **empréstimos**, respeitando o limite de 3 dias.
- Consulta de **livros disponíveis**.
- Relatórios de empréstimos por usuário.

---

## 🔧 Justificativas das Principais Diferenças

### 1. 🧱 **Arquitetura com Múltiplas Camadas**

**Diferença:** Implementamos uma arquitetura em camadas (UI, Application, Domain, Infraestrutura), com uso explícito de **Domain Models**, **Services**, **Repositories** e **Unit of Work**.

**Justificativa:**
- O enunciado incentiva modularidade, reutilização e separação de responsabilidades.
- A arquitetura adotada segue boas práticas da engenharia de software moderna.
- Facilita testes automatizados, manutenção e futuras extensões.
- Ajuda os alunos a consolidarem conhecimento sobre DDD (Domain-Driven Design) e SOLID.

---

### 2. 🔒 **Controle de Acesso por Role**

**Diferença:** Implementamos o conceito de **funções de usuário (Role)** — `Bibliotecario` e `Leitor` — e com isso, restrições a certas ações.

**Justificativa:**
- Torna o sistema mais realista e alinhado com sistemas reais de bibliotecas.
- Enriquece o aprendizado de controle de fluxo baseado em permissões.
- Implementado de forma limpa na camada de UI, como recomendado para evitar acoplamento.

---

### 3. 🧮 **Contagem de Exemplares como Entidade (Exemplar)**

**Diferença:** Em vez de um campo "número de exemplares" no livro, criamos uma **entidade Exemplar** para representar fisicamente cada cópia de um livro.

**Justificativa:**
- Permite rastrear individualmente quais exemplares estão emprestados ou disponíveis.
- Evita inconsistências de contagem (como “5 disponíveis”, mas nenhum identificado).
- Reflete a realidade de forma mais precisa, especialmente se o sistema for expandido futuramente.

---

### 4. 🗄️ **Persistência com SQLite + Dapper**

**Diferença:** Adotamos **persistência com banco de dados SQLite** e acesso via **Dapper**, ao invés de arquivos ou instâncias em memória.

**Justificativa:**
- Exposição clara ao SQL, reforçando aprendizado (em vez de abstrações do EF Core).
- Facilidade de integração com o projeto C# sem dependências externas complexas.
- A persistência melhora o realismo do sistema, mas continua simples e didática.

---

### 5. 🕐 **Armazenamento de Datas como TEXT (ISO 8601 UTC)**

**Diferença:** Todas as datas/hora são armazenadas como **texto no formato ISO 8601**, em UTC.

**Justificativa:**
- Segue a recomendação oficial do SQLite.
- Padroniza e facilita parsing e comparação de datas.
- Garante compatibilidade e legibilidade mesmo fora do sistema.

---

### 6. 📜 **Design Rico em Domain Models e Validações**

**Diferença:** Os **domain models são imutáveis**, com validações em seus construtores, e regras de negócio encapsuladas nos **serviços de domínio**.

**Justificativa:**
- Garante que objetos sejam criados sempre em estado válido.
- Facilita testes, manutenção e integridade do sistema.
- Reflete as práticas modernas de programação orientada a objetos.

---

### 7. 📦 **Uso de Injeção de Dependência (IoC)**

**Diferença:** Implementamos um contêiner de Inversão de Controle (IoC) para gerenciar as dependências entre as classes.

**Justificativa:**
- Desacopla os componentes, aumentando a flexibilidade do sistema.
- Torna o sistema mais testável e extensível.
- Mostra como aplicar padrões profissionais em projetos reais.

---

## 💡 Conclusão

Embora o nosso projeto tenha expandido e refinado a estrutura do sistema além do escopo básico do enunciado, **todas as funcionalidades obrigatórias foram implementadas com rigor e qualidade**. As divergências foram **motivadas por objetivos didáticos** e pela busca de **boas práticas de engenharia de software**, conforme solicitado indiretamente pelo próprio enunciado ao mencionar modularidade, POO e manutenibilidade.

Estamos confiantes de que o projeto demonstra não apenas o que foi solicitado, mas também que o grupo dominou os conceitos fundamentais de POO e conseguiu aplicá-los de forma moderna e estruturada.

---

Se quiser, posso converter esse conteúdo para PDF ou incluir isso como parte da documentação oficial. Deseja isso?