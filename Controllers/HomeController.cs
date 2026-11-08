using ByteFix.Data;
using ByteFix.Models;
using ByteFix.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ByteFix.Controllers;

public sealed class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var totalClientes = await _context.Clientes.CountAsync();
        var totalEquipamentos = await _context.Equipamentos.CountAsync();
        var totalTecnicosAtivos = await _context.Tecnicos.CountAsync(t => t.Ativo);
        var reparacoesEmCurso = await _context.Reparacoes.CountAsync(r =>
            r.Estado == ReparacaoEstado.AguardarDiagnostico ||
            r.Estado == ReparacaoEstado.EmReparacao ||
            r.Estado == ReparacaoEstado.AguardarPecas);
        var reparacoesConcluidas = await _context.Reparacoes.CountAsync(r => r.Estado == ReparacaoEstado.Concluida);
        var reparacoesAguardarPecas = await _context.Reparacoes.CountAsync(r => r.Estado == ReparacaoEstado.AguardarPecas);

        var reparacoesPorEstado = await _context.Reparacoes
            .GroupBy(r => r.Estado)
            .Select(g => new { Rotulo = g.Key, Valor = g.Count() })
            .OrderBy(x => x.Rotulo)
            .ToListAsync();

        var reparacoesMensais = await _context.Reparacoes
            .GroupBy(r => new { r.DataEntrada.Year, r.DataEntrada.Month })
            .Select(g => new
            {
                Ano = g.Key.Year,
                Mes = g.Key.Month,
                Valor = g.Count()
            })
            .OrderBy(x => x.Ano)
            .ThenBy(x => x.Mes)
            .ToListAsync();

        var evolucaoCustos = await _context.Intervencoes
            .GroupBy(i => new { i.DataIntervencao.Year, i.DataIntervencao.Month })
            .Select(g => new
            {
                Ano = g.Key.Year,
                Mes = g.Key.Month,
                CustoMaoObra = g.Sum(i => i.CustoMaoObra) / 100m,
                CustoPecas = g.Sum(i => i.CustoPecas) / 100m
            })
            .OrderBy(x => x.Ano)
            .ThenBy(x => x.Mes)
            .ToListAsync();

        var atividadeRecente = await _context.Reparacoes
            .Include(r => r.Equipamento)
            .OrderByDescending(r => r.DataEntrada)
            .Take(5)
            .Select(r => new
            {
                Tipo = "Reparação",
                RegistoId = r.Id,
                Descricao = r.Equipamento.Marca + " " + r.Equipamento.Modelo + " — " + r.Estado,
                Data = r.DataEntrada
            })
            .ToListAsync();

        var alertas = await _context.Reparacoes
            .Include(r => r.Equipamento)
            .Where(r => r.Estado == ReparacaoEstado.AguardarPecas || (r.Estado == ReparacaoEstado.Concluida && r.DataEntrega == null))
            .OrderByDescending(r => r.DataEntrada)
            .Take(10)
            .Select(r => new
            {
                Tipo = r.Estado == ReparacaoEstado.AguardarPecas ? "Peças pendentes" : "Entrega pendente",
                Descricao = r.Equipamento.Marca + " " + r.Equipamento.Modelo + " — " + r.DescricaoAvaria,
                DataReferencia = r.DataEntrada
            })
            .ToListAsync();

        var model = new DashboardViewModel
        {
            TotalClientes = totalClientes,
            TotalEquipamentos = totalEquipamentos,
            TotalTecnicosAtivos = totalTecnicosAtivos,
            ReparacoesEmCurso = reparacoesEmCurso,
            ReparacoesConcluidas = reparacoesConcluidas,
            ReparacoesAguardarPecas = reparacoesAguardarPecas,
            ReparacoesPorEstado = reparacoesPorEstado
                .Select(x => new DashboardChartPointViewModel(x.Rotulo, x.Valor))
                .ToList(),
            ReparacoesMensais = reparacoesMensais
                .Select(x => new DashboardChartPointViewModel($"{x.Ano}/{x.Mes:00}", x.Valor))
                .ToList(),
            EvolucaoCustos = evolucaoCustos
                .Select(x => new DashboardCostPointViewModel($"{x.Ano}/{x.Mes:00}", x.CustoMaoObra, x.CustoPecas))
                .ToList(),
            AtividadeRecente = atividadeRecente
                .Select(x => new DashboardActivityViewModel(x.Tipo, x.RegistoId, x.Descricao, x.Data))
                .ToList(),
            Alertas = alertas
                .Select(x => new DashboardAlertViewModel(x.Tipo, x.Descricao, x.DataReferencia))
                .ToList()
        };

        return View(model);
    }

    public IActionResult Error()
    {
        return View();
    }
}
