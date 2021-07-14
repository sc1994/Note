namespace 服务注册的生命周期测试.Models
{
    public class ScopedModel
    {
        public ScopedModel()
        {
            System.Console.WriteLine("被创建: "+nameof(ScopedModel));
        }
    }
}