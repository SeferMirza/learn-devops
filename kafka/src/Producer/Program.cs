using Confluent.Kafka;

using var producer = new ProducerBuilder<string, string>(new ProducerConfig()
{
    BootstrapServers = "localhost:9092",
    Acks = Acks.All,
}).Build();

while (true)
{
    try
    {
        var result = await producer.ProduceAsync("demo-topic", new Message<string, string>
        {
            Key = Guid.NewGuid().ToString(),
            Value = $"Message #{DateTime.Now:yyyyMMddhhmmss}"
        });

        Console.WriteLine($"Key:{result.Message.Key}, Value: {result.Message.Value}, Offset: {result.Offset}");

        await Task.Delay(2000);
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}