﻿using Financeiro.FluxoCaixa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Infra.Data.Mappings;

public class PessoaMapping : IEntityTypeConfiguration<Pessoa>
{
    public void Configure(EntityTypeBuilder<Pessoa> builder)
    {
        builder.ToTable("pessoa");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Nome).HasColumnName("nome");
        builder.Property(x => x.HashNome).HasColumnName("hash_nome");
        builder.Property(x => x.DataInclusao).HasColumnName("dt_inclusao");
    }
}
