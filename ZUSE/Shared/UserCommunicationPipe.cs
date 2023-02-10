using System;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;

namespace ZUSE.Shared
{
    public class UserCommunicationPipe
    {
        public static IManagedMqttClient _client { get; set; } = null!;
        public static MqttClientOptions _options = null!;
        private static ManagedMqttClientOptions _managedMqttClientOptions = null!;

        public delegate void MsgListener(string topic, string message);
        private static MsgListener Listener;
        public static Action? Connected { get; set; }
        public static Action? Disconnected { get; set; }
        public static bool isFirstConnection { get; set; } = true;
        public static List<string> Topics { get; set; } = new();


        public static async Task<bool> StartConnection(string topic, string client_id = "",
            Action? connected = null, Action? dissconnected = null, bool onlyPublish = false)
        {
            Connected = connected;
            Disconnected = dissconnected;

            if (_client is not null)
                return true;

            var factory = new MqttFactory();

            _client = factory.CreateManagedMqttClient();
            //configure options
            //var lw = new MqttApplicationMessage()
            //{
            //    Topic = topic,
            //    Payload = Encoding.UTF8.GetBytes($"{client_type}~lost"),
            //    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
            //    Retain = false
            //};
            //FullTopic = $"{topic}/{client_type}";

            //string ClientID = "";

            //if (!same_type_unique_id.Equals(""))
            //    ClientID += $"{topic }/{same_type_unique_id}/{client_type}";
            //else
            //    ClientID += FullTopic;
            Topics.Add(topic);
            _options = new MqttClientOptionsBuilder()
                .WithClientId(client_id)
                .WithWebSocketServer("wss://j56fc68f.us-east-1.emqx.cloud:8084/mqtt")
                .WithCredentials("tathkara", "121212")
                .WithKeepAlivePeriod(TimeSpan.FromSeconds(20))
                .Build();

            _managedMqttClientOptions = new ManagedMqttClientOptionsBuilder()
                .WithClientOptions(_options)
                .Build();

            //await _client.SubscribeAsync($"{topic}/{client_type}");

            await _client.StartAsync(_managedMqttClientOptions);

            if (onlyPublish)
                return false;

            await Subsicribe(topic);
            _client.DisconnectedAsync += _client_DisconnectedAsync;
            _client.ConnectedAsync += _client_ConnectedAsync;

            return false;
        }

        private static Task _client_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            string payload = arg.ApplicationMessage.ConvertPayloadToString();
            //Console.WriteLine($"receiving : {payload}");

            Listener(arg.ApplicationMessage.Topic, payload);
            return Task.FromResult(1);
        }

        private static Task _client_ConnectedAsync(MqttClientConnectedEventArgs arg)
        {
            _client.ApplicationMessageReceivedAsync += _client_ApplicationMessageReceivedAsync;
            Topics.ForEach(
                async topic =>
                    await Subsicribe(topic)
            );
            Console.WriteLine("Connected successfully with servers");
            Connected();
            return Task.FromResult(1);
        }

        private static async Task _client_DisconnectedAsync(MqttClientDisconnectedEventArgs arg)
        {
            try
            {
                Console.WriteLine($"dissconnected {arg.Reason}");
                isFirstConnection = false;
                Disconnected();
                _ = Task.Run(
                    async () =>
                    {
                        // User proper cancellation and no while(true).
                        while (!_client.IsConnected)
                        {
                            try
                            {
                                Console.WriteLine("reconnection in progress");
                                await _client.StartAsync(_managedMqttClientOptions);
                                // Subscribe to topics when session is clean etc.
                            }
                            catch
                            {
                                Console.WriteLine($"reconnection failed {arg.Reason}");
                            }
                            finally
                            {
                                //Check the connection state every 5 seconds and perform a reconnect if required.
                                await Task.Delay(TimeSpan.FromSeconds(1));
                            }
                        }
                        Disconnected();
                    });
                return;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static void AddMsgListener(MsgListener received)
        {
            //MsgListenerPointer = received;
            Listener += received;

        }
        public static void RemoveMsgListener(MsgListener received)
        {
            Listener -= received;
        }

        public static async Task Publish(string topic, string msg, bool retain = false)
        {
            if (_client is null)
                return;

            if(msg.Length < 1000)
                Console.WriteLine($"Publishing {msg} , {topic}");
            else
                Console.WriteLine($"Publishing to {topic}");
            if (!_client.IsConnected)
            {
                await _client.EnqueueAsync(topic: topic, payload: msg,
                    retain: retain);
                return;
            }
            await _client.InternalClient.PublishStringAsync(topic: topic, payload: msg,
                retain: retain);
        }
        public async static Task Subsicribe(string topic)
        {
            Console.WriteLine($"listening to {topic}, number of topics: " + Topics.Count);
            if(!Topics.Contains(topic))
                Topics.Add(topic);
            await _client.SubscribeAsync(topic, MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce);
            //_client.SubscribeAsync(new MqttTopicFilterBuilder()
            //                            .WithTopic(topic)
            //                            .Build());
        }
        public async static Task UnSubsicribe(string topic)
        {
            await _client.UnsubscribeAsync(topic);
            Topics.RemoveAll(t => t.Equals(topic));
        }
        public static bool isSubsicribed(string topic)
        {
            return Topics.Contains(topic);
        }
        public static async Task Disconnect()
        {
            _client.DisconnectedAsync -= _client_DisconnectedAsync;
            Topics.ForEach(
                    async topic =>
                        await UnSubsicribe(topic)
                );
            await _client.StopAsync();
        }
    }
}

