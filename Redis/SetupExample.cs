using StackExchange.Redis;

class SetupExample
{
    static void Main()
    {
        // Connect to the Redis server
        var redisServer = ConnectionMultiplexer.Connect("localhost:6379");

        // Get a reference to the Redis database
        IDatabase db = redisServer.GetDatabase();

        // Set a key-value pair
        db.StringSet("myKey", "Hello, Redis!");

        // Retrieve a value by key
        string value = db.StringGet("myKey");
        Console.WriteLine("Retrieved value: " + value);

        // Delete the key
        bool keyDeleted = db.KeyDelete("myKey");
        Console.WriteLine("Key deleted: " + keyDeleted);
    }
}
