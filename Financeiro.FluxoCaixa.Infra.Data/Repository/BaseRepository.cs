using Dapper;
using Financeiro.FluxoCaixa.Domain.Dtos.Fornecedor;
using Financeiro.FluxoCaixa.Domain.Dtos.Result;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Infra.Data.Repository;

public class BaseRepository
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public BaseRepository(IConfiguration configuration = null)
    {
        if (configuration != null)
        {
            _configuration = configuration;
        }
        _connectionString = _configuration.GetConnectionString("PgSqlConnection");
    }

    protected ResultCreateDto ResultCreate(bool successed, string message, long id)
    {
        ResultCreateDto resultCreate = new();

        resultCreate.Successed = successed;
        resultCreate.Message = message;
        resultCreate.Id = id;

        return resultCreate;
    }

    protected ResultDeleteDto ResultDelete(bool successed, string message, int count)
    {
        ResultDeleteDto resultDelete = new();

        resultDelete.Successed = successed;
        resultDelete.Message = message;
        resultDelete.Count = count;

        return resultDelete;
    }

    protected async Task<dynamic> ListarAsync<T>(string sqlQuery)
    {
        using (NpgsqlConnection conexao = new NpgsqlConnection(_connectionString))
        {
            return await conexao.QueryAsync<T>(sqlQuery);
        }
    }

    protected async Task<dynamic> ListarAsync<T>(string sqlQuery, object model)
    {
        using (NpgsqlConnection conexao = new NpgsqlConnection(_connectionString))
        {
            return await conexao.QueryAsync<T>(sqlQuery, model);
        }
    }

    protected async Task<T> GetFirstOrDefaultAsync<T>(string sqlQuery, object model)
    {
        using (NpgsqlConnection conexao = new NpgsqlConnection(_connectionString))
        {
            return await conexao.QueryFirstOrDefaultAsync<T>(sqlQuery, model);
        }
    }

    protected async Task<T> FirstOrDefaultAsync<T>(string sqlQuery)
    {
        using (NpgsqlConnection conexao = new NpgsqlConnection(_connectionString))
        {
            return await conexao.QueryFirstOrDefaultAsync<T>(sqlQuery);
        }
    }


}
