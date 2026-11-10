using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ByteFix.Models;

// Classe: regista o trabalho, tempo e custos de uma intervenção técnica.
public sealed class Intervencao
{
    // Atributo: identificador único da intervenção.
    public int Id { get; set; }

    [Required]
    public int ReparacaoId { get; set; }

    public DateTime DataIntervencao { get; set; } = DateTime.UtcNow;

    [Required, StringLength(2000)]
    public string DescricaoTrabalho { get; set; } = string.Empty;

    [StringLength(2000)]
    public string? PecasUtilizadas { get; set; }

    [Range(typeof(decimal), "0", "1000000")]
    public decimal TempoTrabalho { get; set; }

    [Range(0d, 99999999.99d)]
    public decimal CustoMaoObra { get; set; }

    [Range(0d, 99999999.99d)]
    public decimal CustoPecas { get; set; }

    [StringLength(2000)]
    public string? Observacoes { get; set; }

    [ValidateNever]
    public Reparacao Reparacao { get; set; } = null!;
}
