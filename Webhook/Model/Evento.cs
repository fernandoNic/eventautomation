namespace Webhook.Model
{
    public class Evento
    {
        public string client_key { get; set; }
        public string @event { get; set; }
        public ulong create_time { get; set; }
        public string user_openid { get; set; }
        public string content { get; set; }
    }
}
