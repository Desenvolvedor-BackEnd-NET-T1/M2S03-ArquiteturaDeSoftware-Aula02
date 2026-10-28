# SPDD Analysis: PizzariaByte — Quatro Arquiteturas Didáticas em .NET 10 (MVC, Hexagonal, Onion e Combinada)

## Original Business Requirement

> Fonte: `prompt.md` (conteúdo integral, sem alterações)

# Projeto didático de arquiteturas de software .NET — PizzariaByte

## 1. Contexto e objetivo

Você é um arquiteto de software sênior e desenvolvedor especialista em C# e .NET 10.

Sua missão é criar **quatro projetos de exemplo completos, independentes, executáveis e testáveis**, destinados ao ensino de arquitetura de software para iniciantes.

O objetivo é demonstrar, por meio de um mesmo cenário de negócio, como diferentes arquiteturas organizam responsabilidades, dependências, regras de negócio, interfaces, persistência e integrações externas.

Os projetos devem ser suficientemente próximos de aplicações utilizadas no mundo real, mas manter simplicidade e clareza para estudantes iniciantes.

**Não entregue apenas exemplos conceituais, diagramas ou trechos de código. Crie a solução completa, com arquivos, projetos, código-fonte, testes, documentação e instruções para execução e depuração.**

## 2. Arquiteturas obrigatórias

Criar os seguintes projetos, em diretórios independentes:

```text
./mvc
./hexagonal
./onion
./hexagonal_onion_mvc
```

### Projeto 1 — MVC puro

Demonstrar a arquitetura MVC (Model-View-Controller).

Características esperadas:

- Separação clara entre Model, View e Controller.
- Controllers responsáveis por receber e coordenar requisições.
- Models responsáveis pelos dados e pelas regras pertinentes ao modelo.
- Views responsáveis pela apresentação.
- Persistência e integrações implementadas de maneira didática, sem misturar desnecessariamente todo o código no Controller.
- Não impor a estrutura de Onion ou Ports & Adapters como arquitetura principal.
- Utilizar ASP.NET Core MVC com Razor Views.

O projeto deve representar um exemplo coerente de aplicação MVC, e não apenas uma API renomeada como MVC.

### Projeto 2 — Hexagonal Architecture

Demonstrar exclusivamente os princípios da Arquitetura Hexagonal (Ports & Adapters).

Características esperadas:

- Núcleo da aplicação isolado de tecnologias externas.
- Portas definidas por contratos ou interfaces.
- Adaptadores de entrada e saída.
- Integrações externas implementadas por adaptadores mockados.
- Persistência em memória implementada por um adaptador.
- Dependências direcionadas para as abstrações apropriadas.
- Demonstração clara de como substituir um adaptador sem reescrever as regras de negócio.

Não utilizar a Onion Architecture como estrutura organizacional obrigatória deste projeto.

### Projeto 3 — Onion Architecture

Demonstrar exclusivamente os princípios da Onion Architecture.

Características esperadas:

- Domínio no centro da arquitetura.
- Organização em camadas concêntricas ou projetos que representem claramente essas camadas.
- Dependências apontando para dentro.
- Regras de negócio independentes de infraestrutura.
- Interfaces posicionadas na camada apropriada, de acordo com o papel que exercem.
- Infraestrutura responsável por implementar persistência e integrações externas.
- Injeção de dependência para conectar as abstrações às implementações.

Não impor Ports & Adapters como o modelo arquitetural principal. Evite, entretanto, apresentar interfaces e abstrações como algo proibido na Onion.

### Projeto 4 — Hexagonal + Onion + MVC

Combinar as três abordagens de maneira coerente.

Responsabilidades:

- **Onion:** organizar o domínio, a aplicação e a direção das dependências.
- **Hexagonal:** definir portas e adaptadores para comunicação com sistemas externos.
- **MVC:** organizar a interface web por Models, Views e Controllers.

Demonstrar como essas abordagens podem coexistir sem duplicar responsabilidades ou criar camadas desnecessárias.

A aplicação web deve utilizar ASP.NET Core MVC com Razor Views. O domínio não deve depender de Controllers, Views, ASP.NET Core, Entity Framework Core ou detalhes específicos de infraestrutura.

Incluir um diagrama que indique onde cada abordagem se manifesta e como as dependências se relacionam.

## 3. Cenário de negócio comum

Todos os projetos devem implementar o mesmo caso de uso da aplicação **PizzariaByte**, uma pizzaria fictícia.

### Caso de uso: realizar pedido de pizza

O cliente deve conseguir:

1. Acessar a página inicial da pizzaria.
2. Consultar o cardápio.
3. Escolher uma pizza.
4. Informar a quantidade e o endereço de entrega.
5. Confirmar o pedido.
6. Visualizar o resumo do pedido, o valor total e o status das integrações.

### Regras de negócio mínimas

- O cardápio deve conter pelo menos três pizzas com preços definidos.
- A quantidade deve ser maior que zero.
- A pizza solicitada deve existir no cardápio.
- O total deve ser calculado a partir do preço da pizza e da quantidade.
- A taxa de entrega deve ser calculada por uma regra simples, documentada e compartilhada conceitualmente entre os quatro projetos.
- O pedido deve receber um identificador e um status inicial.
- O pedido confirmado deve ser salvo no armazenamento em memória.
- Falhas nas integrações externas devem ser tratadas sem deixar o pedido em um estado inconsistente.
- A interface deve apresentar mensagens compreensíveis de sucesso e erro.

Mantenha as mesmas regras, os mesmos dados iniciais, as mesmas entradas e os mesmos resultados esperados nos quatro projetos, para permitir comparação durante as aulas.

Não adicione funcionalidades de negócio desnecessárias, como autenticação, carrinho complexo, checkout real ou cadastro completo de usuários.

