using AutoMapper;
using System.Reflection;

namespace Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                foreach (var mappingInterface in type.GetInterfaces().Where(x => x.Name == "IMapFrom`1"))
                {
                    var methodInfo = mappingInterface.GetMethod("Mapping");
                    methodInfo?.Invoke(instance, new object[] { this });
                }


            }
        }
    }
}
