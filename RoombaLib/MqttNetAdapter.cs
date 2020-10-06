using System.Threading;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;

namespace RoombaLib
{
    public class MqttNetAdapter : IRoombaAdapter
    {
        private const int RoombaMqttPort = 8883;
        
        public bool Connected { get; private set; }

        private static readonly MqttFactory Factory = new MqttFactory();
        
        private readonly IMqttClient _mqttClient;
        private readonly IMqttClientOptions _mqttClientOptions;

        public MqttNetAdapter(string username, string password, string host)
        {
            _mqttClient = Factory.CreateMqttClient();
            
            _mqttClient.UseConnectedHandler(arg => Connected = true);
            _mqttClient.UseDisconnectedHandler(arg => Connected = false);
            
            _mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(host, RoombaMqttPort)
                .WithClientId(username)
                .WithCredentials(username, password)
                .WithTls(x =>
                {
                    x.UseTls = true;
                    x.CertificateValidationHandler = context => true;
                })
                .WithCleanSession(false)
                .Build();
        }

        private async Task ConnectAsync()
        {
            if (Connected)
                return;
            
            await _mqttClient.ConnectAsync(_mqttClientOptions, CancellationToken.None);
        }

        private async Task DisconnectAsync()
        {
            if (!Connected)
                return;
            
            await _mqttClient.DisconnectAsync();
        }

        public async Task SendCommandAsync(string payload)
        {
            await ConnectAsync();
            var message = new MqttApplicationMessageBuilder()
                .WithTopic("cmd")
                .WithPayload(payload)
                .WithAtMostOnceQoS()
                .Build();
            await _mqttClient.PublishAsync(message);
        }

        public void Dispose()
        {
            if (Connected)
            {
                DisconnectAsync().GetAwaiter().GetResult();
            }
        }
    }
}