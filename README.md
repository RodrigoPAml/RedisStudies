# Redis

A repository to study the basics of redis using C#

Redis, which stands for "Remote Dictionary Server," is an open-source, in-memory data structure store. It is often referred to as a "data structure server" because it provides a way to store, manipulate, and manage data structures in memory. Redis is widely used for its speed, simplicity, and versatility. Here are some key features and use cases of Redis:

1. **In-Memory Data Store**: Redis stores data primarily in RAM, which makes it exceptionally fast for read and write operations.

2. **Key-Value Store**: Redis is a key-value store, where data is associated with unique keys. You can store various types of values, including strings, lists, sets, hashes, and more.

3. **Data Structures**: Redis provides a rich set of data structures, such as strings, lists, sets, sorted sets, hashes, bitmaps, and hyperloglogs. These data structures enable you to perform operations like adding, removing, and querying data efficiently.

4. **Caching**: Redis is often used as a cache for databases and frequently accessed data. Its in-memory nature allows for rapid data retrieval, reducing the load on the primary database.

5. **Pub/Sub Messaging**: Redis supports publish/subscribe messaging, making it suitable for building real-time applications and message brokers.

6. **Atomic Operations**: Redis provides atomic operations, allowing multiple clients to perform operations on shared data structures safely.

7. **Lua Scripting**: Redis allows you to execute Lua scripts, enabling custom server-side logic for complex data operations.

8. **Geospatial Indexing**: Redis supports geospatial data, allowing you to perform location-based queries and calculations.

9. **Persistence Options**: Redis offers different options for data persistence, such as snapshots and append-only files, to ensure data durability.

10. **High Availability**: Redis can be configured for high availability and data replication, ensuring that data is available even in the case of server failures.

11. **Cluster Mode**: Redis Cluster allows you to distribute data across multiple Redis instances to improve scalability and fault tolerance.

12. **Rich Ecosystem**: Redis has client libraries available for various programming languages, making it easy to integrate with applications.

Redis is commonly used in a wide range of applications, including web and mobile applications, real-time analytics, caching layers, session stores, leaderboards, and more. It is appreciated for its simplicity, speed, and the ability to handle high-throughput workloads, making it a popular choice for various use cases in the world of data storage and retrieval.

## Database 

Redis supports the concept of multiple databases (often referred to as "selectable databases") using numeric indexes, typically ranging from 0 to 15. Each database is separate and isolated from the others, and you can use different databases for different purposes or to isolate different parts of your application's data.

```C#
IDatabase db1 = connection.GetDatabase(1); // Select database 1
IDatabase db2 = connection.GetDatabase(2); // Select database 2

db1.StringSet("key1", "value1"); // Set a key in database 1
db2.StringSet("key2", "value2"); // Set a key in database 2

string value1 = db1.StringGet("key1"); // Get a key from database 1
string value2 = db2.StringGet("key2"); // Get a key from database 2
```

## Key Value

A trivial example with key and value

```C#
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
```

## Hash tables

In Redis, a hash is a data structure that maps fields to values. It is also referred to as a "hash table" or "dictionary" in some programming languages. Redis hashes are ideal for representing objects with multiple attributes, where each attribute is associated with a value. 

```C#
using StackExchange.Redis;
using System;

// Create a connection to the Redis server
ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("your_redis_server_address");
IDatabase db = redis.GetDatabase();

// Set a field in a hash
db.HashSet("user:1000", "username", "john_doe");
db.HashSet("user:1000", "age", 30);
db.HashSet("user:1000", "email", "john@example.com");

// Get the value associated with a field in a hash
string username = db.HashGet("user:1000", "username");
int age = (int)db.HashGet("user:1000", "age");
string email = db.HashGet("user:1000", "email");

Console.WriteLine($"Username: {username}");
Console.WriteLine($"Age: {age}");
Console.WriteLine($"Email: {email}");
```

## Lists

Redis Lists are similiar to any list structure that allows duplicates and are ordered (in the sense of choose the position of element)

1. **Add an Element to the Beginning (Left) of the List:**

 ```csharp
 IDatabase db = connection.GetDatabase();
 db.ListLeftPush("myList", "element1");
 db.ListLeftPush("myList", "element2");
 ```

2. **Add an Element to the End (Right) of the List:**

 ```csharp
 IDatabase db = connection.GetDatabase();
 db.ListRightPush("myList", "element3");
 db.ListRightPush("myList", "element4");
 ```

3. **Get Elements from a List:**

 You can retrieve elements from a list by their indexes.

 ```csharp
 IDatabase db = connection.GetDatabase();
 string element = db.ListGetByIndex("myList", 0); // Get the first element
 ```

4. **Get the Range of Elements from a List:**

You can get a range of elements from a list, e.g., from index 0 to -1 (the entire list).

```csharp
IDatabase db = connection.GetDatabase();
var elements = db.ListRange("myList", 0, -1);
foreach (var element in elements)
{
   Console.WriteLine(element);
}
```

5. **Remove Elements from a List:**

