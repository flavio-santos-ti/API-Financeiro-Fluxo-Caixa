using AutoMapper;
using Financeiro.FluxoCaixa.Domain.Dtos.Categoria;
using Financeiro.FluxoCaixa.Domain.Dtos.Cliente;
using Financeiro.FluxoCaixa.Domain.Dtos.Result;
using Financeiro.FluxoCaixa.Domain.Entities;
using Financeiro.FluxoCaixa.Domain.Interface.Repository;
using Financeiro.FluxoCaixa.Domain.Interface.Service;
using Financeiro.FluxoCaixa.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Business.Service;

public class CategoriaService : BaseService, ICategoriaService
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaService(IConfiguration configuration, IMapper mapper, ICategoriaRepository categoriaRepository)
    {
        _configuration = configuration;
        _mapper = mapper;
        _categoriaRepository = categoriaRepository;
    }

    public async Task<ResultCreateDto> SetCadastrarAsync(CategoriaCreateDto dados)
    {
        using (var context = new DatabaseContext(_configuration))
        {
            dados.Nome = dados.Nome.ToUpper();
            dados.Tipo = dados.Tipo.ToUpper();

            if (dados.Tipo != "E" && dados.Tipo != "S")
            {
                return base.ResultCreate(false, "O tipo de categoria deve ser a letra E ou S.", 0);
            }

            var categoria = await _categoriaRepository.GetNoTrackingAsync(context, dados.Nome);

            if (categoria != null)
            {
                return base.ResultCreate(false, "Categoria já cadastrada!", categoria.Id);
            }

            var categoriaNew = _mapper.Map<Categoria>(dados);

            return await _categoriaRepository.SetIncluirAsync(context, categoriaNew);
        }
    }

    public async Task<ResultDeleteDto> SetRemoverAsync(int id)
    {
        using (var context = new DatabaseContext(_configuration))
        {
            var categoria = await _categoriaRepository.GetNoTrackingAsync(context, id);

            if (categoria == null)
            {
                return base.ResultDelete(false, "Categoria não localizada!", 1);
            }

            return await _categoriaRepository.SetExcluirAsync(context, id);
        }

    }

    public async Task<IEnumerable<CategoriaDto>> GetListarAsync()
    {
        using (var context = new DatabaseContext(_configuration))
        {
            var categorias = await _categoriaRepository.GetListarAsync(context);

            return _mapper.Map<IEnumerable<CategoriaDto>>(categorias);

        }
    }

    private async Task<Categoria> GetNoTrackingAsync(DbContext ctx, long idCategoria)
    {
        var context = (DatabaseContext)ctx;

        return await _categoriaRepository.GetNoTrackingAsync(context, idCategoria);
    }

    public async Task<ResultCreateDto> ParseAsync(DbContext ctx, int id)
    {
        var context = (DatabaseContext)ctx;

        string invalidMessage = string.Empty;

        var categoria = await GetNoTrackingAsync(context, id);

        if (categoria == null) 
            invalidMessage = "Categoria não cadastrada!";

        return base.ResultCreate((invalidMessage == string.Empty), invalidMessage, id);
    }

}