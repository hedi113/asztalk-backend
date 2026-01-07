using System.Reflection;

namespace Solution.Domain.Database;

public class DomainAssemblyReference
{
    public static readonly Assembly Assembly = typeof(DomainAssemblyReference).Assembly;
}
