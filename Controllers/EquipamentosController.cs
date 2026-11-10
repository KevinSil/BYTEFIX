using ByteFix.Data;
using ByteFix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ByteFix.Controllers;

// Classe: gere os equipamentos e a sua ligação aos clientes.
public sealed class EquipamentosController : Controller
{
    // Atributo: acesso aos dados da aplicação.
    private readonly ApplicationDbContext _context;

    public EquipamentosController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var equipamentos = await _context.Equipamentos
            .Include(e => e.Cliente)
            .OrderBy(e => e.Marca)
            .ThenBy(e => e.Modelo)
            .ToListAsync();

        return View(equipamentos);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Clientes = await _context.Clientes
            .OrderBy(c => c.Nome)
            .Select(c => new SelectListItem(c.Nome, c.Id.ToString()))
            .ToListAsync();

        return View(new Equipamento());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Equipamento equipamento)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Clientes = await _context.Clientes
                .OrderBy(c => c.Nome)
                .Select(c => new SelectListItem(c.Nome, c.Id.ToString()))
                .ToListAsync();
            return View(equipamento);
        }

        if (!await _context.Clientes.AnyAsync(c => c.Id == equipamento.ClienteId))
        {
            ModelState.AddModelError(nameof(equipamento.ClienteId), "Cliente inválido.");
            ViewBag.Clientes = await _context.Clientes
                .OrderBy(c => c.Nome)
                .Select(c => new SelectListItem(c.Nome, c.Id.ToString()))
                .ToListAsync();
            return View(equipamento);
        }

        equipamento.DataRegisto = DateTime.UtcNow;
        _context.Equipamentos.Add(equipamento);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var equipamento = await _context.Equipamentos.FindAsync(id);
        if (equipamento == null)
        {
            return NotFound();
        }

        ViewBag.Clientes = await _context.Clientes
            .OrderBy(c => c.Nome)
            .Select(c => new SelectListItem(c.Nome, c.Id.ToString()))
            .ToListAsync();

        return View(equipamento);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Equipamento equipamento)
    {
        if (id != equipamento.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Clientes = await _context.Clientes
                .OrderBy(c => c.Nome)
                .Select(c => new SelectListItem(c.Nome, c.Id.ToString()))
                .ToListAsync();
            return View(equipamento);
        }

        var entity = await _context.Equipamentos.FindAsync(id);
        if (entity == null)
        {
            return NotFound();
        }

        entity.ClienteId = equipamento.ClienteId;
        entity.TipoEquipamento = equipamento.TipoEquipamento;
        entity.Marca = equipamento.Marca;
        entity.Modelo = equipamento.Modelo;
        entity.NumeroSerie = equipamento.NumeroSerie;
        entity.Descricao = equipamento.Descricao;

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var equipamento = await _context.Equipamentos
            .Include(e => e.Cliente)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (equipamento == null)
        {
            return NotFound();
        }

        return View(equipamento);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var equipamento = await _context.Equipamentos
            .Include(e => e.Reparacoes)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (equipamento == null)
        {
            return NotFound();
        }

        if (equipamento.Reparacoes.Any())
        {
            TempData["Error"] = "Não é possível eliminar um equipamento com reparações associadas.";
            return RedirectToAction(nameof(Index));
        }

        _context.Equipamentos.Remove(equipamento);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
