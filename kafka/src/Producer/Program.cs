using Confluent.Kafka;

var producerBuilder = new ProducerBuilder<string, string>(new ProducerConfig()
{
    BootstrapServers = "localhost:9092",
    Acks = Acks.All,
});

using var producer = producerBuilder.Build();

Task[] producers = [
    Produce(),
    Produce(key: "1234")
];

await Task.WhenAll(producers);

Task Produce(
    string? key = default
) => Task.Run(async () =>
    {
        while (true)
        {
            try
            {
                var result = await producer.ProduceAsync("demo-topic", new Message<string, string>
                {
                    Key = key ?? $"#{DateTime.Now:ddhhmmss}",
                    Value = "Message"
                });
                Console.WriteLine(
                    $"Partition: {result.Partition.Value}," +
                    $"Offset: {result.Offset}," +
                    $"Key: {result.Message.Key}," +
                    $"Value: {result.Message.Value}"
                );

                await Task.Delay(2000);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    });
