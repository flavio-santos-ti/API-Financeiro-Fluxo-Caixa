using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Financeiro.FluxoCaixa.Infra.Data.Context;
using Financeiro.FluxoCaixa.Domain.Dtos.Result;
using Financeiro.FluxoCaixa.Domain.Dtos.Pessoa;
using Financeiro.FluxoCaixa.Domain.Entities;
using Financeiro.FluxoCaixa.Domain.Interface.Service;

namespace Financeiro.FluxoCaixa.Business.Service;

public class PessoaService : BaseService, IPessoaService
{
    public PessoaService()
    {
    }

    public async Task<ResultCreateDto> SetIncluirAsync(DbContext ctx, PessoaCreateDto dados)
    {
        var context = (DatabaseContext)ctx;

        try
        {
            dados.Nome = ClearString(dados.Nome.ToUpper());
            string hashNome = GetHashMD5(dados.Nome);
            var pessoaRegistered = await context.Pessoas
            .Where(b => b.HashNome == hashNome)
            .FirstOrDefaultAsync();

            if (pessoaRegistered == null)
            {
                Pessoa pessoaNew = new();
                pessoaNew.Nome = dados.Nome;
                pessoaNew.HashNome = hashNome;
                pessoaNew.DataInclusao = DateTime.Now;
                context.Pessoas.Add(pessoaNew);
                await context.SaveChangesAsync();

                return base.ResultCreate(true, "Pessoa cadastrada com sucesso!", pessoaNew.Id);
            }
            else
            {
                return base.ResultCreate(true, "Pessoa já cadastrada!", pessoaRegistered.Id);
            }
        }
        catch (Exception ex)
        {
            return base.ResultCreate(false, "Erro ao tentar cadastrar/consultar uma pessoa:" + ex.Message, 0);
        }
    }
}