## 4. Frontend, backend, dados e integrações

Cada projeto deve conter os componentes necessários para demonstrar o fluxo completo da aplicação.

### Frontend

- Interface web funcional.
- Utilizar Razor Views e HTML/CSS simples nos projetos web.
- Formulários para consultar o cardápio e criar pedidos.
- Página ou seção para visualizar o resultado da operação.
- Validação de entrada e apresentação de mensagens de erro.

### Backend

- Implementar o caso de uso de criação de pedido.
- Separar responsabilidades conforme a arquitetura demonstrada.
- Não concentrar regras de negócio, persistência e integrações em um único método ou Controller.

### Dados

- Utilizar armazenamento em memória.
- Não exigir SQL Server, PostgreSQL, SQLite, Docker ou serviços externos para executar os exemplos.
- Os dados devem ser inicializados de maneira previsível.
- Explicar que os dados são perdidos quando a aplicação é reiniciada.
- Garantir que os testes possam executar sem depender do estado de uma execução anterior.

### Integrações mockadas

Implementar três conectores simulados:

- `ApiEmail`: simula o envio de confirmação do pedido por e-mail.
- `ApiDeliveryAiQFome`: simula a solicitação de entrega.
- `ApiMObservabilidade`: simula o registro de eventos de observabilidade.

Essas integrações não devem realizar chamadas reais à internet.

Cada integração deve possuir uma abstração apropriada e uma implementação mockada ou fake. Os nomes dos conectores devem ser preservados para facilitar a identificação durante a aula.

A integração de observabilidade deve registrar eventos relevantes, como pedido criado, pedido persistido e falha de integração, sem exigir uma plataforma externa.

O sistema deve permitir substituir as implementações mockadas por outras implementações mediante configuração de injeção de dependência, sem alterar as regras de negócio.

## 5. Demonstração de injeção de dependência

A lógica de negócio deve depender de pelo menos uma abstração injetada por DI. Preferencialmente, demonstrar múltiplas dependências, incluindo persistência, e-mail, entrega e observabilidade.

Exemplo conceitual:

```csharp
public class CriarPedidoService
{
    private readonly IPedidoRepository _repository;
    private readonly IEmailGateway _email;
    private readonly IDeliveryGateway _delivery;
    private readonly IObservabilityGateway _observability;

    public CriarPedidoService(
        IPedidoRepository repository,
        IEmailGateway email,
        IDeliveryGateway delivery,
        IObservabilityGateway observability)
    {
        _repository = repository;
        _email = email;
        _delivery = delivery;
        _observability = observability;
    }
}
```

O exemplo é ilustrativo. Adapte nomes, localização das interfaces e estrutura de classes à arquitetura de cada projeto.

Não force todas as arquiteturas a utilizar exatamente a mesma organização de pastas. A lógica de negócio deve permanecer equivalente, mas a estrutura deve refletir os princípios de cada arquitetura.

Demonstrar o registro das dependências no contêiner de DI do .NET e explicar como trocar uma implementação.

## 6. Tecnologia e compatibilidade

Utilizar:

- C#.
- .NET 10.
- ASP.NET Core.
- Razor Views nos projetos web.
- Injeção de dependência nativa do .NET.
- xUnit para testes de unidade.
- Armazenamento em memória.
- Código compatível com Windows, Linux e macOS.

Utilizar um estilo de solução tradicional baseado em arquivos `.csproj` e pastas convencionais, sem depender de ferramentas proprietárias ou geradores externos.

Interprete “Legacy Style” como **estrutura tradicional de projetos .NET**, mantendo compatibilidade com o SDK .NET 10. Não utilizar sintaxe ou APIs obsoletas apenas para simular versões antigas do .NET.

Evite dependências NuGet desnecessárias. Se algum pacote adicional for utilizado, justifique sua necessidade no README.

## 7. Testes automatizados

Cada projeto deve possuir seu próprio projeto de testes com xUnit.

**Mínimo obrigatório: dois cenários de testes de unidade por arquitetura.**

Implemente pelo menos estes cenários:

1. **Pedido válido:** quando a pizza existe e a quantidade é válida, o total é calculado corretamente e o pedido é persistido.
2. **Quantidade inválida:** quando a quantidade é zero ou negativa, a operação é rejeitada e o pedido não é persistido.

Sempre que possível, inclua cenários adicionais:

- Pizza inexistente.
- Falha no envio de e-mail.
- Falha na solicitação de entrega.
- Verificação de que a lógica de negócio utiliza as abstrações esperadas.

Utilize fakes ou mocks apropriados, sem acessar recursos externos.

Os testes devem ser determinísticos, independentes entre si e executáveis por linha de comando.

## 8. Qualidade e organização do código

O código deve:

- Utilizar nomes claros e consistentes em português ou inglês, sem misturar os idiomas aleatoriamente.
- Conter comentários em português nos pontos importantes para o aprendizado.
- Explicar decisões arquiteturais relevantes, não comentar cada linha de código.
- Seguir convenções idiomáticas de C#.
- Evitar classes gigantes, métodos excessivamente longos e responsabilidades misturadas.
- Tratar erros de maneira explícita.
- Não utilizar `static` como solução genérica para evitar DI.
- Não implementar abstrações artificiais sem uma justificativa didática.
- Não utilizar bancos de dados externos nem chamadas reais aos conectores simulados.
- Ser simples o suficiente para um iniciante acompanhar o fluxo de execução.

Utilize métodos assíncronos quando fizer sentido para as interfaces e operações simuladas, sem adicionar complexidade assíncrona desnecessária.

## 9. Depuração

Os quatro projetos devem ser facilmente depuráveis.

Para cada projeto:

