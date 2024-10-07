public static class SingelServiceLocator
{
    public static TService GetService<TService>() where TService : ISingelService
    => Implamentation<TService>.Instance;

    public static void RegisatryService<TService>(TService service) where TService : ISingelService
    => Implamentation<TService>.Instance = service;

    private static class Implamentation<TService> where TService : ISingelService
    {
        public static TService Instance;
    }
}
