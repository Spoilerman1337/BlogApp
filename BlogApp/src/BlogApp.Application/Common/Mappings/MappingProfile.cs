using AutoMapper;
using BlogApp.Application.Common.Interfaces;
using System.Reflection;

namespace BlogApp.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile(Assembly assembly) => ApplyMappingsFromAssembly(assembly);

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types = assembly.GetExportedTypes()
        .Where(type => type.GetInterfaces()
        .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
        .ToList();

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod("Mapping");
            methodInfo?.Invoke(instance, new object[] { this });
        }
    }
}
