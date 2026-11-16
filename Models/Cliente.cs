using System.ComponentModel.DataAnnotations;

namespace ByteFix.Models;

// Classe: guarda os dados de contacto e os equipamentos associados a um cliente.
public sealed class Cliente
{
    // Atributo: identificador único do cliente.
    public int Id { get; set; }

    [Required, StringLength(150)]
    public string Nome { get; set; } = string.Empty;

    [Required, RegularExpression(@"^\d{9}$"), StringLength(9, MinimumLength = 9)]
    public string NIF { get; set; } = string.Empty;

    // Os restantes dados de contacto são opcionais, mas limitados para validar a entrada.
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

    // Relação um-para-muitos: um cliente pode ter vários equipamentos.
    public ICollection<Equipamento> Equipamentos { get; set; } = new List<Equipamento>();
}
