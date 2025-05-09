## 🚀 Propostas de Expansão e Melhorias (Já Implementadas)

Durante o desenvolvimento deste sistema de biblioteca, diversas melhorias foram propostas e implementadas além dos requisitos obrigatórios. Essas melhorias seguiram os princípios da programação orientada a objetos, reforçando a coesão, o baixo acoplamento e a legibilidade do código.

Abaixo listamos cada uma das melhorias implementadas, com sua justificativa, impacto e benefícios para o sistema.

---

### ✅ 1. Camada de Interface Separada e Modular

**Por que a ideia foi escolhida**  
Para garantir a separação de responsabilidades entre lógica de negócio e apresentação.

**Como ela melhora o sistema**  
Organiza a interface de console em subcomponentes (`LivroUI`, `UsuarioUI`, etc.), todos orquestrados por `UIController`.

**Benefícios**  
Facilita a manutenção, substituição futura por GUI/Web, e melhora a legibilidade do código.

---

### ✅ 2. Controle de Acesso por Role (Leitor / Bibliotecário)

**Por que a ideia foi escolhida**  
Nem todos os usuários devem ter acesso às mesmas funcionalidades.

**Como ela melhora o sistema**  
Filtra dinamicamente os menus com base na `Role` do usuário logado.

**Benefícios**  
Impede ações indevidas de usuários comuns e respeita o princípio de menor privilégio.

---

### ✅ 3. Sistema de Menus com Permissões e Retorno

**Por que a ideia foi escolhida**  
A navegação por menus estava pouco flexível e com má experiência de uso.

**Como ela melhora o sistema**  
Implementamos as classes `Menu`, `MenuItem` e `MenuUtils`, que exibem os menus de forma dinâmica, numerados corretamente, e com controle de retorno ao menu anterior.

**Benefícios**  
Melhora a usabilidade, evita saltos de numeração, torna o código mais coeso e facilita testes.

---

### ✅ 4. Camada de Persistência com Dapper (micro-ORM)

**Por que a ideia foi escolhida**  
Queríamos ter acesso direto ao SQL para fins pedagógicos e controle.

**Como ela melhora o sistema**  
Foi escolhida a biblioteca Dapper, por ser um micro-ORM de alto desempenho, permitindo uso explícito de SQL e total controle do mapeamento.

**Benefícios**  
Exposição ao SQL, menor curva de aprendizado, código mais transparente e compatível com imutabilidade nos domain models.

---

### ✅ 5. Mapeamento de Tipos com TypeHandlers

**Por que a ideia foi escolhida**  
Para mapear corretamente os tipos do domínio (ex: `bool`, `Role`) para os tipos armazenados no banco (ex: `int`, `TEXT`).

**Como ela melhora o sistema**  
Evita uso de aliases e DTOs manuais, mantendo os domain models puros e sem modificações.

**Benefícios**  
Reforça encapsulamento e separação de responsabilidades entre domínio e infraestrutura.

---

### ✅ 6. Validação no Domínio (Construtores Seguros)

**Por que a ideia foi escolhida**  
Para impedir que objetos do domínio sejam criados em estados inválidos.

**Como ela melhora o sistema**  
As regras de negócio básicas são aplicadas já no construtor de cada entidade (ex: não permitir título vazio para um livro).

**Benefícios**  
Consistência dos dados, segurança e menor risco de falhas lógicas em runtime.

---

### ✅ 7. Imutabilidade Inspirada na Programação Funcional

**Por que a ideia foi escolhida**  
Para garantir maior previsibilidade e segurança das entidades de domínio.

**Como ela melhora o sistema**  
Objetos como `Livro`, `Exemplar`, `Usuario` e `Emprestimo` não têm setters públicos. Mudanças de estado são feitas via novos objetos.

**Benefícios**  
Evita efeitos colaterais, facilita testes e compatibiliza com práticas modernas de desenvolvimento.

---

### ✅ 8. Implementação de Unidade de Trabalho (`IUnidadeDeTrabalho`)

**Por que a ideia foi escolhida**  
Garantir atomicidade em operações que envolvem múltiplos repositórios e etapas de negócio.

