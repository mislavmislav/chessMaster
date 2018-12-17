using ServiceStack.Redis;

namespace RClient
{
    public class Client
    {

        public string Test()
        {
            var manager = new RedisManagerPool("rediscache");
            using (var client = manager.GetClient())
            {
                client.Set("foo", "bar");
                return $"foo={client.Get<string>("foo")}";
            }
        }
    }
}
