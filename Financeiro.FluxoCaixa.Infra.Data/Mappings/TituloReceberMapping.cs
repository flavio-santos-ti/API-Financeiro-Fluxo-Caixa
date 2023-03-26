using Financeiro.FluxoCaixa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Infra.Data.Mappings;

public class TituloReceberMapping : IEntityTypeConfiguration<TituloReceber>
{
    public void Configure(EntityTypeBuilder<TituloReceber> builder)
    {
        builder.ToTable("titulo_receber");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.CategoriaId).HasColumnName("categoria_id");
        builder.Property(x => x.Descricao).HasColumnName("descricao");
        builder.Property(x => x.ValorReal).HasColumnName("valor_real");
        builder.Property(x => x.DataReal).HasColumnName("dt_real");
        builder.Property(x => x.ClienteId).HasColumnName("cliente_id");
        builder.Property(x => x.DataInclusao).HasColumnName("dt_inclusao");
        builder.Property(x => x.ExtratoId).HasColumnName("extrato_id");
    }
}
