namespace 服务注册的生命周期测试.Models
{
    public class TransientModel
    {
        public TransientModel()
        {
            System.Console.WriteLine("被创建: " + nameof(TransientModel));
        }
    }
}