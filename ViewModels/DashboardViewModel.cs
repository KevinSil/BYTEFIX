namespace ByteFix.ViewModels;

// Classe: transporta os indicadores, gráficos, atividades e alertas do painel.
public sealed class DashboardViewModel
{
    // Atributo: exemplo de indicador apresentado no painel.
    public long? TotalClientes { get; init; }
    public long? TotalEquipamentos { get; init; }
    public long? TotalTecnicosAtivos { get; init; }
    public long? ReparacoesFinalizadas { get; init; }
    public long? ReparacoesPorConcluir { get; init; }
    public long? ReparacoesEmCurso { get; init; }
    public long? ReparacoesConcluidas { get; init; }
    public long? ReparacoesAguardarPecas { get; init; }

    public IReadOnlyList<DashboardChartPointViewModel> ReparacoesPorEstado { get; init; } = [];
    public IReadOnlyList<DashboardChartPointViewModel> ReparacoesMensais { get; init; } = [];
    public IReadOnlyList<DashboardCostPointViewModel> EvolucaoCustos { get; init; } = [];
    public IReadOnlyList<DashboardActivityViewModel> AtividadeRecente { get; init; } = [];
    public IReadOnlyList<DashboardAlertViewModel> Alertas { get; init; } = [];
}

// Classe (record): representa um valor individual apresentado num gráfico.
public sealed record DashboardChartPointViewModel(string Rotulo, long Valor);

// Classe (record): transporta os custos mensais de mão de obra e de peças.
public sealed record DashboardCostPointViewModel(
    string Periodo,
    decimal CustoMaoObra,
    decimal CustoPecas);

// Classe (record): resume uma atividade recente apresentada no painel.
public sealed record DashboardActivityViewModel(
    string Tipo,
    int RegistoId,
    string Descricao,
    DateTime Data);

// Classe (record): resume um alerta que requer atenção da oficina.
public sealed record DashboardAlertViewModel(
    string Tipo,
    string Descricao,
    DateTime DataReferencia);
