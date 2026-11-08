namespace ByteFix.Models;

public static class ReparacaoEstado
{
    public const string AguardarDiagnostico = "A aguardar diagnóstico";
    public const string EmReparacao = "Em reparação";
    public const string AguardarPecas = "A aguardar peças";
    public const string Concluida = "Concluída";
    public const string EntregueAoCliente = "Entregue ao cliente";

    public static readonly string[] Todos =
    [
        AguardarDiagnostico,
        EmReparacao,
        AguardarPecas,
        Concluida,
        EntregueAoCliente
    ];
}
