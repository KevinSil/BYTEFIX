# Requisitos — ByteFix

## Objetivos

- Centralizar a informação de clientes, equipamentos, técnicos, reparações e intervenções.
- Permitir acompanhar uma reparação desde a receção do equipamento até à entrega.
- Preservar o histórico operacional e os custos registados.
- Preparar uma interface responsiva e um dashboard cujos indicadores futuros se baseiem apenas em dados persistidos.

## Atores

- **Utilizador da oficina:** na fase de gestão, consulta e mantém os registos operacionais.
- **Técnico:** executa reparações e regista intervenções; uma reparação pode ainda não ter técnico atribuído.
- **Responsável da oficina:** acompanha estados, custos, atividade e alertas.

Os atores identificam os utilizadores previstos. Autenticação, autorização e funcionalidades de gestão ainda não estão implementadas.

## Requisitos funcionais

1. Manter clientes e associar-lhes equipamentos.
2. Manter técnicos, incluindo a sua especialidade e estado ativo.
3. Registar uma reparação para um equipamento, com técnico opcional, datas, avaria, diagnóstico, estado e observações.
4. Registar uma ou mais intervenções numa reparação, incluindo trabalho, peças, duração e custos.
5. Restringir os estados da reparação a: A aguardar diagnóstico, Em reparação, A aguardar peças, Concluída e Entregue ao cliente.
6. Consultar futuramente indicadores, gráficos, atividade recente e alertas do dashboard a partir da base de dados.

## Requisitos técnicos

- C# e ASP.NET Core MVC sobre .NET 10.
- Entity Framework Core 10 e provider SQLite.
- SQLite como única base de dados, no ficheiro `ByteFix.db`.
- Bootstrap 5, Tabler UI, HTML5, CSS3 e JavaScript no layout web.
- Migrações EF Core para versionar o esquema.
- Campos monetários e duração persistidos em centésimas inteiras na SQLite para evitar imprecisão de ponto flutuante.

## Requisitos não funcionais

- Interface responsiva, consistente, acessível e em português europeu.
- Integridade referencial e preservação do histórico.
- Validação de formato e comprimento nos campos adequados.
- Os indicadores não devem apresentar valores demonstrativos ou dados fictícios.
- A configuração da ligação à base de dados deve ficar separada do código.

## Regras de negócio

- Cada equipamento pertence a exatamente um cliente.
- Cada reparação pertence a exatamente um equipamento; a atribuição de técnico é opcional.
- Cada intervenção pertence a exatamente uma reparação.
- A remoção de clientes, equipamentos, técnicos ou reparações referenciados é restringida para preservar o histórico.
- O NIF do cliente e o email do técnico são únicos.
- O número de série, quando indicado, é único.
- O estado de uma reparação tem de pertencer à lista dos cinco estados definida.
- Duração e custos não podem ser negativos. A duração é expressa em horas com resolução de 0,01 h; custos em euros com resolução de 0,01 EUR.
- Datas de registo e de intervenção são inicializadas em UTC pela aplicação. Datas de conclusão e entrega são opcionais.
- Regras de transição de estados, limites temporais dos alertas e outras regras de negócio operacionais ficam para a Fase 2.
