using System.Linq.Expressions;

namespace Application.Interfaces;

public interface ICodeGeneration
{
    Task<string> GenerateCodeAsync<T>(Expression<Func<T, string>> fieldSelector, string prefix, int length = 5)
        where T : class;
}