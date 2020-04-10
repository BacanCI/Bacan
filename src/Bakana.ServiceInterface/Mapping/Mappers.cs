namespace Bakana.ServiceInterface.Mapping
{
    public static class Mappers
    {
        public static void Register()
        {
            EntityToServiceModelMappers.Register();
            ServiceModelToEntityMappers.Register();
        }
        
    }
}