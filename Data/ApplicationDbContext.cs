using ByteFix.Models;
using Microsoft.EntityFrameworkCore;

namespace ByteFix.Data;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Equipamento> Equipamentos => Set<Equipamento>();
    public DbSet<Tecnico> Tecnicos => Set<Tecnico>();
    public DbSet<Reparacao> Reparacoes => Set<Reparacao>();
    public DbSet<Intervencao> Intervencoes => Set<Intervencao>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasIndex(cliente => cliente.NIF).IsUnique();
            entity.HasMany(cliente => cliente.Equipamentos)
                .WithOne(equipamento => equipamento.Cliente)
                .HasForeignKey(equipamento => equipamento.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Equipamento>(entity =>
        {
            entity.HasIndex(equipamento => equipamento.NumeroSerie)
                .IsUnique()
                .HasFilter("\"NumeroSerie\" IS NOT NULL");
            entity.HasMany(equipamento => equipamento.Reparacoes)
                .WithOne(reparacao => reparacao.Equipamento)
                .HasForeignKey(reparacao => reparacao.EquipamentoId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Tecnico>(entity =>
        {
            entity.HasIndex(tecnico => tecnico.Email).IsUnique();
            entity.HasMany(tecnico => tecnico.Reparacoes)
                .WithOne(reparacao => reparacao.Tecnico)
                .HasForeignKey(reparacao => reparacao.TecnicoId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Reparacao>(entity =>
        {
            var estadosPermitidos = string.Join(", ", ReparacaoEstado.Todos.Select(estado =>
                $"'{estado.Replace("'", "''")}'"));

            entity.ToTable(table => table.HasCheckConstraint(
                "CK_Reparacoes_Estado",
                $"\"Estado\" IN ({estadosPermitidos})"));
            entity.HasMany(reparacao => reparacao.Intervencoes)
                .WithOne(intervencao => intervencao.Reparacao)
                .HasForeignKey(intervencao => intervencao.ReparacaoId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Intervencao>(entity =>
        {
            entity.Property(intervencao => intervencao.TempoTrabalho)
                .HasConversion<long>(
                    horas => decimal.ToInt64(decimal.Round(horas * 100m, 0, MidpointRounding.ToEven)),
                    centesimos => centesimos / 100m)
                .HasColumnType("INTEGER");
            entity.Property(intervencao => intervencao.CustoMaoObra)
                .HasConversion<long>(
                    euros => decimal.ToInt64(decimal.Round(euros * 100m, 0, MidpointRounding.ToEven)),
                    centimos => centimos / 100m)
                .HasColumnType("INTEGER");
            entity.Property(intervencao => intervencao.CustoPecas)
                .HasConversion<long>(
                    euros => decimal.ToInt64(decimal.Round(euros * 100m, 0, MidpointRounding.ToEven)),
                    centimos => centimos / 100m)
                .HasColumnType("INTEGER");
            entity.ToTable(table =>
            {
                table.HasCheckConstraint("CK_Intervencoes_TempoTrabalho", "\"TempoTrabalho\" >= 0");
                table.HasCheckConstraint("CK_Intervencoes_CustoMaoObra", "\"CustoMaoObra\" >= 0");
                table.HasCheckConstraint("CK_Intervencoes_CustoPecas", "\"CustoPecas\" >= 0");
            });
        });
    }
}
