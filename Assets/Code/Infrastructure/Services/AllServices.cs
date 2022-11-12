namespace Code.Infrastructure.Services
{
    //Место в котором мы можем получить реализацию по запросу некоторых интерфейсов
    public class AllServices
    {
        private static AllServices _instance;
        public static AllServices Container => _instance ?? (_instance = new AllServices());

        public void RegisterSingle<TService>(TService implementation) where TService :  IService => 
            Implementation<TService>.serviceInstance = implementation;

        public TService Single<TService>() where TService :  IService =>
            Implementation<TService>.serviceInstance;

        private  static class  Implementation<TService> where TService: IService
        {
            public static TService serviceInstance;
        }
    }
}