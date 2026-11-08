# Dicionário de dados

`NOT NULL` representa obrigatoriedade na base de dados. Campos indicados como opcionais admitem `NULL`. As chaves primárias são inteiros autogerados.

## Cliente (`Clientes`)

| Campo | Tipo SQLite | Obrigatório | Chave / validação | Descrição |
|---|---|---:|---|---|
| `Id` | INTEGER | Sim | PK | Identificador do cliente. |
| `Nome` | TEXT | Sim | Máx. 150 caracteres | Nome do cliente. |
| `NIF` | TEXT | Sim | UK; nove algarismos | Identificação fiscal. |
| `Email` | TEXT | Não | Email, máx. 254 caracteres | Email de contacto. |
| `Telefone` | TEXT | Não | Máx. 30 caracteres | Telefone de contacto. |
| `Morada` | TEXT | Não | Máx. 200 caracteres | Morada. |
| `CodigoPostal` | TEXT | Não | Máx. 16 caracteres | Código postal. |
| `Localidade` | TEXT | Não | Máx. 100 caracteres | Localidade. |
| `DataRegisto` | TEXT | Sim | Data/hora UTC | Data de registo atribuída pela aplicação. |

## Equipamento (`Equipamentos`)

| Campo | Tipo SQLite | Obrigatório | Chave / validação | Descrição |
|---|---|---:|---|---|
| `Id` | INTEGER | Sim | PK | Identificador do equipamento. |
| `ClienteId` | INTEGER | Sim | FK → `Clientes.Id`, RESTRICT | Proprietário. |
| `TipoEquipamento` | TEXT | Sim | Máx. 80 caracteres | Tipo de equipamento. |
| `Marca` | TEXT | Sim | Máx. 100 caracteres | Marca. |
| `Modelo` | TEXT | Sim | Máx. 100 caracteres | Modelo. |
| `NumeroSerie` | TEXT | Não | UK se indicado; máx. 120 caracteres | Número de série. |
| `Descricao` | TEXT | Não | Máx. 1000 caracteres | Descrição adicional. |
| `DataRegisto` | TEXT | Sim | Data/hora UTC | Data de registo atribuída pela aplicação. |

## Técnico (`Tecnicos`)

| Campo | Tipo SQLite | Obrigatório | Chave / validação | Descrição |
|---|---|---:|---|---|
| `Id` | INTEGER | Sim | PK | Identificador do técnico. |
| `Nome` | TEXT | Sim | Máx. 150 caracteres | Nome do técnico. |
| `Email` | TEXT | Sim | UK; formato email; máx. 254 caracteres | Email do técnico. |
| `Telefone` | TEXT | Não | Máx. 30 caracteres | Telefone. |
| `Especialidade` | TEXT | Não | Máx. 120 caracteres | Área de especialidade. |
| `Ativo` | INTEGER | Sim | Booleano SQLite (0/1) | Indica se o técnico está ativo; predefinição `true` no modelo. |

## Reparação (`Reparacoes`)

| Campo | Tipo SQLite | Obrigatório | Chave / validação | Descrição |
|---|---|---:|---|---|
| `Id` | INTEGER | Sim | PK | Identificador da reparação. |
| `EquipamentoId` | INTEGER | Sim | FK → `Equipamentos.Id`, RESTRICT | Equipamento recebido. |
| `TecnicoId` | INTEGER | Não | FK → `Tecnicos.Id`, RESTRICT | Técnico atribuído, se existente. |
| `DataEntrada` | TEXT | Sim | Data/hora UTC | Data de receção; inicializada pela aplicação. |
| `DescricaoAvaria` | TEXT | Sim | Máx. 2000 caracteres | Avaria reportada. |
| `Diagnostico` | TEXT | Não | Máx. 2000 caracteres | Diagnóstico técnico. |
| `Estado` | TEXT | Sim | CHECK; máx. 40 caracteres | Estado atual da reparação. |
| `DataConclusao` | TEXT | Não | Data/hora | Data de conclusão. |
| `DataEntrega` | TEXT | Não | Data/hora | Data de entrega ao cliente. |
| `Observacoes` | TEXT | Não | Máx. 2000 caracteres | Observações. |

Estados permitidos: `A aguardar diagnóstico`, `Em reparação`, `A aguardar peças`, `Concluída`, `Entregue ao cliente`.

## Intervenção (`Intervencoes`)

| Campo | Tipo SQLite | Obrigatório | Chave / validação | Descrição |
|---|---|---:|---|---|
| `Id` | INTEGER | Sim | PK | Identificador da intervenção. |
| `ReparacaoId` | INTEGER | Sim | FK → `Reparacoes.Id`, RESTRICT | Reparação associada. |
| `DataIntervencao` | TEXT | Sim | Data/hora UTC | Data da intervenção; inicializada pela aplicação. |
| `DescricaoTrabalho` | TEXT | Sim | Máx. 2000 caracteres | Trabalho realizado. |
| `PecasUtilizadas` | TEXT | Não | Máx. 2000 caracteres | Peças utilizadas. |
| `TempoTrabalho` | INTEGER | Sim | Não negativo | Duração em centésimas de hora; por exemplo, `125` representa `1,25 h`. |
| `CustoMaoObra` | INTEGER | Sim | Não negativo | Custo em cêntimos de euro; por exemplo, `1250` representa `12,50 EUR`. |
| `CustoPecas` | INTEGER | Sim | Não negativo | Custo das peças em cêntimos de euro. |
| `Observacoes` | TEXT | Não | Máx. 2000 caracteres | Observações. |
