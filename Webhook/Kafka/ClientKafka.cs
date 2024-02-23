using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Text.Json;
using Webhook.Model;

namespace Webhook.Kafka
{
    public class ClientKafka
    {
        private readonly IConfiguration _configuration;

        public ClientKafka(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void publishMessage(string message)
        {
            try
            {
                /*
                this way is not performance friendly, because we will load the complete file appsettings.json
                on memory (on runtime) every time our controller is called
                */

                //first method
                var setting = _configuration.GetSection("Kafka");
                string bootstrapServersKafka = setting.GetSection("BootstrapServers").Value;
                string SaslUsername          = setting.GetSection("SaslUsername").Value;
                string SaslPassword          = setting.GetSection("SaslPassword").Value;
                string topic                 = setting.GetSection("Topic").Value;
                string SslCaLocation         = setting.GetSection("SslCaLocation").Value;

                //string bootstrapServersKafka = _configuration.GetSection("Kafka").GetSection("BootstrapServers").Value;

                // second method
                //string bootstrapServersKafka2 = _configuration.GetValue<string>("Kafka:BootstrapServers");

                var config = new ProducerConfig
                {
                    BootstrapServers = bootstrapServersKafka,
                    SslCaLocation    = SslCaLocation,
                    SaslMechanism    = SaslMechanism.ScramSha512,
                    SecurityProtocol = SecurityProtocol.SaslSsl,
                    SaslUsername     = SaslUsername,
                    SaslPassword     = SaslPassword
                };

                var producer = new ProducerBuilder<Null, string>(config)
                    //.SetValueSerializer(new JsonSerializer().SerializeToElement<Evento>)
                    .Build();
                //var kafkamessage = new Message<Null, string> { Value = "Hello, Kafka! from webhook" };
                
                //var msg = JsonSerializer.Deserialize<Evento>(message);
                var kafkamessage = new Message<Null, string> { Value = message, };

                var result = producer.ProduceAsync(topic, kafkamessage);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.ToString());
            }
        }
    }
}
