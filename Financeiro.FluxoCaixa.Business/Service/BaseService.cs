using Financeiro.FluxoCaixa.Domain.DTO.Result;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Business.Service;

public class BaseService
{
    public BaseService()
    {
    }

    protected static string GetHashMD5(string input)
    {
        using (MD5 md5Hash = System.Security.Cryptography.MD5.Create())
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString().ToUpper();
        }
    }

    public static string ClearString(string text)
    {
        return new string(text
                .Normalize(NormalizationForm.FormD)
                .Where(ch => char.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
                .ToArray());
    }

    protected ResultCreateDTO ResultCreate(bool successed, string message, long id)
    {
        ResultCreateDTO resultCreate = new();

        resultCreate.Successed = successed;
        resultCreate.Message = message;
        resultCreate.Id = id;

        return resultCreate;
    }

    protected ResultDeleteDTO ResultDelete(bool successed, string message, int count)
    {
        ResultDeleteDTO resultDelete = new();

        resultDelete.Successed = successed;
        resultDelete.Message = message;
        resultDelete.Count = count;

        return resultDelete;
    }


}
