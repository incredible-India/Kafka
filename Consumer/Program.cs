using Confluent.Kafka;
// Import the Kafka library provided by Confluent, which allows working with Kafka producers and consumers.
var config = new ConsumerConfig
{
    GroupId = "test-consumer-group",
    BootstrapServers = "localhost:9092",
    AutoOffsetReset = AutoOffsetReset.Earliest
};
// Configure the Kafka consumer:
// GroupId: Specifies the consumer group the consumer belongs to (used for load balancing).
// BootstrapServers: The Kafka server address (broker).
// AutoOffsetReset: Defines where to start reading messages if no committed offset exists (Earliest = from the beginning).
using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
// Create a Kafka consumer instance with the specified configuration.
// Ignore is the key type (not used here), and string is the value type (the message's content).
consumer.Subscribe("mytopic");
// Subscribe the consumer to the topic "mytopic" to start consuming messages from it.
CancellationTokenSource token = new();
// Create a CancellationTokenSource to allow cancellation of the consumer loop.
try
{
    while (true)
    {
        var resposne = consumer.Consume(token.Token);
        // Consume a message from the subscribed topic. This is a blocking call that waits until a message is available or the token is canceled.

        Console.WriteLine($"Consumed message '{resposne.Message.Value}' at: '{resposne.TopicPartitionOffset}'");
        // Print the message's value (content) and its metadata (topic, partition, and offset) to the console.
    }
}
catch (Exception)
{
    throw;
    // Catch any exceptions that might occur and rethrow them. (Consider improving this with specific exception handling.)
}

//gettting out put in console  Consumed message 'Hi from Server--> hi' at: 'mytopic [[0]] @0'
//Hi from Server--> hi:
//This is the content of the message that was published to the Kafka topic. It is extracted from resposne.Message.Value.

//mytopic:
//This is the name of the Kafka topic from which the message was consumed. It is part of the metadata provided by resposne.TopicPartitionOffset.

//[[0]]:
//This indicates the partition number within the topic. Kafka topics are divided into partitions to distribute data across brokers and allow parallelism.
//Here, 0 means the message was consumed from partition 0 of the topic mytopic.

//@0:
//This represents the offset of the message in the partition.
//The offset is a unique number assigned to each message within a partition, indicating its position.
//In this case, @0 means this is the first message in partition 0.

//Full Interpretation:
//The output means:

//A message with the content 'Hi from Server--> hi' was consumed from topic mytopic,
//It came from partition 0,
//And it was the first message in that partition (offset 0).