**Como ela melhora o sistema**  
Centraliza o controle de transações (`BEGIN`, `COMMIT`, `ROLLBACK`) de forma reutilizável, sem acoplar as transações à lógica dos serviços de aplicação.

**Benefícios**  
Evita inconsistências nos dados, melhora a testabilidade e organiza o uso de transações de forma clara.

---

### ✅ 9. Exceções de Domínio Específicas

**Por que a ideia foi escolhida**  
Evitar o uso genérico de exceções do sistema e representar corretamente erros de negócio.

**Como ela melhora o sistema**  
Cada violação de regra de negócio (ex: empréstimo já devolvido, livro não encontrado) tem sua própria exceção nomeada e documentada.

**Benefícios**  
Melhora a clareza do código, facilita o tratamento de erros na UI e expressa melhor as regras do domínio.

---

### ✅ 10. Mapeamento Manual com `splitOn` no Dapper

**Por que a ideia foi escolhida**  
Mapeamentos com joins seriam necessários para recuperar relacionamentos compostos entre entidades, sem usar DTOs.

**Como ela melhora o sistema**  
Permite criar objetos complexos como `Emprestimo` com `Usuario` e `Exemplar` diretamente do resultado da consulta SQL.

**Benefícios**  
Elimina a necessidade de duplicar estruturas de dados (DTOs), mantém o domínio como fonte única da verdade.

---

### ✅ 11. Armazenamento de `DateTime` como `TEXT ISO 8601` em UTC

**Por que a ideia foi escolhida**  
Para garantir portabilidade, legibilidade e consistência no armazenamento de datas.

**Como ela melhora o sistema**  
Todas as datas no banco estão em UTC, armazenadas como `TEXT` com o formato `"YYYY-MM-DDTHH:MM:SSZ"`.

**Benefícios**  
Evita problemas com fuso horário, facilita debugging e uso com outras ferramentas, como relatórios ou ETL.

---

### ✅ 12. Design Clean com Camadas Bem Definidas

**Por que a ideia foi escolhida**  
Para promover separação de responsabilidades e tornar o sistema extensível.

**Como ela melhora o sistema**  
Camadas bem definidas: UI → Application Service → Domain Service → Repositório/Infra. Cada camada conhece apenas a imediatamente inferior.

**Benefícios**  
Alta coesão, baixo acoplamento, fácil de manter, testar e evoluir.

---

### ✅ 13. Uso do padrão IoC com Injeção de Dependência

**Por que a ideia foi escolhida**  
Para permitir a montagem flexível do sistema, desacoplar dependências, facilitar testes e separação entre infraestrutura e lógica.

**Como ela melhora o sistema**  
Todas as dependências são injetadas via construtor. O `ConfiguracaoIoC` cuida de instanciar e conectar os componentes, aplicando o padrão de **Injeção de Dependências**.

**Benefícios**  
Reduz dependências rígidas, melhora a testabilidade e deixa o código mais desacoplado.

---

### ✅ 14. Menus Dinâmicos com Filtro por Permissão (RBAC)

**Por que a ideia foi escolhida**  
Evitar que usuários vejam opções de menu para as quais não têm permissão, mantendo a interface limpa e segura.

**Como ela melhora o sistema**  
O sistema monta dinamicamente os menus com base no `Role` do usuário autenticado, seguindo o princípio de RBAC (Role-Based Access Control), utilizando a classe `MenuItem` com uma lista de roles permitidas.

**Benefícios**  
Melhora a usabilidade e previne interações inválidas. Além disso, facilita manutenção futura caso novos roles sejam adicionados.

---

### ✅ 15. Voltar ao Menu Anterior Padronizado

**Por que a ideia foi escolhida**  
Para melhorar a navegação da interface e manter uma experiência consistente para o usuário.

**Como ela melhora o sistema**  
Cada menu recebe automaticamente uma opção de "Voltar", adicionada via parâmetro no construtor da classe `Menu`.

**Benefícios**  
Reduz confusão na navegação, facilita o retorno ao ponto anterior e evita duplicação de código para adicionar essa funcionalidade manualmente em todos os menus.

---

### ✅ 16. Classe `ConsoleEx` para Entrada de Dados Segura e Validada

