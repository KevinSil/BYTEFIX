namespace ByteFix.ViewModels;

public sealed class DashboardViewModel
{
    public long? TotalClientes { get; init; }
    public long? TotalEquipamentos { get; init; }
    public long? TotalTecnicosAtivos { get; init; }
    public long? ReparacoesEmCurso { get; init; }
    public long? ReparacoesConcluidas { get; init; }
    public long? ReparacoesAguardarPecas { get; init; }

    public IReadOnlyList<DashboardChartPointViewModel> ReparacoesPorEstado { get; init; } = [];
    public IReadOnlyList<DashboardChartPointViewModel> ReparacoesMensais { get; init; } = [];
    public IReadOnlyList<DashboardCostPointViewModel> EvolucaoCustos { get; init; } = [];
    public IReadOnlyList<DashboardActivityViewModel> AtividadeRecente { get; init; } = [];
    public IReadOnlyList<DashboardAlertViewModel> Alertas { get; init; } = [];
}

public sealed record DashboardChartPointViewModel(string Rotulo, long Valor);

public sealed record DashboardCostPointViewModel(
    string Periodo,
    decimal CustoMaoObra,
    decimal CustoPecas);

public sealed record DashboardActivityViewModel(
    string Tipo,
    int RegistoId,
    string Descricao,
    DateTime Data);

public sealed record DashboardAlertViewModel(
    string Tipo,
    string Descricao,
    DateTime DataReferencia);
