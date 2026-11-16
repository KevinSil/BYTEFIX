using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ByteFix.Models;

// Classe: regista um equipamento e liga-o ao respetivo cliente e reparações.
public sealed class Equipamento
{
    // Atributo: identificador único do equipamento.
    public int Id { get; set; }

    [Required]
    // Chave estrangeira do cliente proprietário do equipamento.
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

    // Propriedade carregada pelo EF Core; não é submetida diretamente pelo formulário.
    [ValidateNever]
    public Cliente Cliente { get; set; } = null!;

    // Histórico das reparações realizadas neste equipamento.
    public ICollection<Reparacao> Reparacoes { get; set; } = new List<Reparacao>();
}