**Por que a ideia foi escolhida**  
Evitar código repetitivo para validação de entrada do usuário em console e unificar o tratamento de erros.

**Como ela melhora o sistema**  
ConsoleEx abstrai a leitura de valores primitivos com validação intrínseca e customizável.
Os métodos da `ConsoleEx` fazem leitura validada com mensagens de erro e permitem regras customizadas de validação.

**Benefícios**  
Evita duplicação de lógica, aumenta a robustez da interface de usuário e padroniza o comportamento para todas as entradas.

---

### ✅ 17. Design Orientado a Objetos com Imutabilidade no Domínio

**Por que a ideia foi escolhida**  
Proteger os objetos de domínio contra alterações indevidas e forçar consistência desde a criação.

**Como ela melhora o sistema**  
Os Domain Models são imutáveis: seus campos são `readonly`, setados apenas no construtor, e não expõem métodos para alteração de estado, com exceção de operações controladas via Services.

**Benefícios**  
Evita estados inconsistentes, facilita testes, e favorece raciocínio orientado a regras de negócio. Alinha-se a boas práticas de DDD e programação funcional.

---

### ✅ 18. Eliminação de DTOs desnecessários

**Por que a ideia foi escolhida**  
Manter o domínio como representação única dos dados, sem criar estruturas paralelas para transferência de dados.

**Como ela melhora o sistema**  
A UI interage diretamente com Application Services usando tipos primitivos, sem intermediários. O Dapper mapeia diretamente para os Domain Models com `splitOn`.

**Benefícios**  
Redução de complexidade, menos classes para manter, código mais direto e transparente.

---

### ✅ 19. Uso explícito de `readonly` para garantir imutabilidade

**Por que a ideia foi escolhida**  
Prevenir modificações acidentais nas dependências e garantir thread safety em cenários futuros.

**Como ela melhora o sistema**  
Campos injetados via construtor são marcados com `readonly`, seguindo os princípios de design robusto.

**Benefícios**  
Reforça a intenção do desenvolvedor, ajuda o compilador a otimizar e previne bugs de alteração de dependência durante o ciclo de vida do objeto.

---

### ✅ 20. Separação entre Modelos, Casos de Uso, Infraestrutura e Interface

**Por que a ideia foi escolhida**  
Aplicar uma arquitetura limpa e escalável, com camadas bem separadas.

**Como ela melhora o sistema**  
Cada camada tem uma pasta/namespaces próprios, com responsabilidades delimitadas (UI, Aplicação, Domínio, Infraestrutura).

**Benefícios**  
Facilita manutenção, testes, onboarding de novos desenvolvedores e futura evolução para interface gráfica ou Web sem reescrever a lógica do negócio.

---

### ✅ 21. Uso de Exceções Específicas para Regras de Negócio

**Por que a ideia foi escolhida**  
Evitar que exceções genéricas sejam lançadas em caso de erro de regra de negócio, melhorando clareza e controle de fluxo.

**Como ela melhora o sistema**  
Cada violação de regra importante tem sua própria exceção: `LivroNaoEncontradoException`, `EmprestimoJaDevolvidoException`, etc.

**Benefícios**  
Facilita o entendimento da falha, tratamento específico por camada de UI ou testes, e documentação implícita de regras.

---

### ✅ 22. Arquitetura Guiada por Diagramas de Classes e Sequência

**Por que a ideia foi escolhida**  
Garantir que a implementação siga exatamente o modelo lógico e os fluxos definidos, reduzindo o risco de inconsistências.

**Como ela melhora o sistema**  
Toda a implementação (domain models, services, UI, etc.) foi derivada diretamente dos diagramas D2 de classes e sequência.

**Benefícios**  
A rastreabilidade entre modelo e código é total. A documentação serve como referência e contrato entre os membros do time.

---

### ✅ 23. Criação de TypeHandlers Personalizados para Integração com Dapper

**Por que a ideia foi escolhida**  
Evitar a necessidade de DTOs ou aliases nas queries, permitindo que o Dapper mapeie diretamente para os domain models.

**Como ela melhora o sistema**  
TypeHandlers como `DateTimeUtcTypeHandler`, `RoleTypeHandler` e `BooleanIntTypeHandler` garantem mapeamento correto entre C# e SQLite.

