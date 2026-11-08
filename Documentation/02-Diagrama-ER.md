# Diagrama Entidade-Relação

```mermaid
erDiagram
    Cliente ||--o{ Equipamento : possui
    Equipamento ||--o{ Reparacao : recebe
    Tecnico o|--o{ Reparacao : executa
    Reparacao ||--o{ Intervencao : inclui

    Cliente {
        int Id PK
        string Nome
        string NIF UK
        string Email
        string Telefone
        string Morada
        string CodigoPostal
        string Localidade
        datetime DataRegisto
    }
    Equipamento {
        int Id PK
        int ClienteId FK
        string TipoEquipamento
        string Marca
        string Modelo
        string NumeroSerie UK
        string Descricao
        datetime DataRegisto
    }
    Tecnico {
        int Id PK
        string Nome
        string Email UK
        string Telefone
        string Especialidade
        boolean Ativo
    }
    Reparacao {
        int Id PK
        int EquipamentoId FK
        int TecnicoId FK
        datetime DataEntrada
        string DescricaoAvaria
        string Diagnostico
        string Estado
        datetime DataConclusao
        datetime DataEntrega
        string Observacoes
    }
    Intervencao {
        int Id PK
        int ReparacaoId FK
        datetime DataIntervencao
        string DescricaoTrabalho
        string PecasUtilizadas
        integer TempoTrabalho
        integer CustoMaoObra
        integer CustoPecas
        string Observacoes
    }
```

## Cardinalidades e notas

- Um cliente pode ter zero ou vários equipamentos; cada equipamento tem exatamente um cliente.
- Um equipamento pode ter zero ou várias reparações; cada reparação tem exatamente um equipamento.
- Um técnico pode estar associado a zero ou várias reparações; uma reparação pode não ter técnico ou ter um técnico.
- Uma reparação pode ter zero ou várias intervenções; cada intervenção pertence a exatamente uma reparação.
- Os valores inteiros de duração e custo são centésimas da unidade indicada no modelo.
- As relações têm eliminação restrita para proteger os registos históricos.
