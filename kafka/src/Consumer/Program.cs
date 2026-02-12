using Confluent.Kafka;

var config = new ConsumerConfig
{
    BootstrapServers = "localhost:9092",
    GroupId = "1",
    AutoOffsetReset = AutoOffsetReset.Earliest
};
var consumerBuilder = new ConsumerBuilder<string, string>(config);
var consumerWithNoCommitBuilder = new ConsumerBuilder<string, string>(new ConsumerConfig(config) { EnableAutoCommit = false });

Task[] consumers = [
    Consume(consumerBuilder, 0),
    Consume(consumerWithNoCommitBuilder, 1, manualCommit: true),
    Consume(consumerWithNoCommitBuilder, 2)
];

await Task.WhenAll(consumers);

Task Consume(ConsumerBuilder<string, string> builder, int consumerId,
    bool manualCommit = false
) =>
    Task.Run(() =>
    {
        using var consumer = builder.Build();
        consumer.Subscribe("demo-topic");

        while (true)
        {
            var result = consumer.Consume(TimeSpan.FromSeconds(5));
            if (result == null) continue;

            Console.WriteLine(
                $"Consumer: {consumerId}," +
                $"Partition: {result.Partition.Value}," +
                $"Offset: {result.Offset}," +
                $"Key: {result.Message.Key}," +
                $"Value: {result.Message.Value}"
            );

            if (manualCommit)
            {
                consumer.Commit(result);
            }
        }
    });