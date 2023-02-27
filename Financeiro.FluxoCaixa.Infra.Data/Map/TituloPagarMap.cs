using Financeiro.FluxoCaixa.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Infra.Data.Map;

public class TituloPagarMap : IEntityTypeConfiguration<TituloPagar>
{
    public void Configure(EntityTypeBuilder<TituloPagar> builder)
    {
        builder.ToTable("titulo_pagar");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.CategoriaId).HasColumnName("categoria_id");
        builder.Property(x => x.Descricao).HasColumnName("descricao");
        builder.Property(x => x.ValorReal).HasColumnName("valor_real");
        builder.Property(x => x.DataReal).HasColumnName("dt_real");
        builder.Property(x => x.FornecedorId).HasColumnName("fornecedor_id");
        builder.Property(x => x.DataInclusao).HasColumnName("dt_inclusao");
        builder.Property(x => x.ExtratoId).HasColumnName("extrato_id");
    }
}