- Incluir configuração de execução adequada no Visual Studio Code ou instruções equivalentes para Visual Studio e Rider.
- Permitir executar a aplicação pelo terminal com `dotnet run`.
- Explicar como definir breakpoints em pontos relevantes.
- Indicar uma sequência de execução para acompanhar o fluxo desde a requisição até a regra de negócio, persistência e integrações.
- Fornecer exemplos de payload ou entradas de teste, quando aplicável.
- Evitar configurações locais que dependam de caminhos absolutos ou de segredos.

Quando houver configuração específica do VS Code, incluir arquivos `.vscode/launch.json` e `tasks.json` válidos, se forem necessários para uma experiência de depuração reproduzível.

## 10. README obrigatório

Cada diretório deve conter um README.md próprio, escrito em português.

O README deve explicar:

1. Objetivo do projeto.
2. Arquitetura demonstrada.
3. Conceitos fundamentais.
4. Estrutura de diretórios e responsabilidade de cada projeto.
5. Fluxo de execução do caso de uso.
6. Diagrama arquitetural em Mermaid.
7. Tecnologias e dependências utilizadas.
8. Pré-requisitos.
9. Como restaurar, compilar, executar e testar.
10. Como executar no Windows, Linux e macOS.
11. Como depurar passo a passo.
12. Como trocar uma integração mockada por outra implementação.
13. Limitações do exemplo e simplificações feitas para fins didáticos.

Os comandos devem ser apresentados para cada sistema operacional quando houver diferenças relevantes.

Utilizar comandos multiplataforma sempre que possível, como:

```bash
dotnet restore
dotnet build
dotnet test
dotnet run
```

Informar como acessar a aplicação no navegador e como encerrar a execução.

Criar também um README.md na raiz, comparando as quatro arquiteturas, explicando quando cada uma é apropriada e fornecendo uma sequência recomendada para ensinar os exemplos em sala de aula.

## 11. Diagramas arquiteturais

Cada projeto deve possuir pelo menos um diagrama Mermaid que represente sua arquitetura real.

Os diagramas devem:

- Mostrar os principais componentes.
- Mostrar as dependências e a direção das chamadas.
- Identificar os adaptadores e as portas no projeto Hexagonal.
- Identificar o domínio central e a direção das dependências na Onion.
- Identificar Model, View e Controller nos projetos MVC.
- Distinguir claramente os princípios combinados no quarto projeto.

Não gerar diagramas genéricos que não correspondam à implementação real.

## 12. Estrutura esperada do repositório

Utilize uma estrutura semelhante a esta, adaptando-a quando necessário:

```text
PizzariaByte/
├── README.md
├── mvc/
│   ├── README.md
│   ├── PizzariaByte.Mvc.sln
│   ├── src/
│   │   └── ...
│   └── tests/
│       └── ...
├── hexagonal/
│   ├── README.md
│   ├── PizzariaByte.Hexagonal.sln
│   ├── src/
│   │   └── ...
│   └── tests/
│       └── ...
├── onion/
│   ├── README.md
│   ├── PizzariaByte.Onion.sln
│   ├── src/
│   │   └── ...
│   └── tests/
│       └── ...
└── hexagonal_and_onion/
    ├── README.md
    ├── PizzariaByte.Combined.sln
    ├── src/
    │   └── ...
    └── tests/
        └── ...
```

Essa estrutura é uma referência. O importante é manter os quatro exemplos independentes, fáceis de localizar e executar.

## 13. Critérios de aceite

O trabalho somente será considerado concluído quando:

- [ ] Os quatro projetos estiverem implementados nos diretórios corretos.
- [ ] Todos utilizarem o mesmo cenário e as mesmas regras de negócio.
- [ ] Cada aplicação possuir frontend, backend, persistência em memória e integrações mockadas.
- [ ] A injeção de dependência estiver implementada e demonstrável.
- [ ] Cada arquitetura possuir pelo menos dois testes de unidade.
- [ ] Todos os projetos compilarem com o SDK .NET 10.
- [ ] Todos os testes automatizados passarem.
- [ ] Cada projeto possuir README com instruções para Windows, Linux e macOS.
- [ ] Os diagramas corresponderem à implementação real.
- [ ] For possível executar e depurar os quatro projetos localmente.
- [ ] Nenhuma integração exigir acesso real à internet.
- [ ] O projeto combinado utilizar MVC, Onion e Hexagonal de maneira coerente.
- [ ] A raiz possuir documentação comparativa e instruções gerais.

## 14. Estratégia de execução

Trabalhe em etapas:

1. Inspecione o ambiente e confirme a disponibilidade do SDK .NET 10.
2. Defina o contrato funcional comum aos quatro projetos.
3. Implemente o primeiro projeto completo.
4. Implemente os outros projetos respeitando as diferenças arquiteturais.
5. Execute restauração, compilação e testes em cada diretório.
6. Corrija os problemas encontrados antes de considerar a implementação concluída.
7. Revise os READMEs, diagramas, instruções multiplataforma e depuração.
8. Apresente um resumo final com a estrutura criada, os comandos de execução, os resultados reais dos testes e quaisquer limitações.

Não declare que compilação, testes ou execução foram validados sem efetivamente executá-los.

Se houver limitações no ambiente que impeçam alguma validação, informe exatamente o que foi possível verificar e o que ficou pendente.

**Prioridade final:** clareza didática, fidelidade arquitetural, código executável e facilidade de comparação entre os quatro projetos. Não sacrifique esses objetivos para adicionar funcionalidades desnecessárias.

---

## Domain Concept Identification

