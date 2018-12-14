using ServiceStack.Redis;

namespace RClient
{
    class Client
    {

        public string Test()
        {
            var manager = new RedisManagerPool("localhost:6379");
            using (var client = manager.GetClient())
            {
                client.Set("foo", "bar");
                return $"foo={client.Get<string>("foo")}";
            }
        }
    }
}
