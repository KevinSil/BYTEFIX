using System.ComponentModel.DataAnnotations;

namespace ByteFix.Models;

public sealed class Cliente
{
    public int Id { get; set; }

    [Required, StringLength(150)]
    public string Nome { get; set; } = string.Empty;

    [Required, RegularExpression(@"^\d{9}$"), StringLength(9, MinimumLength = 9)]
    public string NIF { get; set; } = string.Empty;

    [EmailAddress, StringLength(254)]
    public string? Email { get; set; }

    [StringLength(30)]
    public string? Telefone { get; set; }

    [StringLength(200)]
    public string? Morada { get; set; }

    [StringLength(16)]
    public string? CodigoPostal { get; set; }

    [StringLength(100)]
    public string? Localidade { get; set; }

    public DateTime DataRegisto { get; set; } = DateTime.UtcNow;

    public ICollection<Equipamento> Equipamentos { get; set; } = new List<Equipamento>();
}
