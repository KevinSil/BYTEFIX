# Dashboard inteligente

## Organização

O dashboard previsto tem uma barra superior com pesquisa (reservada à Fase 2), navegação lateral, cartões de indicadores, painéis de gráficos, atividade recente e alertas. A estrutura visual é responsiva e usa Bootstrap 5 e Tabler UI.

Nesta fase os indicadores e gráficos não são calculados: os cartões mostram um marcador neutro, sem números fictícios. Os ViewModels definem os tipos de dados de destino e as coleções começam vazias. O dashboard não é preenchido com dados de demonstração.

## Indicadores

| ViewModel | Origem futura | Regra de cálculo a implementar |
|---|---|---|
| `TotalClientes` | `Clientes` | Contagem de clientes. |
| `TotalEquipamentos` | `Equipamentos` | Contagem de equipamentos registados. |
| `TotalTecnicosAtivos` | `Tecnicos` | Contagem de técnicos com `Ativo = true`. |
| `ReparacoesEmCurso` | `Reparacoes` | Contagem de reparações em execução, a definir incluindo os estados em curso. |
| `ReparacoesConcluidas` | `Reparacoes` | Contagem no estado `Concluída`. |
| `ReparacoesAguardarPecas` | `Reparacoes` | Contagem no estado `A aguardar peças`. |

As contagens são anuláveis para distinguir um indicador ainda não calculado de um resultado real igual a zero.

## Gráficos

- **Reparações por estado:** `ReparacoesPorEstado`, pontos com rótulo e contagem a agrupar pelo estado persistido.
- **Reparações mensais:** `ReparacoesMensais`, pontos com período e contagem pela data de entrada.
- **Evolução dos custos:** `EvolucaoCustos`, pontos por período, agregando custos de mão de obra e peças das intervenções. Os custos são `decimal` em euros no modelo e persistidos como cêntimos inteiros.

## Atividade recente

`AtividadeRecente` foi preparado para apresentar reparações registadas, intervenções e equipamentos recebidos, ordenados por data descendente. A seleção e o limite serão definidos na implementação futura.

## Alertas

`Alertas` está preparado para:

- Reparações que excedam um limite de tempo sem atualização.
- Reparações no estado `A aguardar peças`.
- Reparações no estado `Concluída` sem data de entrega.

Os limites de tempo e condições exatas devem ser confirmados/definidos antes de implementar os cálculos. Nesta fase nenhum alerta é simulado.

## Origem dos dados

Todos os valores futuros deverão ser consultados na base SQLite `ByteFix.db` através do `ApplicationDbContext`. Não usar listas em memória nem dados de demonstração como substituição da persistência.
