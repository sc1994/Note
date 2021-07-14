using 服务注册的生命周期测试.Models;

namespace 服务注册的生命周期测试.Services
{
    public class ScopedService
    {
        public ScopedService(
            ScopedModel scopedModel,
            SingletonModel singletonModel,
            TransientModel transientModel)
        {
            System.Console.WriteLine("被创建: " + nameof(ScopedService));
        }
    }
}