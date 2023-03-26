using Financeiro.FluxoCaixa.Domain.Dtos.Result;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

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


}