> **Estado do codebase (base desta análise):** o repositório é *greenfield*. A branch `main` não tem commits. Os únicos arquivos são `prompt.md` e os comandos SPDD em `.claude/commands/`. Existem quatro diretórios **vazios**: `mvc/`, `hexagonal/`, `onion/` e `hexagonal_onion_mvc/`. Não há `spdd/prompt/` nem análises anteriores, nem `.gitignore`, `global.json`, solução ou `.csproj`. O ambiente tem os SDKs **.NET 10.0.112** e **10.0.401** instalados em `/usr/local/share/dotnet/sdk`. Por isso, todas as convenções abaixo vêm do requisito e das convenções do ASP.NET Core / .NET 10, não de código existente.

### Existing Concepts (from codebase)

- **Nenhum conceito de domínio existe no código.** Não há entidades, tabelas, classes ou contratos para reaproveitar.
- **Estrutura de diretórios pré-criada**: `mvc/`, `hexagonal/` e `onion/` correspondem ao requisito. `hexagonal_onion_mvc/` **diverge** do nome exigido (`hexagonal_and_onion/`). Ver *Requirement Ambiguities*.
- **Workflow SPDD** (`.claude/commands/spdd-*.md`): é ferramenta de processo, não código de produto. Define que os artefatos seguintes vão para `spdd/prompt/`.

### New Concepts Required

Conceitos de negócio, com a **mesma semântica nos quatro projetos**:

- **Pizza (item do cardápio)**: produto vendável, com identificação, nome e preço definido. É a referência de preço para o cálculo do pedido. Não muda durante a execução.
- **Cardápio**: conjunto fixo e previsível de pelo menos três pizzas, iniciado na partida da aplicação. É a fonte de verdade para validar se a pizza existe e para obter o preço. Os dados iniciais são idênticos nos quatro projetos.
- **Pedido**: agregado central do caso de uso. Referencia **uma** pizza do cardápio e guarda a quantidade, o endereço de entrega, o subtotal, a taxa de entrega, o total, um identificador e um status. É criado apenas quando todas as regras de validação passam.
- **Endereço de entrega**: dado informado pelo cliente e exigido para o pedido e para a solicitação de entrega. No requisito é texto livre e não influencia a taxa.
- **Taxa de entrega**: valor calculado por uma regra simples, documentada e **conceitualmente compartilhada** entre os quatro projetos. Compõe o total do Pedido.
- **Status do pedido**: ciclo de vida mínimo, com um status inicial atribuído na criação. Pode também refletir o resultado das integrações, a depender da política de falha (ver Strategic Approach).
- **Resultado / Status de integração**: registro, por conector, de sucesso ou falha (com mensagem compreensível). Aparece no resumo do pedido. Ele não é o estado do Pedido, mas é exibido junto com ele.
- **Resultado da operação de criação**: resposta do caso de uso para a interface. Pode ser sucesso (pedido, total e status das integrações) ou rejeição (mensagens de validação). É o que permite ter mensagens de sucesso e de erro compreensíveis.
- **Armazenamento de pedidos em memória**: repositório que dura o tempo de vida da aplicação. Os dados se perdem ao reiniciar, e cada teste usa uma instância isolada.
- **ApiEmail**: conector simulado que envia a confirmação do pedido. Recebe o Pedido já persistido.
- **ApiDeliveryAiQFome**: conector simulado que solicita a entrega. Recebe o Pedido e o endereço.
- **ApiMObservabilidade**: conector simulado que registra eventos (pedido criado, pedido persistido, falha de integração). É transversal ao caso de uso e não muda o resultado de negócio.
- **Caso de uso "Criar Pedido"** (`CriarPedidoService` ou equivalente): orquestra validação, cálculo, persistência e integrações. É o ponto onde a DI fica visível. O nome e o lugar variam por arquitetura (Model/Service no MVC, núcleo no Hexagonal, Application Service na Onion, Application + Ports no combinado).

**Relações conceituais e limites de ciclo de vida**

- Cardápio 1 —contém→ N Pizzas. Leitura apenas, com ciclo de vida igual ao da aplicação.
- Pedido N —referencia→ 1 Pizza. O Pedido deve **copiar o preço unitário** no momento da criação para que o total seja estável.
- O Pedido tem seu próprio endereço, taxa, total, identificador e status.
- O Pedido é guardado pelo Armazenamento em memória, que o possui depois da persistência.
- Os conectores consomem o Pedido persistido e não o modificam, exceto ao registrar o status de integração, se essa for a opção escolhida.
- A Observabilidade observa todo o fluxo, e uma falha dela nunca deve impedir o pedido.

### Key Business Rules

- **RN01 – Cardápio mínimo**: pelo menos três pizzas com preço definido e positivo. *(Cardápio, Pizza)*
- **RN02 – Quantidade válida**: a quantidade precisa ser maior que zero. Zero ou negativo rejeita a operação **sem persistir**. *(Pedido)*
- **RN03 – Pizza existente**: a pizza solicitada precisa existir no cardápio. Caso contrário, a operação é rejeitada sem persistir. *(Pedido, Cardápio)*
- **RN04 – Cálculo do total**: subtotal = preço unitário × quantidade, e total = subtotal + taxa de entrega. Valores monetários usam tipo decimal, nunca ponto flutuante binário. *(Pedido, Taxa)*
- **RN05 – Taxa de entrega**: regra simples e documentada, igual nos quatro projetos. A regra concreta **não está definida** no requisito (ver Ambiguities). *(Taxa, Pedido)*
- **RN06 – Identidade e status inicial**: todo Pedido criado recebe um identificador único e um status inicial definido. *(Pedido, Status)*
- **RN07 – Persistência**: o pedido confirmado e válido é salvo no armazenamento em memória. Pedido rejeitado nunca é salvo. *(Pedido, Armazenamento)*
- **RN08 – Consistência diante de falhas de integração**: falha em e-mail ou entrega não pode deixar o pedido em estado inconsistente. Por exemplo, o pedido não pode estar salvo com status "sucesso total" quando a entrega falhou, e um pedido não salvo não pode ser notificado. *(Pedido, Integrações, Status)*
- **RN09 – Mensagens compreensíveis**: todo resultado, de sucesso ou de erro, mostra ao usuário uma mensagem clara em português. *(Resultado da operação)*
- **RN10 – Paridade entre projetos**: mesmas regras, mesmos dados iniciais, mesmas entradas e mesmos resultados esperados nos quatro projetos. *(todos)*

