using Financeiro.FluxoCaixa.Business.Service;
using Financeiro.FluxoCaixa.Domain.Dtos.Categoria;
using Financeiro.FluxoCaixa.Domain.Dtos.Cliente;
using Financeiro.FluxoCaixa.Domain.Dtos.TituloPagar;
using Financeiro.FluxoCaixa.Domain.Dtos.TituloReceber;
using Financeiro.FluxoCaixa.Domain.Interface.Repository;
using Financeiro.FluxoCaixa.Domain.Interface.Service;
using Financeiro.FluxoCaixa.Infra.Data.Context;
using Financeiro.FluxoCaixa.Infra.Data.Repository;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("PgSqlConnection");


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DatabaseContext>(opts => opts.UseNpgsql(connectionString) );
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IPessoaService, PessoaService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IFornecedorRepository, FornecedorRepository>();
builder.Services.AddScoped<IFornecedorService, FornecedorService>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<ITituloPagarRepository, TituloPagarRepository>();
builder.Services.AddScoped<ITituloPagarService, TituloPagarService>();
builder.Services.AddScoped<ITituloReceberRepository, TituloReceberRepository>();
builder.Services.AddScoped<ITituloReceberService, TituloReceberService>();
builder.Services.AddScoped<IExtratoRepository, ExtratoRepository>();
builder.Services.AddScoped<IExtratoService, ExtratoService>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ICaixaService, CaixaService>();
builder.Services.AddScoped<ISaldoDiarioRepository, SaldoDiarioRepository>();
builder.Services.AddScoped<ISaldoDiarioService, SaldoDiarioService>();

// Fluent Validation
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<ClienteCreateDto>();
builder.Services.AddValidatorsFromAssemblyContaining<TituloPagarCreateDto>();
builder.Services.AddValidatorsFromAssemblyContaining<TituloReceberCreateDto>();
builder.Services.AddValidatorsFromAssemblyContaining<CategoriaCreateDto>();

builder.Services.AddApiVersioning(p =>
{
    p.DefaultApiVersion = new ApiVersion(1, 0);
    p.ReportApiVersions = true;
    p.AssumeDefaultVersionWhenUnspecified = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
