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