**Regras implícitas a tornar explícitas**

- **RI01 – Endereço obrigatório**: o endereço não pode ser vazio nem só espaços. O requisito pede "informar" o endereço mas não diz que é obrigatório.
- **RI02 – Limite superior de quantidade**: não está definido. Sem limite, um valor como 1.000.000 gera totais absurdos e expõe overflow e formatação ruim.
- **RI03 – Preço congelado no pedido**: o total não pode mudar depois de calculado, mesmo que o cardápio mude, o que é teórico já que o cardápio é fixo.
- **RI04 – Observabilidade é best-effort**: falha do `ApiMObservabilidade` não altera o resultado do pedido.
- **RI05 – Ordem de efeitos**: validar → criar → persistir → integrar. Integrações nunca rodam para pedido não persistido.
- **RI06 – Moeda e cultura**: os valores são em Reais (BRL) e formatados em pt-BR, igualmente em Windows, Linux e macOS.
- **RI07 – Determinismo dos testes**: nenhum estado estático compartilhado. Cada teste monta seu próprio repositório e seus fakes.

---

## Strategic Approach

### Solution Direction

- **Quatro soluções independentes** (`.sln` próprio, `src/`, `tests/` e `README.md` em cada diretório), sem biblioteca compartilhada entre elas. A paridade vem de um **contrato funcional comum documentado** no README raiz: cardápio inicial, regra da taxa, status, mensagens e cenários de entrada e saída esperados. Cada projeto reimplementa esse contrato segundo sua arquitetura.
- **Fluxo de dados comum (conceitual)**: formulário Razor → componente de entrada (Controller/Page/adapter) → caso de uso "Criar Pedido" → validação e cálculo no domínio → persistência em memória → integrações (e-mail, entrega) → eventos de observabilidade em cada marco → resultado → página de resumo.
- **Diferenciação arquitetural** (o que muda entre os projetos é *onde* cada responsabilidade fica e *para onde* as dependências apontam):
  - **MVC**: um único projeto web. As regras ficam nos Models e em serviços de modelo. Os Controllers coordenam e as Views apresentam. As integrações ficam atrás de interfaces simples registradas na DI, sem vocabulário de portas e adaptadores.
  - **Hexagonal**: núcleo da aplicação (domínio + caso de uso + **portas** de entrada e saída) isolado em projeto próprio. **Adaptadores** de entrada (web) e de saída (persistência em memória, três conectores mock) em projetos separados que dependem do núcleo. Um ponto de composição (host) liga portas a adaptadores. A demonstração de troca de adaptador é explícita.
  - **Onion**: camadas concêntricas como projetos (Domain → Application → Infrastructure / Web). As interfaces ficam na camada que *precisa* delas: repositório no Domain ou Application, gateways na Application. A Infrastructure implementa. A camada Web/host é a mais externa e faz a composição de DI.
  - **Combinado**: Onion organiza os projetos e a direção das dependências (Domain ← Application ← Adapters/Web). Hexagonal nomeia as interfaces de saída da Application como **Ports** e as implementações como **Adapters**. MVC organiza só a camada web: Controllers + ViewModels + Views como adaptador de entrada. Um diagrama mostra onde cada princípio aparece.
- **Aproveitar o que o .NET já oferece**: `Microsoft.Extensions.DependencyInjection` nativo, model binding e validação do ASP.NET Core, xUnit. Nenhum pacote NuGet além do template de testes, e fakes escritos à mão.

### Key Design Decisions

