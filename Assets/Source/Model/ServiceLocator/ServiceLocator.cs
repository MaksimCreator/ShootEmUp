namespace ShootEmUp.Model
{
    public class ServiceLocator
    {
        public void RegistareService<TService>(TService service) where TService : IService
        => Implamentation<TService>.Instance = service;

        public TService GetService<TService>() where TService : IService
        => Implamentation<TService>.Instance;

        private static class Implamentation<TService> where TService : IService
        {
            public static TService Instance;
        }
    }
}
