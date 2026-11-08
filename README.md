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

## ✨ Funcionalidades

- 👥 **Gestão de clientes** — Registo e consulta dos dados dos clientes.
- 💻 **Gestão de equipamentos** — Associação de equipamentos aos respetivos clientes.
- 🧑‍🔧 **Gestão de técnicos** — Registo de técnicos, especialidades e estado de atividade.
- 🛠️ **Gestão de reparações** — Acompanhamento das reparações e dos respetivos estados.
- 📋 **Gestão de intervenções** — Registo dos trabalhos realizados em cada reparação.
- 💰 **Controlo de custos** — Registo dos custos de mão de obra e peças utilizadas.
- 📊 **Dashboard Inteligente** — Visão geral da atividade através de indicadores e informação relevante.
- 🔎 **Pesquisa e consulta** — Facilita a localização e consulta dos registos.

## 📊 Dashboard Inteligente

O ByteFix inclui um Dashboard Inteligente concebido para apresentar, de forma clara, o estado geral da atividade da empresa.

Entre os indicadores previstos encontram-se:

- Número de reparações em curso;
- Reparações concluídas;
- Equipamentos à espera de peças;
- Atividade recente.

O objetivo é permitir uma consulta rápida da situação da oficina e apoiar a organização do trabalho.

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

Estrutura principal prevista:

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
├── Documentation/     # Documentação técnica do projeto
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

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Visual Studio Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)

### 2. Clonar o repositório

```bash
git clone https://github.com/TEU-UTILIZADOR/ByteFix.git
```

Entra na pasta do projeto:

```bash
cd ByteFix
```

### 3. Restaurar dependências

```bash
dotnet restore
```

### 4. Instalar a ferramenta de migrações

Caso ainda não tenhas o Entity Framework Core CLI instalado:

```bash
dotnet tool install --global dotnet-ef
```

Se já estiver instalado, podes atualizá-lo:

```bash
dotnet tool update --global dotnet-ef
```

### 5. Criar ou atualizar a base de dados

```bash
dotnet ef database update
```

Este comando aplica as migrações existentes e atualiza a base de dados SQLite.

### 6. Executar a aplicação

```bash
dotnet run
```

Depois, abre no navegador o endereço apresentado no terminal.

> Nota: Os comandos devem ser executados na pasta que contém o ficheiro `.csproj`. Se o projeto ainda não tiver migrações criadas, será necessário criá-las antes de executar a atualização da base de dados.

## 📚 Documentação

A documentação do projeto encontra-se na pasta `Documentation/` e pode incluir:

- Requisitos funcionais e técnicos;
- Diagrama Entidade-Relação (ER);
- Modelo relacional;
- Dicionário de dados;
- Documentação técnica;
- Manual de utilização.

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

