## Kafka Docker Komutları

| İşlem | Komut |
|-------|-------|
| **Topic oluştur** | `docker exec -it kafka /opt/kafka/bin/kafka-topics.sh --create --topic quickstart-events --bootstrap-server localhost:9092` |
| **Topic’leri listele** | `docker exec -it kafka /opt/kafka/bin/kafka-topics.sh --list --bootstrap-server localhost:9092` |
| **Producer başlat (mesaj gönder)** | `docker exec -it kafka /opt/kafka/bin/kafka-console-producer.sh --topic quickstart-events --bootstrap-server localhost:9092` |
| **Consumer başlat (mesaj al)** | `docker exec -it kafka /opt/kafka/bin/kafka-console-consumer.sh --topic quickstart-events --from-beginning --bootstrap-server localhost:9092` |
| **Container’ın içinde neler var görmek istersen** | `docker exec -it kafka bash`<br>`ls /opt/kafka/bin` |

> Bu komutlar, Kafka konteynerinde temel işlemleri (topic oluşturma, mesaj gönderme/alma) hızlıca denemek için kullanılabilir. <br>

> Bu projeyi çalıştırmak ve mesaj sistemini göndermeyi görmemiz için clientte Docker Desktop uygulaması kurulu olması gerekmektedir. Sonrasında projenin dizinine gelip bu dizinde docker compose up -d yaparak kafkayı ayağa kaldırdıktan sonra yukarıda bulunan komutları yazarak mesajların consumer tarafına iletildiği görebilir. <br>

> Producer projesini Visual Studio üzerinden kaldırmak istersek Producer altındaki Program.cs e gidip bootstrap olarak tanımlanan bağlanacağı adresi ***const string bootstrap = "localhost:9092";*** bu şekilde değiştirilmesi gerekmektedir.
