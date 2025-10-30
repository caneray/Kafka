using Confluent.Kafka;

const string bootstrap = "host.docker.internal:9092"; // Hangi adrese bağlanayım?
const string topic = "quickstart-events"; // Mesajların gittiği kanal adı

var config = new ProducerConfig // ProducerConfig = nereye bağlanacağını ve davranışını söyler.
{
    BootstrapServers = bootstrap,
    LingerMs = 5,
};

using var producer = new ProducerBuilder<string, string>(config).Build();

Console.WriteLine("Producer hazır. Mesaj yaz ve Enter'a bas. Çıkmak için boş satır gönder.");
while (true)
{
    var line = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(line))
        break;

    var key = Guid.NewGuid().ToString("N"); // mesajın anahtarı (partition seçimine etki eder, aynı key → aynı partition).
    var msg = new Message<string, string> { Key = key, Value = line };

    try
    {
        var result = await producer.ProduceAsync(topic, msg);
        Console.WriteLine($"Gönderildi -> {result.TopicPartitionOffset} | key={key}");
        /* 
            result.TopicPartitionOffset: hangi topic, hangi partition, kaçıncı offset’e yazıldı:
                quickstart-events [[0]] @3:
                    topic: quickstart-events
                    partition: 0
                    offset: 3 (bu, o partition’daki 4. mesaj demek; 0’dan başlar)
         */
    }
    catch (ProduceException<string, string> ex)
    {
        Console.WriteLine($"HATA: {ex.Error.Reason}");
    }
}

producer.Flush(TimeSpan.FromSeconds(5));
Console.WriteLine("Producer kapandı.");
