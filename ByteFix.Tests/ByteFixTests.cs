using ByteFix.Controllers;
using ByteFix.Data;
using ByteFix.Models;
using ByteFix.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ByteFix.Tests;

public sealed class ByteFixTests : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly ApplicationDbContext _context;

    public ByteFixTests()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(_connection)
            .Options;

        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();
    }

    [Fact]
    public async Task CanCreateCustomerAndEquipment()
    {
        var cliente = new Cliente { Nome = "Ana Costa", NIF = "123456789", Email = "ana@bytefix.pt", Telefone = "912345678" };
        var equipamento = new Equipamento
        {
            Cliente = cliente,
            TipoEquipamento = "Laptop",
            Marca = "Dell",
            Modelo = "Latitude 5530",
            NumeroSerie = "ABC123",
            Descricao = "Laptop para revisão"
        };

        _context.Clientes.Add(cliente);
        _context.Equipamentos.Add(equipamento);
        await _context.SaveChangesAsync();

        var stored = await _context.Equipamentos
            .Include(e => e.Cliente)
            .SingleAsync();

        Assert.Equal("Ana Costa", stored.Cliente.Nome);
        Assert.Equal("Dell", stored.Marca);
    }

    [Fact]
    public async Task CanRegisterTechnicianRepairAndIntervention()
    {
        var cliente = new Cliente { Nome = "Pedro Silva", NIF = "987654321", Email = "pedro@bytefix.pt" };
        var equipamento = new Equipamento
        {
            Cliente = cliente,
            TipoEquipamento = "Desktop",
            Marca = "HP",
            Modelo = "EliteDesk",
            NumeroSerie = "HP-2024" 
        };

        var tecnico = new Tecnico
        {
            Nome = "Rui Martins",
            Email = "rui@bytefix.pt",
            Especialidade = "Hardware",
            Ativo = true
        };

        var reparacao = new Reparacao
        {
            Equipamento = equipamento,
            Tecnico = tecnico,
            DescricaoAvaria = "Não liga",
            Estado = ReparacaoEstado.EmReparacao,
            DataEntrada = new DateTime(2026, 10, 10, 9, 0, 0, DateTimeKind.Utc)
        };

        var intervencao = new Intervencao
        {
            Reparacao = reparacao,
            DescricaoTrabalho = "Substituição de fonte e limpeza interna",
            TempoTrabalho = 2.5m,
            CustoMaoObra = 65.50m,
            CustoPecas = 120.00m
        };

        _context.Clientes.Add(cliente);
        _context.Equipamentos.Add(equipamento);
        _context.Tecnicos.Add(tecnico);
        _context.Reparacoes.Add(reparacao);
        _context.Intervencoes.Add(intervencao);
        await _context.SaveChangesAsync();

        var storedRepair = await _context.Reparacoes
            .Include(r => r.Tecnico)
            .Include(r => r.Intervencoes)
            .SingleAsync();

        Assert.Equal("Rui Martins", storedRepair.Tecnico!.Nome);
        Assert.Equal(ReparacaoEstado.EmReparacao, storedRepair.Estado);
        Assert.Single(storedRepair.Intervencoes);
        Assert.Equal(185.50m, storedRepair.Intervencoes.Sum(i => i.CustoMaoObra + i.CustoPecas));
    }

    [Fact]
    public async Task DashboardCountsMatchStoredData()
    {
        var cliente = new Cliente { Nome = "Marta Lemos", NIF = "111111111", Email = "marta@bytefix.pt" };
        var equipamento = new Equipamento
        {
            Cliente = cliente,
            TipoEquipamento = "Tablet",
            Marca = "Samsung",
            Modelo = "Galaxy Tab",
            NumeroSerie = "TAB-001"
        };

        var reparacao1 = new Reparacao
        {
            Equipamento = equipamento,
            DescricaoAvaria = "Ecrã trincado",
            Estado = ReparacaoEstado.AguardarPecas,
            DataEntrada = new DateTime(2026, 10, 01, 9, 0, 0, DateTimeKind.Utc)
        };

        var reparacao2 = new Reparacao
        {
            Equipamento = equipamento,
            DescricaoAvaria = "Sem som",
            Estado = ReparacaoEstado.Concluida,
            DataEntrada = new DateTime(2026, 10, 02, 9, 0, 0, DateTimeKind.Utc),
            DataEntrega = new DateTime(2026, 10, 03, 9, 0, 0, DateTimeKind.Utc)
        };

        _context.Clientes.Add(cliente);
        _context.Equipamentos.Add(equipamento);
        _context.Reparacoes.AddRange(reparacao1, reparacao2);
        await _context.SaveChangesAsync();

        var controller = new HomeController(_context);
        var result = await controller.Index();

        var view = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<DashboardViewModel>(view.Model);

        Assert.Equal(1, model.TotalClientes);
        Assert.Equal(1, model.TotalEquipamentos);
        Assert.Equal(1, model.ReparacoesAguardarPecas);
        Assert.Equal(1, model.ReparacoesConcluidas);
        Assert.Contains(model.ReparacoesPorEstado, x => x.Rotulo == ReparacaoEstado.AguardarPecas && x.Valor == 1);
        Assert.Contains(model.ReparacoesPorEstado, x => x.Rotulo == ReparacaoEstado.Concluida && x.Valor == 1);
    }

    [Fact]
    public async Task DuplicateNifIsRejectedByDatabase()
    {
        var cliente1 = new Cliente { Nome = "Cliente 1", NIF = "222222222", Email = "cliente1@bytefix.pt" };
        var cliente2 = new Cliente { Nome = "Cliente 2", NIF = "222222222", Email = "cliente2@bytefix.pt" };

        _context.Clientes.Add(cliente1);
        await _context.SaveChangesAsync();

        _context.Clientes.Add(cliente2);

        await Assert.ThrowsAsync<DbUpdateException>(() => _context.SaveChangesAsync());
    }

    public void Dispose()
    {
        _context.Dispose();
        _connection.Dispose();
    }
}