**Benefícios**  
Maior fidelidade entre o banco e o modelo, menos código, menor chance de erro, e flexibilidade para alterações futuras.

---

### ✅ 24. Registro Centralizado de TypeHandlers

**Por que a ideia foi escolhida**  
Evitar repetição de código e esquecimentos na configuração de handlers ao longo do projeto.

**Como ela melhora o sistema**  
A classe `RegistradorDeTypeHandlers` registra todos os handlers de uma vez só, de forma segura e reaproveitável.

**Benefícios**  
Facilidade de manutenção e inicialização consistente de infraestrutura.

---

### ✅ 25. UI Modularizada com Subcontroladores

**Por que a ideia foi escolhida**  
Evitar uma classe gigante controlando toda a interface de usuário e facilitar a separação por entidade.

**Como ela melhora o sistema**  
Cada entidade tem sua própria UI: `LivroUI`, `UsuarioUI`, etc., e todas são coordenadas por um `UIController`.

**Benefícios**  
Facilidade para modificar menus de forma isolada, testar funcionalidades e escalar a interface no futuro.

---

### ✅ 26. Menu Principal com Controle de Sessão

**Por que a ideia foi escolhida**  
Permitir que o usuário navegue no sistema com base em seu perfil, com persistência da sessão enquanto logado.

**Como ela melhora o sistema**  
A classe `Sessao` gerencia o estado atual do usuário logado, enquanto `MenuPrincipalUI` adapta as opções disponíveis com base no `Role`.

**Benefícios**  
Permite um sistema multi-usuário simples, respeitando regras de acesso e com UX clara.

---

### ✅ 27. Interface de Usuário Pronta para Troca

**Por que a ideia foi escolhida**  
Permitir que no futuro o console seja substituído por uma interface gráfica ou web, sem alterar a lógica do negócio.

**Como ela melhora o sistema**  
A arquitetura foi projetada com separação total entre UI e camada de aplicação/domínio, com dependências invertidas.

**Benefícios**  
Abre caminho para expansão futura com menor esforço de refatoração.

---

### ✅ 28. Separação entre IU e Aplicação com Tipos Primitivos

**Por que a ideia foi escolhida**  
Evitar dependência acidental da UI em objetos do domínio, mantendo as camadas desacopladas.

**Como ela melhora o sistema**  
A interface de usuário chama Application Services usando tipos simples (strings, int, bool), evitando expor internamente os domain models.

**Benefícios**  
Mais controle, evita violação de encapsulamento, e mantém clara a fronteira entre camadas.

---

### ✅ 29. Aplicação do Princípio da Responsabilidade Única (SRP)

**Por que a ideia foi escolhida**  
Organizar o código em partes pequenas e com responsabilidades bem definidas.

**Como ela melhora o sistema**  
Cada classe tem um propósito único, como CadastroDeUsuarios, AppServiceLivros, UsuarioUI, etc., promovendo o Single Responsibility Principle (SRP).

**Benefícios**
Facilidade de manutenção, testabilidade e expansão.

---

### ✅ 30. Serviços de Domínio para Isolar Lógica de Negócio (DDD + Factory)

**Por que a ideia foi escolhida**
Centralizar lógica de criação e operações sobre entidades.

**Como ela melhora o sistema**  
Serviços como CadastroDeUsuarios e GestorDeEmprestimos atuam como Factories e domain services, de acordo com DDD.

**Benefícios**
Lógica reutilizável, testável e com alta coesão.

---

### ✅ 31. Migrações e Criação Automatizada do Banco de Dados

**Por que a ideia foi escolhida**
Garantir que o sistema possa rodar em qualquer ambiente sem passos manuais.

**Como ela melhora o sistema**  
Ao iniciar o sistema, o banco é verificado e as tabelas são criadas se necessário. As migrações foram implementadas via SQL diretamente.

**Benefícios**
Ambiente de desenvolvimento e entrega mais estável, sem necessidade de scripts manuais.
Quando o programa é executado, mesmo que seja pela primeira vez, já pode ser usado imediatamente sem precisar cadastrar nenhum dado antes.
