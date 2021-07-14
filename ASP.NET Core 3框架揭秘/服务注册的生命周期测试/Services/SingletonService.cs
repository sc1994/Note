using 服务注册的生命周期测试.Models;

namespace 服务注册的生命周期测试.Services
{
    public class SingletonService
    {
        public SingletonService(
            SingletonModel singletonModel)
        {
            System.Console.WriteLine("被创建: " + nameof(SingletonService));
        }
    }
}