using Confluent.Kafka;

var consumerBuilder = new ConsumerBuilder<string, string>(new ConsumerConfig
{
    BootstrapServers = "localhost:9092",
    GroupId = "1",
    AutoOffsetReset = AutoOffsetReset.Earliest
});

var consumers = Enumerable.Repeat(0, 3).Select((_, index) => Execute(consumerBuilder, index));

await Task.WhenAll(consumers);

Task Execute(ConsumerBuilder<string, string> builder, int consumerId) =>
    Task.Run(() =>
    {
        using var consumer = builder.Build();
        consumer.Subscribe("demo-topic");

        while (true)
        {
            var result = consumer.Consume(TimeSpan.FromSeconds(5));
            if (result == null) continue;

            Console.Write($"Consumer: {consumerId},");
            Console.Write($"Partition: {result.Partition.Value},");
            Console.Write($"Offset: {result.Offset},");
            Console.Write($"Key: {result.Message.Key},");
            Console.WriteLine($"Value: {result.Message.Value}");
        }
    });