using Confluent.Kafka;
using System.Xml;

//ack.all ean all broker inside cluseter will infor they got the data
var config = new ProducerConfig { BootstrapServers = "localhost:9092", Acks = Acks.All };

//null -Key string -value 
using var producer = new ProducerBuilder<Null, string>(config).Build();

try
{
    string? input;
while( (input = Console.ReadLine()) != null)
{
        //produce also methid which will return void nut  it has deliate handler
        //prodduce async will return reposne too
        var reposne = await producer.ProduceAsync("mytopic", new Message<Null, string> { Value = "Hi from Server--> "+input });
        Console.WriteLine($"Delivered '{reposne.Value}' to '{reposne.TopicPartitionOffset}'");

    }   
    }
catch (ProduceException<Null,string> excepton)
{

    Console.WriteLine( excepton.Message);
}
