using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ByteFix.Models;

// Classe: reúne os dados, estado e relações de uma reparação.
public sealed class Reparacao
{
    // Atributo: identificador único da reparação.
    public int Id { get; set; }

    [Required]
    public int EquipamentoId { get; set; }

    public int? TecnicoId { get; set; }

    public DateTime DataEntrada { get; set; } = DateTime.UtcNow;

    [Required, StringLength(2000)]
    public string DescricaoAvaria { get; set; } = string.Empty;

    [StringLength(2000)]
    public string? Diagnostico { get; set; }

    [Required, StringLength(40)]
    public string Estado { get; set; } = ReparacaoEstado.AguardarDiagnostico;

    public DateTime? DataConclusao { get; set; }

    public DateTime? DataEntrega { get; set; }

    [StringLength(2000)]
    public string? Observacoes { get; set; }

    [ValidateNever]
    public Equipamento Equipamento { get; set; } = null!;

    [ValidateNever]
    public Tecnico? Tecnico { get; set; }

    [ValidateNever]
    public ICollection<Intervencao> Intervencoes { get; set; } = new List<Intervencao>();
}