- **D1 – Política diante de falha de integração (RN08)**: (a) rejeitar e desfazer o pedido se a entrega falhar (compensação), contra (b) manter o pedido persistido e registrar o status de cada integração, contra (c) transação "tudo ou nada" antes de persistir. A opção (a) exige compensação, que é complexa para iniciantes. A (c) é impossível com efeitos externos. A (b) é simples, auditável e casa com "visualizar o status das integrações". → **Recomendação: (b)**. Persistir primeiro e depois chamar cada integração isoladamente, capturando a falha. O pedido fica com status coerente (ex.: "Confirmado" quando tudo funciona, "Confirmado – entrega pendente" quando a entrega falha) e o resumo mostra o resultado por conector. Observabilidade é best-effort. **Precisa de confirmação** (ver Ambiguities).
- **D2 – Como sinalizar erros de validação**: exceções de domínio contra um objeto de resultado explícito. Exceções são idiomáticas mas escondem o fluxo para iniciantes. Um resultado explícito deixa sucesso e rejeição visíveis. → **Recomendação**: invariantes do domínio protegidas por guardas (exceção de domínio quando violadas), e o caso de uso devolvendo um **resultado explícito** (sucesso ou falha com mensagens) para a interface. Isso cumpre "tratar erros de maneira explícita" sem usar exceção como controle de fluxo na UI.
- **D3 – Granularidade física das camadas**: pastas contra projetos `.csproj` separados. Pastas são mais simples, mas o compilador não impede dependências proibidas. → **Recomendação**: no MVC, um projeto web + um de testes. No Hexagonal, Onion e Combinado, **projetos separados por camada/anel**, para que a direção das dependências seja garantida pelas referências de projeto. Isso é um ponto didático central e vale o custo de alguns `.csproj` a mais.
- **D4 – Tecnologia de UI nos projetos Hexagonal e Onion**: o requisito exige "Razor Views nos projetos web" e reserva ASP.NET Core MVC para os projetos 1 e 4. Usar MVC nos projetos 2 e 3 confunde a comparação. Razor Pages também usa `.cshtml` e não impõe o padrão MVC. → **Recomendação**: **Razor Pages** como adaptador de entrada (Hexagonal) ou camada de apresentação (Onion), deixando claro no README que é uma escolha de tecnologia de UI, não de arquitetura. **Precisa de confirmação** (ver Ambiguities).
- **D5 – Convenção de idioma dos identificadores**: português no domínio contra inglês em tudo. O exemplo do requisito mistura `CriarPedidoService`, `IPedidoRepository` e `IEmailGateway`, e os conectores têm nomes fixos em português/marca. → **Recomendação**: **conceitos de domínio em português** (Pedido, Pizza, Cardapio, TaxaEntrega) e **sufixos de papel técnico em inglês** (Repository, Gateway, Service, Controller, Port, Adapter), seguindo o exemplo do enunciado. Documentar a regra no README raiz para que não pareça mistura aleatória.
- **D6 – Simulação de falhas nos conectores mock**: entrada "mágica" (ex.: endereço com "falha"), contra configuração (`appsettings`), contra apenas fakes de teste. Entrada mágica é prática em aula mas mistura regra de teste na lógica. Configuração é explícita e ainda demonstra a troca via DI. → **Recomendação**: fakes de **produção** configuráveis por `appsettings` (ex.: ligar a falha de e-mail ou de entrega) para a demonstração em sala, e **fakes de teste** dedicados (que sempre falham, sempre funcionam ou registram as chamadas) nos testes de unidade. Nenhum dos dois faz I/O de rede.
- **D7 – Ciclo de vida na DI**: o repositório em memória como **Singleton**, senão os dados somem a cada requisição. Caso de uso e gateways como **Scoped/Transient**. Coleção thread-safe no repositório. → **Recomendação**: Singleton para cardápio e repositório, Scoped para caso de uso e conectores. Documentar o motivo no README, já que é uma armadilha comum para iniciantes.
- **D8 – Identificador do pedido**: `Guid` (realista, gerado no domínio, sem estado compartilhado) contra inteiro sequencial (legível em aula, "Pedido #1", mas exige contador no repositório). → **Recomendação**: **Guid** gerado na criação do Pedido, com exibição abreviada na UI se ajudar. Os testes verificam que existe, não o valor.
- **D9 – Formato da solução e pin do SDK**: no .NET 10, `dotnet new sln` gera **`.slnx`** por padrão, mas o requisito pede `.sln` ("Legacy Style" / estrutura tradicional). Há dois SDKs 10.x instalados. → **Recomendação**: gerar `.sln` clássico de forma explícita, `TargetFramework net10.0`, e um `global.json` por projeto (ou na raiz) fixando a banda 10.0 com `rollForward: latestFeature`, para reprodutibilidade em Windows, Linux e macOS.
- **D10 – Frontend sem dependências pesadas**: o template padrão (`dotnet new mvc`) traz Bootstrap e jQuery em `wwwroot/lib` (centenas de arquivos). → **Recomendação**: projetos montados à mão, com um CSS próprio pequeno e **validação no servidor** (DataAnnotations + validação do domínio). Validação no cliente é opcional e fica fora se exigir bibliotecas JS. Menos ruído para o aluno.
- **D11 – Estratégia de testes**: Moq/NSubstitute contra fakes escritos à mão. Bibliotecas de mock adicionam dependência NuGet e sintaxe extra. → **Recomendação**: **fakes à mão** com xUnit puro. Eles também servem de material didático ("um adaptador de teste é só mais um adaptador"). Cobrir os dois cenários obrigatórios e os opcionais (pizza inexistente, falha de e-mail, falha de entrega, uso das abstrações).
- **D12 – Depuração**: `.vscode/launch.json` + `tasks.json` dentro de cada diretório (para abrir cada projeto isoladamente) e, opcionalmente, na raiz com uma configuração por projeto. As portas HTTP são fixas e distintas por projeto, via `launchSettings.json`, para rodar os quatro ao mesmo tempo em aula. → **Recomendação**: as duas coisas, sem caminhos absolutos.

### Alternatives Considered

- **Biblioteca de domínio compartilhada entre os quatro projetos**: rejeitada. Quebra a independência exigida e esconde exatamente o que se quer comparar, que é onde cada arquitetura coloca o domínio.
- **EF Core InMemory como armazenamento**: rejeitada. Adiciona uma dependência NuGet sem ganho didático e o próprio requisito afasta o domínio do EF Core. Uma coleção em memória atrás de um repositório basta.
- **Minimal APIs / API JSON + SPA como frontend**: rejeitada. O requisito exige Razor e diz explicitamente que o MVC "não [pode ser] apenas uma API renomeada como MVC".
- **ASP.NET Core MVC também nos projetos Hexagonal e Onion**: rejeitada como padrão (ver D4) porque apaga a diferença em relação aos projetos 1 e 4. Fica como alternativa se o professor preferir uniformidade de UI.
- **Mensageria ou outbox para as integrações**: rejeitada. É complexidade desnecessária para iniciantes, e a consistência pedida é atendida pela política D1(b).
- **Adaptadores mock via `HttpClient` + `HttpMessageHandler` fake**: rejeitada. Aproxima do mundo real mas acrescenta uma camada de infraestrutura HTTP que distrai do objetivo. Os mocks implementam a abstração diretamente.
- **Usar templates `dotnet new mvc/webapp` sem ajustes**: rejeitada (ver D10). Muito boilerplate (Bootstrap, jQuery, Privacy, Error) que polui a comparação.

---

## Risk & Gap Analysis

### Requirement Ambiguities

