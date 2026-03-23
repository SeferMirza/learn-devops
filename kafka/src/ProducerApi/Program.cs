using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("/produce/{topic}", async ([FromRoute] string topic, [FromBody] object message) =>
{
    var producerBuilder = new ProducerBuilder<Null, string>(new ProducerConfig()
    {
        BootstrapServers = "localhost:9092",
        Acks = Acks.All,
    });

    using var producer = producerBuilder.Build();

    return await producer.ProduceAsync(topic, new Message<Null, string>
    {
        Value = JsonSerializer.Serialize(message)
    });
});

app.Run();