# Documentação Técnica — ByteFix

## 1. Descrição geral da aplicação
ByteFix é uma aplicação web para gestão de reparações informáticas. O sistema foi desenhado para organizar a informação da oficina, acompanhar o estado das reparações e manter os dados de clientes, equipamentos, técnicos e intervenções num único local.

A aplicação oferece um fluxo principal de trabalho de assistência técnica:
- registar clientes
- registar equipamentos associados
- criar reparações
- atribuir técnicos
- registar intervenções
- seguir custos e estado da reparação
- consultar indicadores do dashboard

## 2. Objetivos do projeto
Os principais objetivos são:
- centralizar a atividade da oficina em uma aplicação web
- reduzir erros manuais e duplicações de informação
- acompanhar cada equipamento durante o ciclo de reparação
- controlar custos de mão de obra e peças
- manter histórico de intervenções
- melhorar a visibilidade operacional com um dashboard real

## 3. Tecnologias utilizadas
- C#
- ASP.NET Core MVC
- Entity Framework Core
- SQLite
- Bootstrap 5
- Tabler UI
- HTML/CSS

## 4. Arquitetura MVC
O projeto segue a arquitetura MVC (Model-View-Controller):
- Models: entidades do domínio e regras de negócio baseadas em data annotations.
- Views: interfaces em Razor para os formulários e listagens.
- Controllers: mediadores entre a UI e o acesso aos dados.

A aplicação também inclui ViewModels para apresentação do dashboard e organização dos dados que chegam à view.

## 5. Organização do projeto
Estrutura principal:

```text
BYTEFIX/
├── Controllers/
├── Data/
├── Documentation/
├── Migrations/
├── Models/
├── Services/
├── ViewModels/
├── Views/
├── wwwroot/
├── appsettings.json
├── ByteFix.csproj
├── ByteFix.db
├── Program.cs
├── README.md
└── ByteFix.Tests/
```

### Controllers
- HomeController: dashboard principal e página inicial.
- ClientesController: operações CRUD para clientes.
- EquipamentosController: operações CRUD para equipamentos.
- TecnicosController: operações CRUD para técnicos.
- ReparacoesController: operações CRUD para reparações.
- IntervencoesController: operações CRUD para intervenções.

### Models
- Cliente
- Equipamento
- Tecnico
- Reparacao
- Intervencao
- ReparacaoEstado

### ViewModels
- DashboardViewModel
- DashboardChartPointViewModel
- DashboardCostPointViewModel
- DashboardActivityViewModel
- DashboardAlertViewModel

## 6. Estrutura da base de dados SQLite
A base de dados é gerida pelo Entity Framework Core e persiste em SQLite. O ficheiro principal é `ByteFix.db`.

A estrutura inclui as tabelas:
- Clientes
- Equipamentos
- Tecnicos
- Reparacoes
- Intervencoes

A configuração do contexto é feita em `Data/ApplicationDbContext.cs`.

### Regras relevantes
- NIF único por cliente.
- Email único por técnico.
- Número de série único quando preenchido.
- Estado da reparação limitado aos valores definidos em `ReparacaoEstado`.
- Integração referencial com restrições para evitar eliminações acidentais.
- Custos em moeda são armazenados como inteiros em centimos para evitar problemas de precisão.

## 7. Relações entre as entidades
- Cliente tem vários Equipamentos.
- Equipamento pertence a um Cliente.
- Equipamento pode ter várias Reparações.
- Reparação pode ter várias Intervenções.
- Reparação pode ter um Técnico associado.
- Técnico pode estar associado a várias Reparações.

Estas relações são configuradas em `ApplicationDbContext` com `HasOne`, `HasMany` e regras `OnDelete(DeleteBehavior.Restrict)` para preservar integridade.

## 8. Organização dos Controllers
Os controllers implementam operações de CRUD e a lógica de validação básica:

- `Create`: apresenta formulário e guarda novos registos.
- `Edit`: ajusta registos existentes.
- `Delete`: apresenta confirmação e elimina registo se permitido.
- `Index`: lista os registos da entidade.

No caso de equipamentos e reparações, existem validações para impedir ações inválidas, como eliminar socio com relações existentes.

## 9. Organização dos Models e ViewModels
### Models
Os models têm propriedades fundamentais, data annotations e navegações.

Exemplos:
- Cliente: Nome, NIF, Email, Morada, Telefone.
- Equipamento: Marca, Modelo, TipoEquipamento, NumeroSerie, ClienteId.
- Tecnico: Nome, Email, Especialidade, Ativo.
- Reparacao: Estado, DescricaoAvaria, EquipamentoId, TecnicoId, DataEntrada.
- Intervencao: DescricaoTrabalho, TempoTrabalho, CustoMaoObra, CustoPecas.

### ViewModels
Os ViewModels servem para estruturar informação que vai para o dashboard e para a camada de apresentação.

## 10. Serviços e regras de negócio
A aplicação usa o EF Core como camada de acesso a dados e mantém regras de negócio no domínio e no contexto EF:
- validações de required fields
- unicidade por campos críticos
- restrições de estado
- controlo de eliminação de registos ligados
- contagens e agregações para o dashboard

Neste momento, não existe uma camada de serviços complexa separada; a lógica principal está nos controllers e no modelo de domínio.

## 11. Funcionamento do Dashboard Inteligente
O dashboard é gerado em `HomeController` e usa consultas ao `ApplicationDbContext` para calcular indicadores reais.

Inclui:
- total de clientes
- total de equipamentos
- técnicos ativos
- reparações em curso
- reparações concluídas
- reparações pendentes de peças
- atividade recente
- alertas

A lógica é calculada a partir do conteúdo real da base de dados e não recorre a dados fictícios.

## 12. Procedimentos de instalação
### Pré-requisitos
- .NET SDK 10 ou superior disponível no PATH
- VS Code ou outra IDE de desenvolvimento
- Git

### Instalação
```bash
git clone https://github.com/KevinSil/BYTEFIX.git
cd BYTEFIX
dotnet restore
```

## 13. Procedimentos de execução
### Execução local da aplicação
```bash
dotnet run
```

A aplicação inicia normalmente em http://localhost:5187, salvo outra porta disponível.

### Atualização da base de dados
```bash
dotnet ef database update
```

### Verificação do estado da aplicação
- confirmar que a página inicial responde
- confirmar que o dashboard carrega os indicadores
- confirmar que as listagens das entidades funcionam
- confirmar que a base de dados persiste entre execuções

## Observações finais
A implementação atual está funcional para a gestão operacional principal da oficina e para a base do dashboard. Não existe, nesta versão, um módulo de relatórios em PDF/Excel ou pesquisa avançada por texto, pois a funcionalidade implementada foi limitada ao CRUD e dashboard diretamente suportado pelo código atual.
