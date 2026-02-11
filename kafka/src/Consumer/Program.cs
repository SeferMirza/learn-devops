using Confluent.Kafka;

using var consumer = new ConsumerBuilder<string, string>(new ConsumerConfig
{
    BootstrapServers = "localhost:9092",
    GroupId = "1",
    AutoOffsetReset = AutoOffsetReset.Earliest
}).Build();

consumer.Subscribe("demo-topic");

while (true)
{
    var result = consumer.Consume(TimeSpan.FromSeconds(5));
    if (result == null)
    {
        Console.WriteLine("Awaiting Message");
        continue;
    }

    Console.WriteLine($"Key: {result.Message.Key}, Value: {result.Message.Value}, Offset: {result.Offset}");
}
