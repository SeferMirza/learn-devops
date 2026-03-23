using Consumer;

// /* Uncomment to use specific consumer setup */
// var multipleConsumers = new MultipleConsumers();
// await multipleConsumers.RunAsync();

var batchConsumer = new BatchConsumer();
await batchConsumer.RunAsync();

// /* Uncomment to use specific consumer setup */
// var exceedMaxPollIntervalConsumer = new ExceedMaxPollIntervalConsumer();
// await exceedMaxPollIntervalConsumer.RunAsync();