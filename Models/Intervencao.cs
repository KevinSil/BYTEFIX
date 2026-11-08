using System.ComponentModel.DataAnnotations;

namespace ByteFix.Models;

public sealed class Intervencao
{
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

    [Range(typeof(decimal), "0", "99999999.99")]
    public decimal CustoMaoObra { get; set; }

    [Range(typeof(decimal), "0", "99999999.99")]
    public decimal CustoPecas { get; set; }

    [StringLength(2000)]
    public string? Observacoes { get; set; }

    public Reparacao Reparacao { get; set; } = null!;
}