You can remove elements by specifying their values.

```csharp
IDatabase db = connection.GetDatabase();
long removedCount = db.ListRemove("myList", "element1");
```

6. **Get the Length of a List:**

You can retrieve the length of a list.

```csharp
IDatabase db = connection.GetDatabase();
long listLength = db.ListLength("myList");
```

7. **Blocking Pop from a List:**

You can perform a blocking pop operation, which will block until an element is available.

```csharp
IDatabase db = connection.GetDatabase();
var element = db.ListLeftPop("myList", TimeSpan.FromMinutes(1)); // Wait for up to 1 minute for an element
```

8. **Non-blocking Pop from a List:**

You can perform a non-blocking pop operation.

```csharp
IDatabase db = connection.GetDatabase();
var element = db.ListLeftPop("myList");
```

9. **Blocking Pop from Multiple Lists:**

You can perform a blocking pop operation on multiple lists.

```csharp
IDatabase db = connection.GetDatabase();
RedisKey[] keys = { "list1", "list2" };
var element = db.ListLeftPop(keys, TimeSpan.FromMinutes(1)); // Wait for up to 1 minute for an element from any list
```

10. **Trim a List:**

You can trim a list to keep only a specified range of elements, removing the others.

```csharp
IDatabase db = connection.GetDatabase();
db.ListTrim("myList", 0, 2); // Keep only the elements at indexes 0, 1, and 2
```

## Sets

Redis Lists are similiar to any list structure that don't duplicates and are not ordered (in the sense of choose the position of element)

Typically because of internal implementation sets can be more fast for some operations

```C#
using StackExchange.Redis;

// Connect to the Redis server
ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
IDatabase db = redis.GetDatabase();

// Add items to the set
db.SetAdd("mySet", "item1");
db.SetAdd("mySet", "item2");
db.SetAdd("mySet", "item3");

// Check if an item exists in the set
bool exists = db.SetContains("mySet", "item1");

// Get all items in the set
var allItems = db.SetMembers("mySet");

foreach (var item in allItems)
{
    Console.WriteLine(item);
}

// Remove an item from the set
db.SetRemove("mySet", "item2");

// Close the Redis connection
redis.Close();
```

## Sorted Sets

The same as a sets but can be ordered

```C#
using StackExchange.Redis;

// Connect to the Redis server
ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
IDatabase db = redis.GetDatabase();

// Add elements with scores to the Sorted Set
db.SortedSetAdd("leaderboard", "player1", 1000);
db.SortedSetAdd("leaderboard", "player2", 800);
db.SortedSetAdd("leaderboard", "player3", 1200);

// Retrieve elements by rank (top players)
var topPlayers = db.SortedSetRangeByRank("leaderboard", 0, -1, Order.Descending);

foreach (var player in topPlayers)
{
    Console.WriteLine(player.Element + " - " + player.Score);
}

// Close the Redis connection
redis.Close();
```

## Publish/Subscribe

In Redis, the publish/subscribe (pub/sub) feature allows clients to subscribe to channels and receive messages published to those channels. It's a powerful way to implement real-time messaging, event broadcasting, and notifications.

In this example, we're publishing a message to the "myChannel" channel.

```C#
using StackExchange.Redis;
using System;

class Program
{
    static void Main()
    {
        // Connect to the Redis server
        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
        ISubscriber subscriber = redis.GetSubscriber();

        // Publish a message to a channel
        subscriber.Publish("myChannel", "Hello, subscribers!");

        // Close the Redis connection
        redis.Close();
    }
}
```

The subscriber

```C#
using StackExchange.Redis;
using System;

class Program
{
    static void Main()
    {
        // Connect to the Redis server
        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
        ISubscriber subscriber = redis.GetSubscriber();

        // Subscribe to a channel
        subscriber.Subscribe("myChannel", (channel, message) =>
        {
            Console.WriteLine($"Received message from {channel}: {message}");
        });

        Console.WriteLine("Subscribed to myChannel. Press Enter to exit.");
        Console.ReadLine();

        // Close the Redis connection
        redis.Close();
    }
}
```

# Atomic operations

In Redis, atomic transactions are a way to ensure that a series of Redis commands are executed as a single, indivisible unit, guaranteeing that all the commands either succeed or fail together. Redis provides the MULTI and EXEC commands to create and execute atomic transactions.

```C#
using StackExchange.Redis;
using System;

class Program
{
    static void Main()
    {
        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
        IDatabase db = redis.GetDatabase();

        // Start a new transaction
        var transaction = db.CreateTransaction();

        // Queue up multiple commands within the transaction
        transaction.StringSetAsync("key1", "value1");
        transaction.StringSetAsync("key2", "value2");
        transaction.StringSetAsync("key3", "value3");

        // Execute the transaction atomically
        bool committed = transaction.Execute();

        if (committed)
        {
            Console.WriteLine("Transaction succeeded");
        }
        else
        {
            Console.WriteLine("Transaction failed");
        }

        // Close the Redis connection
        redis.Close();
    }
}
```


