# 🔧 ByteFix — Sistema de Gestão de Reparações Informáticas

<p align="center">
  <strong>Smart Repair Management</strong><br>
  Uma solução para simplificar e organizar a gestão de reparações informáticas.
</p>

---

## 📌 Sobre o projeto

O **ByteFix** é uma aplicação web desenvolvida para facilitar a gestão de uma empresa de assistência técnica e reparação de equipamentos informáticos.

O sistema permite centralizar a informação dos clientes, equipamentos, técnicos, reparações e intervenções, tornando o acompanhamento dos serviços mais simples, rápido e eficiente.

O projeto foi desenvolvido com o objetivo de melhorar a organização do trabalho diário de uma oficina informática, reduzir tarefas manuais e facilitar o controlo de todas as etapas de uma reparação.

## ✨ Funcionalidades previstas

- 👥 **Clientes e equipamentos** — Registo de clientes e dos respetivos equipamentos.
- 🧑‍🔧 **Técnicos** — Especialidades, estado de atividade e atribuição a reparações.
- 🛠️ **Reparações e intervenções** — Acompanhamento das reparações e trabalhos realizados.
- 💰 **Custos** — Registo dos custos de mão de obra e peças.
- 📊 **Dashboard Inteligente** — Indicadores, gráficos, atividade recente e alertas baseados em dados reais.

Nesta Fase 1 estão implementados os modelos, a persistência, a identidade visual e a estrutura do dashboard. As operações de gestão e os cálculos serão desenvolvidos na Fase 2.

## 📊 Dashboard Inteligente previsto

O dashboard foi estruturado para apresentar, quando os cálculos forem implementados:

- Indicadores de clientes, equipamentos, técnicos e reparações;
- Distribuição e evolução mensal das reparações;
- Evolução dos custos de mão de obra e peças;
- Atividade recente e alertas.

Nenhum valor demonstrativo é apresentado nesta fase.

## 🔄 Estados das reparações

Cada reparação pode assumir um dos seguintes estados:

| Estado | Descrição |
|---|---|
| A aguardar diagnóstico | O equipamento aguarda avaliação técnica. |
| Em reparação | O equipamento encontra-se em intervenção. |
| A aguardar peças | A reparação depende da chegada de peças. |
| Concluída | O trabalho de reparação está terminado. |
| Entregue ao cliente | O equipamento já foi entregue. |

## 🧰 Tecnologias utilizadas

| Tecnologia | Utilização |
|---|---|
| C# | Linguagem de programação |
| ASP.NET Core MVC | Desenvolvimento da aplicação web |
| Entity Framework Core | Acesso e gestão dos dados |
| SQLite | Base de dados relacional |
| Bootstrap 5 | Interface responsiva |
| Tabler UI | Componentes e identidade visual |
| HTML e CSS | Estrutura e apresentação |

## 🏗️ Arquitetura do projeto

A aplicação segue o padrão arquitetural **MVC (Model-View-Controller)**, separando os dados, a interface e a lógica de controlo.

Estrutura da Fase 1:

```text
ByteFix/
│
├── Controllers/       # Controladores da aplicação
├── Models/            # Modelos e entidades
├── ViewModels/        # Modelos de apresentação
├── Data/              # Contexto e configuração da base de dados
├── Services/          # Serviços e regras de negócio
├── Views/             # Páginas e interfaces
├── wwwroot/           # CSS, JavaScript e imagens
├── Migrations/        # Migrações do Entity Framework Core
├── Documentation/     # Requisitos, diagrama ER, modelo e dicionário de dados
│
├── appsettings.json   # Configurações da aplicação
└── Program.cs         # Ponto de entrada da aplicação
```

## 🗄️ Base de dados

O projeto utiliza **SQLite** como sistema de gestão de base de dados, através do Entity Framework Core.

O modelo de dados é constituído por cinco entidades principais:

| Entidade | Descrição |
|---|---|
| Cliente | Guarda os dados dos clientes. |
| Equipamento | Guarda os equipamentos associados aos clientes. |
| Tecnico | Guarda os dados dos técnicos. |
| Reparacao | Regista as reparações dos equipamentos. |
| Intervencao | Regista os trabalhos realizados em cada reparação. |

### Relações entre entidades

- Um cliente pode ter vários equipamentos.
- Um equipamento pode ter várias reparações.
- Um técnico pode estar associado a várias reparações.
- Uma reparação pode conter várias intervenções.

## ⚙️ Instalação e execução

### 1. Pré-requisitos

Antes de iniciar, certifica-te de que tens instalado:

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Visual Studio Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)

### 2. Restaurar dependências e criar a base de dados

```bash
dotnet restore
dotnet tool install --global dotnet-ef --version 10.0.14
dotnet ef database update
```

O projeto usa o provider SQLite e cria o ficheiro `ByteFix.db` na pasta da aplicação. Para criar uma migração após alterar o modelo:

```bash
dotnet ef migrations add NomeDaMigracao
```

As migrações iniciais estão versionadas em `Migrations/`.

### 3. Executar a aplicação

```bash
dotnet run
```


## 📚 Documentação

Os documentos de análise e conceção encontram-se em `Documentation/`:

- `01-Requisitos.md`
- `02-Diagrama-ER.md`
- `03-Modelo-Relacional.md`
- `04-Dicionario-Dados.md`
- `05-Dashboard.md`

## 🎯 Objetivos do projeto

O desenvolvimento do ByteFix pretende:

- Digitalizar a gestão de uma oficina de reparação informática;
- Centralizar a informação num único sistema;
- Facilitar o acompanhamento das reparações;
- Melhorar a organização dos técnicos e intervenções;
- Permitir um melhor controlo dos custos;
- Disponibilizar informação útil através do Dashboard Inteligente.

## 👨‍💻 Contexto académico

Este projeto foi desenvolvido no âmbito da disciplina de **Desenvolvimento de Aplicações Informáticas Empresariais**, aplicando conhecimentos de programação em C#, desenvolvimento web com ASP.NET Core MVC, bases de dados relacionais e arquitetura de software.

---

<p align="center">
  <strong>ByteFix</strong><br>
  <em>Smart Repair Management</em><br><br>
  🔧 Organizar. Reparar. Simplificar.
</p>
