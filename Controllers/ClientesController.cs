using ByteFix.Data;
using ByteFix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ByteFix.Controllers;

// Classe: gere a consulta e as operações de criação, edição e remoção de clientes.
public sealed class ClientesController : Controller
{
    // Atributo: acesso aos dados da aplicação.
    private readonly ApplicationDbContext _context;

    public ClientesController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var clientes = await _context.Clientes
            .OrderBy(c => c.Nome)
            .ToListAsync();

        return View(clientes);
    }

    public IActionResult Create()
    {
        return View(new Cliente());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Cliente cliente)
    {
        if (!ModelState.IsValid)
        {
            return View(cliente);
        }

        cliente.DataRegisto = DateTime.UtcNow;
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente == null)
        {
            return NotFound();
        }

        return View(cliente);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Cliente cliente)
    {
        if (id != cliente.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(cliente);
        }

        try
        {
            var entity = await _context.Clientes.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            entity.Nome = cliente.Nome;
            entity.NIF = cliente.NIF;
            entity.Email = cliente.Email;
            entity.Telefone = cliente.Telefone;
            entity.Morada = cliente.Morada;
            entity.CodigoPostal = cliente.CodigoPostal;
            entity.Localidade = cliente.Localidade;

            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Clientes.AnyAsync(c => c.Id == id))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
        if (cliente == null)
        {
            return NotFound();
        }

        return View(cliente);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var cliente = await _context.Clientes.Include(c => c.Equipamentos).FirstOrDefaultAsync(c => c.Id == id);
        if (cliente == null)
        {
            return NotFound();
        }

        if (cliente.Equipamentos.Any())
        {
            TempData["Error"] = "Não é possível eliminar um cliente com equipamentos associados.";
            return RedirectToAction(nameof(Index));
        }

        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
