using ByteFix.Data;
using ByteFix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ByteFix.Controllers;

// Classe: gere o registo e a consulta do trabalho feito nas reparações.
public sealed class IntervencoesController : Controller
{
    // Atributo: acesso aos dados da aplicação.
    private readonly ApplicationDbContext _context;

    public IntervencoesController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Carrega as relações necessárias para identificar a reparação e o equipamento em cada linha.
        var intervencoes = await _context.Intervencoes
            .Include(i => i.Reparacao)
            .ThenInclude(r => r.Equipamento)
            .OrderByDescending(i => i.DataIntervencao)
            .ToListAsync();

        return View(intervencoes);
    }

    public async Task<IActionResult> Create()
    {
        // Disponibiliza reparações existentes para o campo de seleção do formulário.
        ViewBag.Reparacoes = await _context.Reparacoes
            .Include(r => r.Equipamento)
            .ThenInclude(e => e.Cliente)
            .OrderBy(r => r.DataEntrada)
            .Select(r => new SelectListItem($"{r.Equipamento.Cliente.Nome} - {r.Equipamento.Marca} {r.Equipamento.Modelo}", r.Id.ToString()))
            .ToListAsync();

        return View(new Intervencao());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Intervencao intervencao)
    {
        // Confirma que a intervenção fica ligada a uma reparação existente.
        if (!ModelState.IsValid)
        {
            await PrepareSelectLists();
            return View(intervencao);
        }

        if (!await _context.Reparacoes.AnyAsync(r => r.Id == intervencao.ReparacaoId))
        {
            ModelState.AddModelError(nameof(intervencao.ReparacaoId), "Reparação inválida.");
            await PrepareSelectLists();
            return View(intervencao);
        }

        // Regista a data no servidor para manter a cronologia consistente.
        intervencao.DataIntervencao = DateTime.UtcNow;
        _context.Intervencoes.Add(intervencao);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var intervencao = await _context.Intervencoes.FindAsync(id);
        if (intervencao == null)
        {
            return NotFound();
        }

        await PrepareSelectLists();
        return View(intervencao);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Intervencao intervencao)
    {
        if (id != intervencao.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            await PrepareSelectLists();
            return View(intervencao);
        }

        var entity = await _context.Intervencoes.FindAsync(id);
        if (entity == null)
        {
            return NotFound();
        }

        // Atualiza os campos do trabalho, sem alterar a data original da intervenção.
        entity.ReparacaoId = intervencao.ReparacaoId;
        entity.DescricaoTrabalho = intervencao.DescricaoTrabalho;
        entity.PecasUtilizadas = intervencao.PecasUtilizadas;
        entity.TempoTrabalho = intervencao.TempoTrabalho;
        entity.CustoMaoObra = intervencao.CustoMaoObra;
        entity.CustoPecas = intervencao.CustoPecas;
        entity.Observacoes = intervencao.Observacoes;

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var intervencao = await _context.Intervencoes
            .Include(i => i.Reparacao)
            .ThenInclude(r => r.Equipamento)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (intervencao == null)
        {
            return NotFound();
        }

        return View(intervencao);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var intervencao = await _context.Intervencoes.FindAsync(id);
        if (intervencao == null)
        {
            return NotFound();
        }

        _context.Intervencoes.Remove(intervencao);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task PrepareSelectLists()
    {
        // Mantém a lista de reparações disponível caso seja necessário voltar ao formulário.
        ViewBag.Reparacoes = await _context.Reparacoes
            .Include(r => r.Equipamento)
            .ThenInclude(e => e.Cliente)
            .OrderBy(r => r.DataEntrada)
            .Select(r => new SelectListItem($"{r.Equipamento.Cliente.Nome} - {r.Equipamento.Marca} {r.Equipamento.Modelo}", r.Id.ToString()))
            .ToListAsync();
    }
}
