# Manual do Utilizador — ByteFix

## 1. Como iniciar a aplicação
1. Abra o terminal no projeto ByteFix.
2. Confirme que o .NET SDK está instalado.
3. Execute:

```bash
dotnet restore
dotnet run
```

4. A aplicação vai abrir no navegador em http://localhost:5187, ou numa porta disponível definida pelo ASP.NET Core.

## 2. Como utilizar o Dashboard Inteligente
O dashboard é a página inicial da aplicação.

Ele mostra:
- total de clientes
- total de equipamentos
- técnicos ativos
- reparações em curso
- reparações concluídas
- reparações em espera por peças
- distribuição por estados
- atividade recente
- alertas

Para interpretar os indicadores:
- valores maiores indicam maior volume de atividade
- as alertas avisam de peças pendentes ou entregas por concluir
- a atividade recente ajuda a ver o que aconteceu recentemente na oficina

## 3. Como interpretar os indicadores do dashboard
### Total de clientes
Número de clientes registados na base de dados.

### Total de equipamentos
Número de equipamentos associados aos clientes.

### Técnicos ativos
Número de técnicos marcados como ativos.

### Reparações em curso
Número de reparações que ainda estão em estado de trabalho ativo ou pendentes.

### Reparações concluídas
Número de reparações finaisizadas.

### Equipamentos à espera de peças
Número de reparações cujo estado é "A aguardar peças".

### Alertas
São gerados automaticamente para:
- reparações à espera de peças
- reparações concluídas ainda sem entrega registada

## 4. Como registar um cliente
1. No menu principal, aceda a "Clientes".
2. Clique em "Novo cliente".
3. Preencha os campos obrigatórios:
   - Nome
   - NIF
   - Email (se aplicável)
   - Telefone (opcional)
   - Morada (opcional)
   - Código postal (opcional)
   - Localidade (opcional)
4. Clique em "Guardar".

O sistema valida o NIF e evita duplicações.

## 5. Como registar um equipamento
1. Aceda a "Equipamentos".
2. Clique em "Novo equipamento".
3. Seleccione o cliente a que pertence o equipamento.
4. Preencha:
   - Tipo de equipamento
   - Marca
   - Modelo
   - Número de série (opcional)
   - Descrição (opcional)
5. Guarde o registo.

## 6. Como registar um técnico
1. Aceda a "Técnicos".
2. Clique em "Novo técnico".
3. Introduza:
   - Nome
   - Email
   - Telefone (opcional)
   - Especialidade (opcional)
   - Estado ativo/inativo
4. Guarde.

## 7. Como abrir uma ordem de reparação
1. Aceda a "Reparações".
2. Clique em "Nova reparação".
3. Seleccione o equipamento.
4. Indique a descrição da avaria.
5. Defina o estado inicial da reparação.
6. Guarde.

## 8. Como atribuir um técnico
1. Abra a reparação a editar.
2. Escolha o técnico na lista disponível.
3. Guarde as alterações.

## 9. Como registar uma intervenção
1. Aceda a "Intervenções".
2. Clique em "Nova intervenção".
3. Seleccione a reparação associada.
4. Descreva o trabalho executado.
5. Registe:
   - tempo de trabalho
   - custo de mão de obra
   - custo de peças
   - observações opcionais
6. Guarde.

## 10. Como consultar os custos
Os custos são vistos na gestão das intervenções e na própria reparação.

A lógica atual regista:
- custo de mão de obra
- custo de peças
- tempo de trabalho

Estas informações ficam associadas à intervenção e podem ser consultadas nas páginas de listagem e detalhe da entidade.

## 11. Como alterar o estado de uma reparação
1. Abra a reparação em edição.
2. Altere o campo Estado para o novo valor:
   - A aguardar diagnóstico
   - Em reparação
   - A aguardar peças
   - Concluída
   - Entregue ao cliente
3. Guarde.

## 12. Como consultar históricos
Os registos são listados por tipo de entidade:
- clientes
- equipamentos
- técnicos
- reparações
- intervenções

A navegação pelas páginas de listagem permite consultar o histórico de cada entidade e confirmar o que foi guardado na base de dados.

## 13. Como utilizar pesquisas e relatórios
Nesta versão da aplicação, a funcionalidade implementada centra-se no CRUD e no dashboard. Não existe, no código atual, um módulo separado de pesquisa avançada ou relatórios em PDF/Excel.

Para consultar dados, use as páginas de listagem e a página inicial do dashboard.

## Dicas práticas
- Antes de eliminar um equipamento ou outra entidade relacionada, confirme se existem reparações ou intervenções associadas.
- Use os estados corretamente para manter o dashboard fiel à realidade da oficina.
- Registe cada intervenção com precisão para manter o controlo financeiro e operacional.

## Resumo
ByteFix permite gerir uma oficina de reparação informática de forma simples e centralizada, mantendo os dados em SQLite e disponibilizando um dashboard com indicadores reais para acompanhamento operacional.
