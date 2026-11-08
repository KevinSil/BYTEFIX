# Modelo relacional

Os nomes das tabelas e colunas correspondem ao mapeamento convencional do EF Core para os modelos.

## `Clientes`

- **PK:** `Id` (`INTEGER`, gerada pela base de dados).
- `Nome`, `NIF`, `Email`, `Telefone`, `Morada`, `CodigoPostal`, `Localidade`, `DataRegisto`.
- Índice único: `NIF`.

## `Equipamentos`

- **PK:** `Id`.
- **FK:** `ClienteId` → `Clientes.Id` (`NOT NULL`, `RESTRICT`).
- `TipoEquipamento`, `Marca`, `Modelo`, `NumeroSerie`, `Descricao`, `DataRegisto`.
- Índice único filtrado: `NumeroSerie` quando não é `NULL`.

## `Tecnicos`

- **PK:** `Id`.
- `Nome`, `Email`, `Telefone`, `Especialidade`, `Ativo`.
- Índice único: `Email`.

## `Reparacoes`

- **PK:** `Id`.
- **FK:** `EquipamentoId` → `Equipamentos.Id` (`NOT NULL`, `RESTRICT`).
- **FK:** `TecnicoId` → `Tecnicos.Id` (`NULL`, `RESTRICT`).
- `DataEntrada`, `DescricaoAvaria`, `Diagnostico`, `Estado`, `DataConclusao`, `DataEntrega`, `Observacoes`.
- `CK_Reparacoes_Estado` restringe `Estado` aos cinco valores definidos no modelo.

## `Intervencoes`

- **PK:** `Id`.
- **FK:** `ReparacaoId` → `Reparacoes.Id` (`NOT NULL`, `RESTRICT`).
- `DataIntervencao`, `DescricaoTrabalho`, `PecasUtilizadas`, `TempoTrabalho`, `CustoMaoObra`, `CustoPecas`, `Observacoes`.
- Duração e custos são armazenados como `INTEGER`, em centésimas de hora e de euro, respetivamente.
- Restrições `CK_Intervencoes_*` impedem duração e custos negativos.

Todas as chaves estrangeiras são criadas com integridade referencial SQLite ativa e sem eliminação em cascata.
