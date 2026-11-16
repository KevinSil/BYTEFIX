namespace ByteFix.Models;

// Classe: centraliza os nomes dos estados permitidos para as reparações.
public static class ReparacaoEstado
{
    // Atributo: estado inicial de uma reparação.
    public const string AguardarDiagnostico = "A aguardar diagnóstico";
    public const string EmReparacao = "Em reparação";
    public const string AguardarPecas = "A aguardar peças";
    public const string Concluida = "Concluída";
    public const string EntregueAoCliente = "Entregue ao cliente";

    // Lista única usada pelos formulários e pela restrição CHECK da base de dados.
    public static readonly string[] Todos =
    [
        AguardarDiagnostico,
        EmReparacao,
        AguardarPecas,
        Concluida,
        EntregueAoCliente
    ];
}
