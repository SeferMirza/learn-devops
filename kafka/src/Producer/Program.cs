using Confluent.Kafka;

var producerBuilder = new ProducerBuilder<string, string>(new ProducerConfig()
{
    BootstrapServers = "localhost:9092",
    Acks = Acks.All,
});

var producer = producerBuilder.Build();

while (true)
{
    try
    {
        var result = await producer.ProduceAsync("demo-topic" , new Message<string, string>
        {
            Key = $"#{DateTime.Now:yyyyMMddhhmmss}",
            Value = "Message"
        });
        Console.WriteLine($"Key:{result.Message.Key}, Value: {result.Message.Value}, Offset: {result.Offset}");

        await Task.Delay(500);

        result = await producer.ProduceAsync("demo-topic", new Message<string, string>
        {
            Key = "1234",
            Value = $"Message"
        });
        Console.WriteLine($"Key:{result.Message.Key}, Value: {result.Message.Value}, Offset: {result.Offset}");

        await Task.Delay(500);
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}