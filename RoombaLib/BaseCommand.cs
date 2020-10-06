using System;

namespace RoombaLib
{
    internal class BaseCommand : ICommand
    {
        public string Command { get; set; }
        public long Time { get; set; } = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
        public string Initiator { get; set; } = "localApp";
    }
}