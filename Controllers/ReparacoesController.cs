using ByteFix.Data;
using ByteFix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ByteFix.Controllers;

public sealed class ReparacoesController : Controller
{
    private readonly ApplicationDbContext _context;

    public ReparacoesController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var reparacoes = await _context.Reparacoes
            .Include(r => r.Equipamento)
            .Include(r => r.Tecnico)
            .OrderByDescending(r => r.DataEntrada)
            .ToListAsync();

        return View(reparacoes);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Equipamentos = await _context.Equipamentos
            .Include(e => e.Cliente)
            .OrderBy(e => e.Cliente.Nome)
            .ThenBy(e => e.Marca)
            .Select(e => new SelectListItem($"{e.Cliente.Nome} - {e.Marca} {e.Modelo}", e.Id.ToString()))
            .ToListAsync();
        ViewBag.Tecnicos = await _context.Tecnicos
            .OrderBy(t => t.Nome)
            .Select(t => new SelectListItem(t.Nome, t.Id.ToString()))
            .ToListAsync();
        ViewBag.Estados = ReparacaoEstado.Todos.Select(estado => new SelectListItem(estado, estado)).ToList();
        return View(new Reparacao());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Reparacao reparacao)
    {
        if (!ModelState.IsValid)
        {
            await PrepareSelectLists();
            return View(reparacao);
        }

        if (!await _context.Equipamentos.AnyAsync(e => e.Id == reparacao.EquipamentoId))
        {
            ModelState.AddModelError(nameof(reparacao.EquipamentoId), "Equipamento inválido.");
            await PrepareSelectLists();
            return View(reparacao);
        }

        if (reparacao.TecnicoId.HasValue && !await _context.Tecnicos.AnyAsync(t => t.Id == reparacao.TecnicoId.Value))
        {
            ModelState.AddModelError(nameof(reparacao.TecnicoId), "Técnico inválido.");
            await PrepareSelectLists();
            return View(reparacao);
        }

        reparacao.DataEntrada = DateTime.UtcNow;
        _context.Reparacoes.Add(reparacao);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var reparacao = await _context.Reparacoes.FindAsync(id);
        if (reparacao == null)
        {
            return NotFound();
        }

        await PrepareSelectLists();
        return View(reparacao);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Reparacao reparacao)
    {
        if (id != reparacao.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            await PrepareSelectLists();
            return View(reparacao);
        }

        var entity = await _context.Reparacoes.FindAsync(id);
        if (entity == null)
        {
            return NotFound();
        }

        entity.EquipamentoId = reparacao.EquipamentoId;
        entity.TecnicoId = reparacao.TecnicoId;
        entity.DescricaoAvaria = reparacao.DescricaoAvaria;
        entity.Diagnostico = reparacao.Diagnostico;
        entity.Estado = reparacao.Estado;
        entity.DataConclusao = reparacao.DataConclusao;
        entity.DataEntrega = reparacao.DataEntrega;
        entity.Observacoes = reparacao.Observacoes;

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var reparacao = await _context.Reparacoes
            .Include(r => r.Equipamento)
            .Include(r => r.Tecnico)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (reparacao == null)
        {
            return NotFound();
        }

        return View(reparacao);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var reparacao = await _context.Reparacoes
            .Include(r => r.Intervencoes)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (reparacao == null)
        {
            return NotFound();
        }

        if (reparacao.Intervencoes.Any())
        {
            TempData["Error"] = "Não é possível eliminar uma reparação com intervenções associadas.";
            return RedirectToAction(nameof(Index));
        }

        _context.Reparacoes.Remove(reparacao);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task PrepareSelectLists()
    {
        ViewBag.Equipamentos = await _context.Equipamentos
            .Include(e => e.Cliente)
            .OrderBy(e => e.Cliente.Nome)
            .ThenBy(e => e.Marca)
            .Select(e => new SelectListItem($"{e.Cliente.Nome} - {e.Marca} {e.Modelo}", e.Id.ToString()))
            .ToListAsync();

        ViewBag.Tecnicos = await _context.Tecnicos
            .OrderBy(t => t.Nome)
            .Select(t => new SelectListItem(t.Nome, t.Id.ToString()))
            .ToListAsync();

        ViewBag.Estados = ReparacaoEstado.Todos.Select(estado => new SelectListItem(estado, estado)).ToList();
    }
}
