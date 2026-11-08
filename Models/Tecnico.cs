using System.ComponentModel.DataAnnotations;

namespace ByteFix.Models;

public sealed class Tecnico
{
    public int Id { get; set; }

    [Required, StringLength(150)]
    public string Nome { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(254)]
    public string Email { get; set; } = string.Empty;

    [StringLength(30)]
    public string? Telefone { get; set; }

    [StringLength(120)]
    public string? Especialidade { get; set; }

    public bool Ativo { get; set; } = true;

    public ICollection<Reparacao> Reparacoes { get; set; } = new List<Reparacao>();
}
