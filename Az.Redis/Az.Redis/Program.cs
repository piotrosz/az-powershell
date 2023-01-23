using StackExchange.Redis;
using static System.Console;

var connectionString = "";

await using var cache = ConnectionMultiplexer.Connect(connectionString);
IDatabase db = cache.GetDatabase();


// Snippet below executes a PING to test the server connection
var result = await db.ExecuteAsync("ping");
WriteLine($"PING = {result.Type} : {result}");

// Call StringSetAsync on the IDatabase object to set the key "test:key" to the value "100"
bool setValue = await db.StringSetAsync("test:key", "100");
WriteLine($"SET: {setValue}");

// StringGetAsync takes the key to retrieve and return the value
string? getValue = await db.StringGetAsync("test:key");

WriteLine($"GET: {getValue}");