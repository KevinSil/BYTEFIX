using ByteFix.Data;
using ByteFix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ByteFix.Controllers;

public sealed class TecnicosController : Controller
{
    private readonly ApplicationDbContext _context;

    public TecnicosController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var tecnicos = await _context.Tecnicos
            .OrderBy(t => t.Nome)
            .ToListAsync();

        return View(tecnicos);
    }

    public IActionResult Create()
    {
        return View(new Tecnico());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Tecnico tecnico)
    {
        if (!ModelState.IsValid)
        {
            return View(tecnico);
        }

        _context.Tecnicos.Add(tecnico);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var tecnico = await _context.Tecnicos.FindAsync(id);
        if (tecnico == null)
        {
            return NotFound();
        }

        return View(tecnico);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Tecnico tecnico)
    {
        if (id != tecnico.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(tecnico);
        }

        var entity = await _context.Tecnicos.FindAsync(id);
        if (entity == null)
        {
            return NotFound();
        }

        entity.Nome = tecnico.Nome;
        entity.Email = tecnico.Email;
        entity.Telefone = tecnico.Telefone;
        entity.Especialidade = tecnico.Especialidade;
        entity.Ativo = tecnico.Ativo;

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var tecnico = await _context.Tecnicos.FirstOrDefaultAsync(t => t.Id == id);
        if (tecnico == null)
        {
            return NotFound();
        }

        return View(tecnico);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var tecnico = await _context.Tecnicos.Include(t => t.Reparacoes).FirstOrDefaultAsync(t => t.Id == id);
        if (tecnico == null)
        {
            return NotFound();
        }

        if (tecnico.Reparacoes.Any())
        {
            TempData["Error"] = "Não é possível eliminar um técnico com reparações associadas.";
            return RedirectToAction(nameof(Index));
        }

        _context.Tecnicos.Remove(tecnico);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
