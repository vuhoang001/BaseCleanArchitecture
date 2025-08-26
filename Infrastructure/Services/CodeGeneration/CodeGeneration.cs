using System.Linq.Expressions;
using Application.Interfaces;
using Infrastructure.Data;
using Shared.ExceptionBase;

namespace Infrastructure.Services.CodeGeneration;

public class CodeGeneration(ApplicationDbContext context) : ICodeGeneration
{
    public async Task<string> GenerateCodeAsync<T>(Expression<Func<T, string>> fieldSelector, string prefix,
        int length = 5)
        where T : class
    {
        var dbSet = context.Set<T>();
        var memberExpression = fieldSelector.Body as MemberExpression ??
            throw new ApiBadRequestException("Field selector must be a member expression");

        var filedName = memberExpression.Member.Name;

        var lastcode = await dbSet
            .Where(x => EF.Property<string>(x, filedName).StartsWith(prefix))
            .Where(x => EF.Property<string>(x, filedName).Length == (length + prefix.Length))
            .OrderByDescending(x => EF.Property<string>(x, filedName))
            .Select(x => EF.Property<string>(x, filedName))
            .FirstOrDefaultAsync();

        int nextNumber = 1;

        if (!string.IsNullOrEmpty(lastcode))
        {
            string numberPart = lastcode[prefix.Length..];
            if (int.TryParse(numberPart, out int lastNumber))
            {
                nextNumber = lastNumber + 1;
            }
        }

        string format = $"{prefix}{{0:D{length}}}";
        return string.Format(format, nextNumber);
    }
}