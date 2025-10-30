using Confluent.Kafka;

const string bootstrap = "host.docker.internal:9092"; // Hangi adrese bağlanayım?
const string topic = "quickstart-events"; // Mesajların gittiği kanal adı

var config = new ConsumerConfig
{
    BootstrapServers = bootstrap,
    GroupId = "demo-consumer-group", // GroupId: tüketici grubunun adı. aynı gruptaki consumer’lar yük paylaşıyor.
    AutoOffsetReset = AutoOffsetReset.Earliest, // AutoOffsetReset = Earliest: bu grup ilk kez başlıyorsa, “en baştan” tüketmeye başla.
    // geliştirme kolaylığı
    EnableAutoCommit = true // EnableAutoCommit: okuduğun offset’ler otomatik kaydedilir.
};

using var consumer = new ConsumerBuilder<string, string>(config).Build();
consumer.Subscribe(topic);

Console.WriteLine("Consumer dinlemede. Çıkmak için Ctrl+C.");
Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    try { consumer.Close(); } catch { /* yoksay */ }
};

try
{
    while (true)
    {
        var cr = consumer.Consume(); // Consume(): broker’dan bir mesaj çeker.
        Console.WriteLine($"[{cr.TopicPartitionOffset}] key={cr.Message.Key} value={cr.Message.Value}");
    }
}
catch (OperationCanceledException)
{
    // normal çıkış
}
