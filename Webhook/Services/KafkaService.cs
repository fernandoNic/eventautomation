using Confluent.Kafka;
using Webhook.Controllers;
using Webhook.Kafka;
using Webhook.Model;

namespace Webhook.Services
{
    public class KafkaService
    {
        private readonly ClientKafka _clientKafka;

        public KafkaService(ClientKafka clientKafka) 
        {
            _clientKafka = clientKafka;
        }

        public void publish(string msg) 
        {
            _clientKafka.publishMessage(msg);
        }
    }
}