- **Nome do diretório do projeto 4**: o requisito exige `./hexagonal_and_onion` (e `PizzariaByte.Combined.sln`), mas o repositório já tem o diretório vazio `hexagonal_onion_mvc/`. Definir qual nome vale e se o diretório divergente deve ser removido ou renomeado.
- **Raiz do repositório contra `PizzariaByte/`**: a estrutura de referência mostra uma pasta `PizzariaByte/` como raiz. Os diretórios já existem na raiz atual do repositório. Assumir a raiz atual como "PizzariaByte/" ou criar a subpasta?
- **Regra concreta da taxa de entrega (RN05)**: não foi definida. Proposta para confirmar: **taxa fixa de R$ 8,00, isenta quando o subtotal ≥ R$ 100,00**. É simples, tem um ramo testável e não depende do endereço.
- **Dados iniciais do cardápio**: nomes e preços não definidos. Proposta: **Margherita R$ 45,00; Calabresa R$ 50,00; Quatro Queijos R$ 55,00**, com identificadores estáveis.
- **Política de falha de integração (RN08)**: "estado inconsistente" não está definido. É preciso confirmar D1(b): o pedido continua persistido e o status mostra a falha do conector. A alternativa seria rejeitar ou cancelar o pedido quando a entrega falha.
- **Conjunto de status do pedido**: só se exige "status inicial". Definir os valores (ex.: `Recebido` como inicial, `Confirmado`, `EntregaPendente`) e se a falha de integração muda o status do pedido ou só o status da integração.
- **"Confirmar o pedido" (passo 5)**: é a submissão do formulário ou uma tela intermediária de revisão ("confirmar?") antes de criar? O passo intermediário acrescenta uma tela e um estado temporário que o requisito não descreve. Recomendação: a submissão do formulário é a confirmação.
- **Uma pizza por pedido**: o requisito diz "escolher uma pizza" e proíbe carrinho complexo. Assumir **um único item por pedido**.
- **"Razor Views" nos projetos Hexagonal e Onion**: aceitar Razor Pages (D4) ou exigir ASP.NET Core MVC Views?
- **Visualização de pedidos persistidos**: o requisito pede o resumo do pedido criado, mas não pede listagem nem consulta posterior. Sem isso, a persistência só é visível pelos testes e pelo debugger. Confirmar se uma página simples "pedidos realizados" é bem-vinda (útil para mostrar que os dados somem ao reiniciar) ou se conta como funcionalidade desnecessária.
- **"Legacy Style"**: o requisito interpreta como estrutura tradicional de `.csproj`. Confirmar que isso inclui o formato `.sln` em vez de `.slnx` (D9).
- **Idioma do código**: "português ou inglês, sem misturar aleatoriamente". Confirmar a convenção D5 (domínio em PT, sufixos técnicos em EN).

### Edge Cases

- **Quantidade zero, negativa ou não numérica no formulário**: o model binding falha antes do domínio. A mensagem precisa ser compreensível e o pedido não é persistido. O caminho da UI e o caminho do domínio têm de dar o mesmo resultado.
- **Quantidade muito grande**: sem limite superior, o total pode ficar irreal. Definir um máximo (ex.: 50) ou documentar que não há.
- **Pizza inexistente vinda de formulário adulterado**: um ID fora do cardápio deve ser rejeitado pelo domínio, não só pelo dropdown.
- **Endereço vazio ou só espaços**: precisa ser rejeitado (RI01).
- **Subtotal exatamente no limite da isenção da taxa** (ex.: R$ 100,00): a regra precisa dizer se usa ≥ ou >. Isso vira um caso de teste.
- **Falha de e-mail com sucesso na entrega, e o inverso**: os dois resultados de integração precisam ser independentes. Falha no primeiro conector não pode impedir a chamada do segundo.
- **Falha do próprio ApiMObservabilidade**: não pode derrubar o pedido nem ocultar o resultado.
- **Duplo envio do formulário (F5 / clique duplo)**: pode criar dois pedidos. Recomenda-se Post-Redirect-Get para a página de resumo, que também serve de lição de MVC.
- **Resumo acessado depois de reiniciar a aplicação**: o pedido não existe mais. É preciso uma mensagem amigável de "pedido não encontrado", o que também ilustra a perda de dados em memória.
- **Requisições concorrentes**: dois pedidos simultâneos gravando no repositório Singleton exigem uma coleção thread-safe.

### Technical Risks

- **`.slnx` como padrão no SDK 10**: risco de entregar uma solução fora do formato pedido. Mitigação: gerar `.sln` explicitamente e verificar.
- **Dois SDKs 10.x instalados (10.0.112 e 10.0.401)**: os comportamentos podem divergir entre as máquinas dos alunos. Mitigação: `global.json` com `rollForward: latestFeature` e documentar o SDK mínimo no README.
- **Formatação de moeda pt-BR multiplataforma**: em Linux/containers com `InvariantGlobalization` ou sem ICU, "R$" e a vírgula decimal podem sair errados. Mitigação: fixar a cultura pt-BR no host e não depender da cultura do sistema. Os testes comparam valores decimais, não strings formatadas.
- **Ciclo de vida errado na DI**: um repositório Scoped "perde" os pedidos entre requisições, e um caso de uso Singleton capturando dependências Scoped gera erro em tempo de execução. Mitigação: D7, com explicação no README.
- **Erosão arquitetural entre os projetos**: o MVC pode acabar parecendo Hexagonal (interfaces por toda parte), e Hexagonal e Onion podem ficar idênticos. Mitigação: vocabulário e organização próprios por projeto (D3–D5) e uma seção comparativa no README raiz com o "que muda e por quê".
- **Diagramas Mermaid divergentes da implementação**: risco explícito no requisito. Mitigação: gerar os diagramas a partir das referências reais entre `.csproj` e das classes criadas, e revisá-los no passo de revisão.
- **Testes dependentes de estado**: estado `static` ou repositório compartilhado quebram a independência. Mitigação: cada teste instancia seus próprios fakes e repositório, sem `static` mutável.
- **Conflito de portas ao rodar os quatro projetos ao mesmo tempo**: mitigação com portas fixas e distintas em `launchSettings.json`, documentadas.
- **Certificado HTTPS de desenvolvimento**: `dotnet dev-certs` pode não estar confiável no Linux e exigir passos extras. Mitigação: perfil HTTP disponível e instruções por sistema operacional.
- **Ausência de `.gitignore`**: `bin/` e `obj/` seriam versionados. Mitigação: incluir `.gitignore` padrão .NET na raiz.
- **Volume de entrega**: quatro soluções completas, com READMEs, diagramas, depuração e testes, são muitos arquivos. Há risco de inconsistência entre projetos. Mitigação: fechar o contrato funcional comum primeiro (passo 2 da estratégia do requisito) e usar o primeiro projeto como referência de comportamento.

