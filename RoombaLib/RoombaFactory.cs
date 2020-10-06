namespace RoombaLib
{
    public class RoombaFactory
    {
        public IRoomba Connect(string user, string password, string host)
        {
            return new Roomba(new MqttNetAdapter(user, password, host));
        }
    }
}