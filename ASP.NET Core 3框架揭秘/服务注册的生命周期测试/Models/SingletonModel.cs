namespace 服务注册的生命周期测试.Models
{
    public class SingletonModel
    {
        public SingletonModel()
        {
            System.Console.WriteLine("被创建: "+nameof(SingletonModel));
        }
    }
}