### Acceptance Criteria Coverage

| AC# | Description | Addressable? | Gaps/Notes |
|-----|-------------|--------------|------------|
| 1 | Os quatro projetos implementados nos diretórios corretos | Partial | Coberto pela abordagem, mas depende de resolver `hexagonal_onion_mvc/` contra `hexagonal_and_onion/` e a questão da raiz `PizzariaByte/`. |
| 2 | Todos usam o mesmo cenário e as mesmas regras de negócio | Partial | Exige o contrato funcional comum. A regra da taxa, o cardápio e os status **ainda não definidos** precisam ser confirmados. |
| 3 | Cada aplicação com frontend, backend, persistência em memória e integrações mockadas | Yes | Razor (MVC/Pages) + caso de uso + repositório Singleton + três conectores fake. A escolha de UI nos projetos 2 e 3 depende de D4. |
| 4 | Injeção de dependência implementada e demonstrável | Yes | DI nativa, com o caso de uso dependendo de quatro abstrações. Troca de implementação demonstrada por configuração (D6) e documentada. |
| 5 | Pelo menos dois testes de unidade por arquitetura | Yes | Os dois obrigatórios mais os opcionais (pizza inexistente, falhas de e-mail e entrega, uso das abstrações), com fakes à mão. |
| 6 | Todos compilam com o SDK .NET 10 | Yes | SDK 10.0.112 e 10.0.401 disponíveis no ambiente. `global.json` e o formato `.sln` precisam de atenção (D9). |
| 7 | Todos os testes automatizados passam | Yes | Verificável via `dotnet test` por diretório. A validação deve ser **executada de fato**. |
| 8 | README com instruções para Windows, Linux e macOS | Yes | 13 itens obrigatórios por README. Diferenças por SO: dev-certs, encerramento (Ctrl+C), shells. |
| 9 | Diagramas correspondem à implementação real | Partial | Não dá para verificar automaticamente. Exige revisão manual contra as referências entre `.csproj`. |
| 10 | Executar e depurar os quatro projetos localmente | Partial | `dotnet run` é verificável no ambiente. A depuração por breakpoint em VS Code, Visual Studio e Rider só pode ser documentada e validada estruturalmente (JSON válido), não executada de forma automática. |
| 11 | Nenhuma integração exige acesso real à internet | Yes | Os conectores são implementações em memória sem `HttpClient` real. |
| 12 | Projeto combinado usa MVC, Onion e Hexagonal de forma coerente | Yes | Onion para camadas e direção, Hexagonal para Ports/Adapters de saída, MVC na camada web. Mais o diagrama de onde cada princípio aparece. |
| 13 | Raiz com documentação comparativa e instruções gerais | Yes | README raiz com comparação, quando usar cada uma, sequência de ensino e o **contrato funcional comum** (cardápio, taxa, status, cenários). |

**Gaps entre os ACs e o escopo completo**: os ACs não citam explicitamente (a) mensagens de erro compreensíveis na UI (RN09), (b) o tratamento consistente de falhas de integração (RN08), (c) a explicação de que os dados se perdem ao reiniciar, nem (d) os arquivos `.vscode/launch.json` e `tasks.json`. Todos são exigidos no corpo do requisito e precisam ser tratados como critérios implícitos na fase REASONS Canvas.

---

## Decisões Confirmadas pelo Usuário (2026-10-28)

| # | Questão | Decisão |
|---|---------|---------|
| a | Diretório do projeto 4 | Usar o diretório existente **`./hexagonal_onion_mvc/`** (substitui `./hexagonal_and_onion/` do requisito). A solução mantém o nome `PizzariaByte.Combined.sln`. |
| b | Regra da taxa e cardápio | **Taxa fixa de R$ 8,00, isenta quando o subtotal ≥ R$ 100,00.** Cardápio: **Margherita R$ 45,00; Calabresa R$ 50,00; Quatro Queijos R$ 55,00**, com identificadores estáveis. |
| c | Política de falha de integração | **D1(b)**: validar → criar → persistir → integrar. Cada conector é chamado de forma isolada e tem sua falha capturada. O pedido continua persistido com status coerente, e o resumo mostra o resultado de cada conector. Observabilidade é best-effort. |
| d | UI nos projetos Hexagonal e Onion | **Razor Pages** (D4). ASP.NET Core MVC fica restrito aos projetos `mvc/` e `hexagonal_onion_mvc/`. |
| e | Formato da solução | **`.sln` clássico** gerado explicitamente (não `.slnx`), `net10.0` e `global.json` com `rollForward: latestFeature` (D9). |

As demais recomendações da análise valem como direção padrão: D2, D3, D5–D8 e D10–D12, a raiz do repositório como "PizzariaByte/", um item por pedido, a submissão do formulário como confirmação e Post-Redirect-Get.
