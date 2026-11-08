# ByteFix

## Identificação do projeto
ByteFix é uma aplicação web para gestão de reparações informáticas, desenvolvida em ASP.NET Core MVC com SQLite e Entity Framework Core.

## Descrição da aplicação
A aplicação centraliza o registo de clientes, equipamentos, técnicos, reparações e intervenções, permitindo acompanhar o estado operacional de uma oficina de assistência técnica e consultar um dashboard com indicadores reais da base de dados.

## Objetivos do projeto
- Organizar a informação de clientes e equipamentos.
- Controlar reparações e técnicos associados.
- Registrar intervenções e custos de mão de obra e peças.
- Disponibilizar um dashboard com indicadores de atividade.
- Manter uma base de dados persistente em SQLite.

## Tecnologias utilizadas
- C#
- ASP.NET Core MVC
- Entity Framework Core
- SQLite
- Bootstrap 5
- Tabler UI

## Funcionalidades disponíveis
- Dashboard Inteligente com contagens e atividade recente.
- Gestão de clientes.
- Gestão de equipamentos associados a clientes.
- Gestão de técnicos.
- Gestão de reparações com estados definidos.
- Gestão de intervenções com custos e tempo de trabalho.
- Regras de integridade referencial no modelo.

## Dashboard Inteligente
O dashboard apresenta indicadores reais calculados a partir dos dados guardados na base de dados, como:
- número de clientes
- número de equipamentos
- técnicos ativos
- reparações em curso
- reparações concluídas
- reparações pendentes de peças
- distribuição por estado
- atividade recente
- alertas de peças pendentes ou entregas por concluir

Os valores não são simulação: são gerados a partir das entidades persistidas no SQLite.

## Requisitos de instalação
- .NET SDK 10.0.403 ou compatível
- Visual Studio Code (opcional, mas recomendado)
- Git

## Instruções de execução no VS Code
1. Abrir a pasta do projeto no VS Code.
2. Abrir o terminal integrado.
3. Executar:

```bash
dotnet restore
```

4. Iniciar a aplicação:

```bash
dotnet run
```

5. A aplicação ficará disponível em http://localhost:5187 (ou outra porta atribuída pelo ASP.NET Core se já estiver em uso).

## Comandos .NET utilizados
```bash
dotnet restore
dotnet build
dotnet test
```

## Localização da base de dados SQLite
O ficheiro da base de dados encontra-se em:

```text
ByteFix.db
```

A string de ligação está em `appsettings.json` e usa SQLite.

## Procedimentos de criação ou atualização da base de dados
A base de dados é criada automaticamente quando a aplicação corre pela primeira vez e a estrutura é gerida com migrações do EF Core.

Para atualizar a base de dados manualmente:

```bash
dotnet ef database update
```

Para criar uma nova migração:

```bash
dotnet ef migrations add NomeDaMigracao
```

## Estrutura de pastas
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

## Relações principais entre as entidades
- Cliente -> Equipamento: um cliente pode ter vários equipamentos.
- Equipamento -> Reparacao: um equipamento pode ter várias reparações.
- Tecnico -> Reparacao: um técnico pode estar associado a várias reparações.
- Reparacao -> Intervencao: uma reparação pode ter várias intervenções.

## Documentação disponível
- `Documentation/01-Requisitos.md`
- `Documentation/02-Diagrama-ER.md`
- `Documentation/03-Modelo-Relacional.md`
- `Documentation/04-Dicionario-Dados.md`
- `Documentation/05-Dashboard.md`
- `Documentation/05-Documentacao-Tecnica.md`
- `Documentation/06-Manual-Utilizador.md`

## Observações
A versão atual implementa o CRUD funcional para clientes, equipamentos, técnicos, reparações e intervenções, bem como o dashboard com indicadores reais. Não existe, nesta implementação, um módulo autónomo de pesquisa avançada ou relatórios em PDF/Excel.
