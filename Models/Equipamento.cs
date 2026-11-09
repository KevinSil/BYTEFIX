using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ByteFix.Models;

public sealed class Equipamento
{
    public int Id { get; set; }

    [Required]
    public int ClienteId { get; set; }

    [Required, StringLength(80)]
    public string TipoEquipamento { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string Marca { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string Modelo { get; set; } = string.Empty;

    [StringLength(120)]
    public string? NumeroSerie { get; set; }

    [StringLength(1000)]
    public string? Descricao { get; set; }

    public DateTime DataRegisto { get; set; } = DateTime.UtcNow;

    [ValidateNever]
    public Cliente Cliente { get; set; } = null!;

    public ICollection<Reparacao> Reparacoes { get; set; } = new List<Reparacao>();
}
