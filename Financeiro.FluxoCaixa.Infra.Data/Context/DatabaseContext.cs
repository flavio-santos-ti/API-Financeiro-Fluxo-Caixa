using Financeiro.FluxoCaixa.Domain.Entity;
using Financeiro.FluxoCaixa.Infra.Data.Map;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Infra.Data.Context;

public class DatabaseContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DatabaseContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Fornecedor> Fornecedores { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<TituloPagar> TitulosPagar { get; set; }
    public DbSet<TituloReceber> TitulosReceber { get; set; }
    public DbSet<Extrato> Extratos { get; set; }
    public DbSet<SaldoDiario> SaldosDiarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string? connectionString = _configuration.GetConnectionString("PgSqlConnection");
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PessoaMap());
        modelBuilder.ApplyConfiguration(new ClienteMap());
        modelBuilder.ApplyConfiguration(new FornecedorMap());
        modelBuilder.ApplyConfiguration(new TituloPagarMap());
        modelBuilder.ApplyConfiguration(new TituloReceberMap());
        modelBuilder.ApplyConfiguration(new CategoriaMap());
        modelBuilder.ApplyConfiguration(new ExtratoMap());
        modelBuilder.ApplyConfiguration(new SaldoDiarioMap());
    }

}


